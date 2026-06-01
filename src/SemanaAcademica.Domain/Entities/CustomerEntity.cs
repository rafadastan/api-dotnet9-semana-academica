namespace SemanaAcademica.Domain.Entities
{
    public class CustomerEntity
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string Cpf { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; private set; }

        protected CustomerEntity() { }

        public CustomerEntity(string fullName, string cpf, string email, string phone, DateTime birthDate)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Cpf = cpf;
            Email = email;
            Phone = phone;
            BirthDate = birthDate;
            CreatedAt = DateTime.UtcNow;
            Active = true;
        }

        public void Deactivate() => Active = false;

        public void Activate() => Active = true;

        public void Update(string fullName, string email, string phone)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
        }
    }
}