namespace BasicToDoList.Models.Entity
{
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsActive { get; set; }
        public virtual List<Mission> Missions { get; set; }
    }
}
