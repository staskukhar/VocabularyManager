namespace VocabularyManager.UseCases.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string EntityType { get; }
        public int EntityId { get; }

        public EntityNotFoundException(string entityType, int entityId)
            : base($"{entityType} with id '{entityId}' was not found.")
        {
            EntityType = entityType;
            EntityId = entityId;
        }
    }
}
