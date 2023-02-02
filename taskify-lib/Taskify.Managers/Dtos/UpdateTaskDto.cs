namespace TaskifyAPI.Dtos
{
    public record UpdateTaskDto
    {
        public UpdateTaskDto(TaskKey key, string title, string description) {
            Key = key;
            Title = title;
            Description = description;
        }
        public TaskKey Key { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
