namespace TaskifyAPI.Dtos {
    public class TaskKey {
        public TaskKey() { }
        public TaskKey(Guid id, Guid? parentId) {
            Id = id;
            ParentId = parentId;
        }
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
    }
}
