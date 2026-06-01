namespace SemanaAcademica.Application.Model.Auth
{
    /// <summary>
    /// Model para autenticação do usuário
    /// </summary>
    public class AccessModel
    {
        /// <summary>
        /// E-mail do usuário
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}