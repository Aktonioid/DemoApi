namespace DemoApi.Dto
{
    public record UserLogin
    {
        public string? Login { get; set; }
        public string? Password { get; set; }

    }
}
