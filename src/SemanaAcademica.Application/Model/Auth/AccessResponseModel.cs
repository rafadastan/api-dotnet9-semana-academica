namespace SemanaAcademica.Application.Models.User
{
    /// <summary>
    /// Model de resposta após autenticação do usuário
    /// </summary>
    public class AccessResponseModel
    {
        /// <summary>
        /// E-mail do usuário autenticado
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Token de acesso gerado após autenticação
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Data de expiração do token
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}