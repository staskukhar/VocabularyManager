using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using VocabularyManager.BlazorApp.Auth.DTOs;

namespace VocabularyManager.BlazorApp.Auth;

public class KeycloakAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    public KeycloakAccountClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor)
        : base(accessor) { }

    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(
        RemoteUserAccount account,
        RemoteAuthenticationUserOptions options)
    {
        var user = await base.CreateUserAsync(account, options);

        if (user.Identity?.IsAuthenticated != true)
            return user;

        var identity = (ClaimsIdentity)user.Identity;

        if (account.AdditionalProperties.TryGetValue("realm_access", out var realmAccessObj)
            && realmAccessObj is JsonElement realmAccess)
        {
            identity.AddClaims(
                ExtractRoles(realmAccess).Select(
                    r => new Claim(ClaimTypes.Role, r)));
        }

        return user;
    }

    private static IEnumerable<string> ExtractRoles(JsonElement realmAccess)
    {
        RealmAccessDto[] entries = realmAccess.ValueKind == JsonValueKind.Array
            ? realmAccess.Deserialize<RealmAccessDto[]>() ?? []
            : realmAccess.Deserialize<RealmAccessDto>() is { } single ? [single] : [];

        return entries.SelectMany(ra => ra.Roles).Distinct();
    }
}
