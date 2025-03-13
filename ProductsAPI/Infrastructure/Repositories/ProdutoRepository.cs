using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
            => await _context.Produtos.ToListAsync();

        public async Task<Produto> GetByIdAsync(int id)
            => await _context.Produtos.FindAsync(id);

        public async Task AddAsync(Produto produto)
        {
            produto.DataCadastro = DateTime.Now;
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var produto = await GetByIdAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }

        public bool NomeExists(string nome, int id)
            => _context.Produtos.Any(p => p.Nome == nome && p.Id != id);
    }
}
