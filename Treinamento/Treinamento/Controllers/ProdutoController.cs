using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Treinamento.Models;
using Treinamento.Repositories.Interfaces;

namespace Treinamento.Controllers
{
    public class ProdutoController : Controller
    {

        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        // GET: Produto
        public ActionResult Index()
        {
            return View(_produtoRepository.GetProdutos());
        }

        // GET: Produto/Details/5
        public ActionResult Details(int id)
        {
            return View(_produtoRepository.Find(id));
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produto produto)
        {
            try
            {
                // TODO: Add insert logic here
                _produtoRepository.Add(produto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int id)
        {

            return View(_produtoRepository.Find(id));
        }

        // POST: Produto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Produto produto)
        {
            try
            {
                // TODO: Add update logic here
                _produtoRepository.Edit(produto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_produtoRepository.Find(id));
        }

        // POST: Produto/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Produto produto)
        {
            try
            {
                // TODO: Add delete logic here
                _produtoRepository.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}