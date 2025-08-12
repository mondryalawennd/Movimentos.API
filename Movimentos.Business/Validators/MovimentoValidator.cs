using FluentValidation;
using Movimentos.Entities.Entities;

namespace Movimentos.Business.Validators
{
    public class MovimentoValidator : AbstractValidator<Movimento>
    {
        public MovimentoValidator()
        {
            RuleFor(m => m.DataAno)
                .GreaterThan(0).WithMessage("O ano do movimento deve ser informado.")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("O ano do movimento não pode ser maior que o ano atual.");

            RuleFor(m => m.DataMes)
                .InclusiveBetween(1, 12).WithMessage("O mês do movimento deve estar entre 1 e 12.");

            RuleFor(m => m.CodigoProduto)
                .NotEmpty().WithMessage("O código do produto é obrigatório.")
                .MaximumLength(15).WithMessage("O código do produto não pode exceder 15 caracteres.");

            RuleFor(m => m.CodigoCosif)
                .NotEmpty().WithMessage("O código COSIF é obrigatório.")
                .MaximumLength(15).WithMessage("O código COSIF não pode exceder 15 caracteres.");

            RuleFor(m => m.Descricao)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(200).WithMessage("A descrição não pode exceder 200 caracteres.");

            RuleFor(m => m.Valor)
                .GreaterThan(0).WithMessage("O valor deve ser maior que zero.");
        }
    }
}
