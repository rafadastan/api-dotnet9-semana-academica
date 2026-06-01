using FluentValidation;
using SemanaAcademica.Domain.Entities;

namespace SemanaAcademica.Domain.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerEntity>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("O nome completo é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.")
                .MaximumLength(150).WithMessage("O nome deve ter no máximo 150 caracteres.");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Length(11).WithMessage("O CPF deve ter exatamente 11 dígitos.")
                .Matches(@"^\d{11}$").WithMessage("O CPF deve conter apenas números.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .MaximumLength(150).WithMessage("O e-mail deve ter no máximo 150 caracteres.")
                .EmailAddress().WithMessage("O e-mail deve estar em um formato válido.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches(@"^\d{10,11}$").WithMessage("O telefone deve conter 10 ou 11 dígitos numéricos.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
                .LessThan(DateTime.UtcNow.AddYears(-18)).WithMessage("O cliente deve ter no mínimo 18 anos.");
        }
    }
}