namespace SistemaGenericoRH.Dtos
{
    public class UserDto
    {
        public int IdUser { get; set; }
        public string Email { get; set; } = null!;
        public string User1 { get; set; } = null!;
        public string? Password { get; set; }
        public bool Status { get; set; }
        public string Gender { get; set; } = null!;
        public DateTime CreateDate { get; set; }
    }
}
