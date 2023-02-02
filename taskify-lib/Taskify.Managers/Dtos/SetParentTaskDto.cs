namespace TaskifyAPI.Dtos
{
    public class SetParentTaskDto {
        public SetParentTaskDto() {
        }
        public SetParentTaskDto(TaskKey key, Guid? newParentId) {
            Key = key;
            NewParentId = newParentId;
        }
        public TaskKey Key { get; set; }
        public Guid? NewParentId {  get; set; }
    }
}
