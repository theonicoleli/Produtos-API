using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator(IProdutoRepository repository)
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres")
                .Must((produto, nome) => !repository.NomeExists(nome, produto.Id))
                .WithMessage("Nome já existe");

            RuleFor(x => x.Preco)
                .NotNull().WithMessage("Preço não pode ser nulo")
                .GreaterThan(0).WithMessage("Preço deve ser maior que zero");
        }
    }
}
