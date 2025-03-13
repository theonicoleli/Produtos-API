using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<Produto> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Produto produto)
            => await _repository.AddAsync(produto);

        public async Task UpdateAsync(Produto produto)
            => await _repository.UpdateAsync(produto);

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}
