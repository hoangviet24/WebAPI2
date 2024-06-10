namespace DataAnimals.DTO.Login
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
