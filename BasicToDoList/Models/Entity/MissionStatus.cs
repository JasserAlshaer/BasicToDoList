namespace BasicToDoList.Models.Entity
{
    public class MissionStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public virtual List<Mission> Missions { get; set; } 
    }
}
