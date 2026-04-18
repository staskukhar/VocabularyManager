# VocabularyManager

A web application for managing vocabularies and words, built with ASP.NET Core Web API and Blazor WebAssembly, secured via Keycloak.

---

## Roles

The application has two roles:

| Role | Description |
|---|---|
| **Administrator** | Full access to all features: create, read, update, and delete vocabularies, words, and meanings. Administrators also have access to the Vocabulary Craft tool. |
| **User** | Read-only access. Can browse vocabularies, search words, and view word details. Cannot create, edit, or delete any data. |

---

## Role Management (Developer Guide)

### Initial Setup

The Keycloak realm is configured via `keycloak/realm-import.json`. Importing this file into Keycloak creates:

- The `vocabulary-manager` realm with the `vocabulary-app` client
- Two realm roles: `Administrator` and `User`
- Self-registration enabled — new accounts receive the `User` role automatically

To import the realm:

1. Start Keycloak (e.g. via Docker)
2. Open the Keycloak Admin Console at `http://localhost:8180`
3. Log in with admin credentials
4. Go to **Create realm** and upload `keycloak/realm-import.json`

### Creating an Administrator Account

Administrator accounts must be created manually — they are never assigned via self-registration.

1. Open the Keycloak Admin Console
2. Navigate to **Users** and create a new user (or select an existing one)
3. Set a password under the **Credentials** tab
4. Go to the **Role mapping** tab and assign the `Administrator` role

### User Self-Registration

Users can self-register via the login page. Every self-registered account is automatically assigned the `User` role. No manual steps are required.

### Revoking or Changing Roles

To change a user's role:

1. Open the Keycloak Admin Console
2. Navigate to **Users** and select the user
3. Go to the **Role mapping** tab
4. Add or remove roles as needed

---

## Local Development Setup

1. Start Keycloak and import the realm (see above)
2. Configure connection strings and Keycloak URLs in `appsettings.json` / `wwwroot/appsettings.json`
3. Run the API: `dotnet run` from `src/VocabularyManager.Api`
4. Run the Blazor app: `dotnet run` from `src/VocabularyManager.BlazorApp`
