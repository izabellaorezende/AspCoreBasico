using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinamento.Models;

namespace Treinamento.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        void Add(Produto produto);
        List<Produto> GetProdutos();
        void Edit(Produto produto);
        void Remove(int ProdutoId);
        Produto Find(int ProdutoId);
    }
}
