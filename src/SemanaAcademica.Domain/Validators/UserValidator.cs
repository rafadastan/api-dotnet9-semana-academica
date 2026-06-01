using FluentValidation;
using SemanaAcademica.Domain.Entities;

namespace SemanaAcademica.Domain.Validators
{
    public class UserValidator : AbstractValidator<UserEntity>
    {
        public UserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("O nome completo é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.")
                .MaximumLength(150).WithMessage("O nome deve ter no máximo 150 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .MaximumLength(150).WithMessage("O e-mail deve ter no máximo 150 caracteres.")
                .EmailAddress().WithMessage("O e-mail deve estar em um formato válido.");
        }
    }
}