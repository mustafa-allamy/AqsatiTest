namespace Application.CQRS.User.Dtos
{
    public class LoginResponseDto
    {
        public required string AuthToken { get; set; }
        public required string RefreshToken { get; set; }
        public required string UserName { get; set; }
        public required string Fullname { get; set; }
        //public UserRole Role { get; set; }
        public List<string> Claims { get; set; }
    }
}