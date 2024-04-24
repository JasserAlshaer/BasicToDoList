namespace BasicToDoList.Models.Entity
{
    public class Mission
    {
        public int Id { get; set; }
        public string PriorityLevel { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }

        public virtual MissionStatus MissionStatus { get; set; }    
        public virtual User User { get; set; }
    }
}
