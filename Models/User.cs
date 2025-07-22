namespace BlazorApp1.Models
{
    public class User
    {
        public int Id { get; set; }
        public  string Username { get; set; }
        public  byte[] PasswordHash { get; set; }
        public  byte[] PasswordSalt { get; set; }

        public  Role Role {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

public enum Previlege
{   
    None = 0,
    ReadOnly = 1,
    Export = 2,
    Create = 4,
    Update = 8,
    Delete = 16
}

public enum Role
{
   // Guest = 0, // none
    User = 0, // just read
    Manager = 1, // except create, update and delete

    Director = 2, // except delete
    Admin = 3 // All of previleges
}
