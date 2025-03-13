using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace Tests
{
    public class ProdutosMvcControllerTests
    {
        private readonly ProdutosMvcController _controller;
        private readonly Mock<IProdutoService> _produtoServiceMock;

        public ProdutosMvcControllerTests()
        {
            _produtoServiceMock = new Mock<IProdutoService>();
            _produtoServiceMock.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<Produto>
                {
                    new Produto { Id = 1, Nome = "Produto 1", Descricao = "Desc 1", Preco = 10 },
                    new Produto { Id = 2, Nome = "Produto 2", Descricao = "Desc 2", Preco = 20 }
                });
            _controller = new ProdutosMvcController(_produtoServiceMock.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfProdutos()
        {
            var result = await _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Produto>>(viewResult.Model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_ModelStateInvalid_ReturnsViewWithProduto()
        {
            _controller.ModelState.AddModelError("Nome", "Campo obrigatório");
            var produto = new Produto { Nome = "", Descricao = "Alguma desc", Preco = 10 };
            var result = await _controller.Create(produto);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(produto, viewResult.Model);
        }

        [Fact]
        public async Task Edit_Get_ProductFound_ReturnsViewResultWithProduto()
        {
            int id = 1;
            var produto = new Produto { Id = id, Nome = "Produto Edit", Descricao = "Desc Edit", Preco = 15 };
            _produtoServiceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(produto);
            var result = await _controller.Edit(id);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Produto>(viewResult.Model);
            Assert.Equal(id, model.Id);
        }

        [Fact]
        public async Task Edit_Get_ProductNotFound_ReturnsNotFound()
        {
            int id = 999;
            _produtoServiceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync((Produto)null);
            var result = await _controller.Edit(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Post_IdMismatch_ReturnsBadRequest()
        {
            int id = 1;
            var produto = new Produto { Id = 2, Nome = "Produto", Descricao = "Desc", Preco = 20 };
            var result = await _controller.Edit(id, produto);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Edit_Post_ModelStateInvalid_ReturnsViewWithProduto()
        {
            int id = 1;
            var produto = new Produto { Id = id, Nome = "", Descricao = "Desc", Preco = 20 };
            _controller.ModelState.AddModelError("Nome", "Campo obrigatório");
            var result = await _controller.Edit(id, produto);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(produto, viewResult.Model);
        }

        [Fact]
        public async Task Delete_Get_ProductFound_ReturnsViewResultWithProduto()
        {
            int id = 1;
            var produto = new Produto { Id = id, Nome = "Produto Delete", Descricao = "Desc Delete", Preco = 30 };
            _produtoServiceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(produto);
            var result = await _controller.Delete(id);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Produto>(viewResult.Model);
            Assert.Equal(id, model.Id);
        }

        [Fact]
        public async Task Delete_Get_ProductNotFound_ReturnsNotFound()
        {
            int id = 999;
            _produtoServiceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync((Produto)null);
            var result = await _controller.Delete(id);
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
