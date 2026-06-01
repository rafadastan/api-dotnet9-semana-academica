namespace SemanaAcademica.Application.Model.Customer
{
    /// <summary>
    /// Model para cadastro de pessoa física
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// Identificador único do cliente
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome completo do cliente
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// CPF do cliente (somente números)
        /// </summary>
        public string Cpf { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do cliente
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Telefone do cliente (somente números)
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Data de nascimento do cliente
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}