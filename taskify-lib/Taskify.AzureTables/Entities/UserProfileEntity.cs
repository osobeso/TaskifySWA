namespace Taskify.AzureTables.Entities
{
    using Azure;
    using Azure.Data.Tables;

    internal class UserProfileEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = string.Empty;
        public string RowKey { get; set; } = string.Empty;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? ZipCode { get; set; }
        public string? Address { get; set; }
    }
}
