namespace Taskify.Data.Models
{
    public record TaskModel
    {
        public Guid Id { get; set; }
        public Guid? ParentTask { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }

        public TaskModel()
        {

        }

        public TaskModel(TaskModel task, Guid? parentTask)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            CreatedBy = task.CreatedBy;
            CreationDate = task.CreationDate;
            ParentTask = parentTask;
        }
    }
}
