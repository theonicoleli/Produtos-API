using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ProdutosMvcController : Controller
    {
        private readonly IProdutoService _produtoService;

        public ProdutosMvcController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoService.GetAllAsync();
            return View(produtos);
        }

        public IActionResult Create() => View(model: new Produto { Nome = string.Empty, Preco = 0 });

        [HttpPost]
        public async Task<IActionResult> Create(Produto produto)
        {
            if (!ModelState.IsValid)
                return View(produto);

            try
            {
                await _produtoService.AddAsync(produto);
                TempData["SuccessMessage"] = "Produto criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao salvar o produto: {ex.Message}");
                return View(produto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _produtoService.GetByIdAsync(id);
            if (produto == null)
                return NotFound();
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            if (id != produto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(produto);

            try
            {
                await _produtoService.UpdateAsync(produto);
                TempData["SuccessMessage"] = "Produto atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao atualizar o produto: {ex.Message}");
                return View(produto);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _produtoService.GetByIdAsync(id);
            if (produto == null)
                return NotFound();
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _produtoService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Produto excluído com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao excluir o produto: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
