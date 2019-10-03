using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinamento.Context;
using Treinamento.Models;
using Treinamento.Repositories.Interfaces;

namespace Treinamento.Repositories.Classes
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly Contexto _contexto;

        public ProdutoRepository(Contexto contexto)
        {
            _contexto = contexto;
        }

        public void Add(Produto produto)
        {
            _contexto.Add(produto);
            _contexto.SaveChanges();
        }


        public void Edit(Produto produto)
        {
            _contexto.Update(produto);
            _contexto.SaveChanges();
        }

        public Produto Find(int ProdutoId)
        {
            return _contexto.Produto.Find(ProdutoId);
        }

        public List<Produto> GetProdutos()
        {
            return _contexto.Produto.ToList();
        }

        public void Remove(int ProdutoId)
        {
            var produto = Find(ProdutoId);
            _contexto.Produto.Remove(produto);
            _contexto.SaveChanges();
        }

        
    }
}
