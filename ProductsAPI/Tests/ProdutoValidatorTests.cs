using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.TestHelper;
using Moq;

namespace Tests
{
    public class ProdutoValidatorTests
    {
        private readonly ProdutoValidator _validator;
        private readonly Mock<IProdutoRepository> _repositoryMock;

        public ProdutoValidatorTests()
        {
            _repositoryMock = new Mock<IProdutoRepository>();
            _repositoryMock.Setup(r => r.NomeExists(It.IsAny<string>(), It.IsAny<int>())).Returns(false);
            _validator = new ProdutoValidator(_repositoryMock.Object);
        }

        [Fact]
        public void Deve_Retornar_Erro_Quando_Nome_For_Vazio()
        {
            var produto = new Produto { Nome = "", Preco = 10 };

            var result = _validator.TestValidate(produto);

            result.ShouldHaveValidationErrorFor(x => x.Nome);
        }

        [Fact]
        public void Deve_Retornar_Erro_Quando_Nome_Exceder_100_Caracteres()
        {
            var nomeExcedido = new string('a', 101);
            var produto = new Produto { Nome = nomeExcedido, Preco = 10 };

            var result = _validator.TestValidate(produto);

            result.ShouldHaveValidationErrorFor(x => x.Nome);
        }

        [Fact]
        public void Deve_Retornar_Erro_Quando_Preco_For_Menor_Ou_Igual_A_Zero()
        {
            var produtoPrecoZero = new Produto { Nome = "Produto Teste", Preco = 0 };
            var produtoPrecoNegativo = new Produto { Nome = "Produto Teste", Preco = -1 };

            var resultZero = _validator.TestValidate(produtoPrecoZero);
            var resultNegativo = _validator.TestValidate(produtoPrecoNegativo);

            resultZero.ShouldHaveValidationErrorFor(x => x.Preco);
            resultNegativo.ShouldHaveValidationErrorFor(x => x.Preco);
        }

        [Fact]
        public void Nao_Deve_Retornar_Erro_Para_Produto_Valido()
        {
            var produtoValido = new Produto { Nome = "Produto Teste", Preco = 10 };

            var result = _validator.TestValidate(produtoValido);

            result.ShouldNotHaveValidationErrorFor(x => x.Nome);
            result.ShouldNotHaveValidationErrorFor(x => x.Preco);
        }

        [Fact]
        public void Deve_Retornar_Erro_Quando_Nome_For_Duplicado()
        {
            _repositoryMock.Setup(r => r.NomeExists("Produto Duplicado", It.IsAny<int>())).Returns(true);
            var produtoDuplicado = new Produto { Nome = "Produto Duplicado", Preco = 10, Id = 2 };

            var result = _validator.TestValidate(produtoDuplicado);

            result.ShouldHaveValidationErrorFor(x => x.Nome);
        }

        [Fact]
        public void Nao_Deve_Retornar_Erro_Quando_Nome_Tem_Exatamente_100_Caracteres()
        {
            var nomeValido = new string('b', 100);
            var produto = new Produto { Nome = nomeValido, Preco = 10 };

            var result = _validator.TestValidate(produto);

            result.ShouldNotHaveValidationErrorFor(x => x.Nome);
        }
    }
}
