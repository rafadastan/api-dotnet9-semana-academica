namespace SemanaAcademica.Application.Model.User
{
    /// <summary>
    /// Model para cadastro de usuário
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do usuário (utilizado como login)
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Confirmação de senha
        /// </summary>
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}