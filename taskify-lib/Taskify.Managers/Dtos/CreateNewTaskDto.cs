namespace TaskifyAPI.Dtos {
    public record CreateNewTaskDto {
        public CreateNewTaskDto(string title, string description, Guid? parentId) {
            Title = title;
            Description = description;
            ParentId = parentId;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime CreationDate { get; set; }
    };
}