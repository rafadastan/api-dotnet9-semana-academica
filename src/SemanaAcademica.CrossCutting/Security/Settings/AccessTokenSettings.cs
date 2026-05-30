namespace SemanaAcademica.CrossCutting.Security.Settings
{
    public class AccessTokenSettings
    {
        public string? SecretKey { get; set; }
        public int ExpirationInHours { get; set; }
    }
}
