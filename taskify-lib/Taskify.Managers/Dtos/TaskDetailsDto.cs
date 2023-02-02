namespace TaskifyAPI.Dtos {
    using Taskify.Data.Models;
    public record TaskDetailsDto {
        public TaskDetailsDto(TaskModel task, TaskModel[] children) {
            Task = task;
            ChildTasks = children;
        }
        public TaskModel Task { get; set; }
        public TaskModel[] ChildTasks { get; set; }
    }
}
