using Microsoft.AspNetCore.Mvc;
using MvcCore.Helpers;
using MvcCore.Models;
using MvcCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class CochesController : Controller
    {
        private IRepositoryCoches repo;
        PathProvider provider;
        public CochesController(IRepositoryCoches repo, PathProvider provider)
        {
            this.repo = repo;
            this.provider = provider;
        }
        public IActionResult Index()
        {
            return View(this.repo.GetCoches());
        }
        [HttpPost]
        public IActionResult Index(string nombre)
        {
            return View(this.repo.BuscarCoche(nombre));
        }
        public IActionResult Details(int idcoche)
        {
            return View(this.repo.BuscarCoche(idcoche));
        }
        public IActionResult Delete(int idcoche)
        {
            this.repo.EliminarCoche(idcoche);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Coche coche)
        {
            this.repo.InsertarCoche(coche);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int idcoche)
        {
            return View(this.repo.BuscarCoche(idcoche));
        }
        [HttpPost]
        public IActionResult Edit(Coche coche)
        {
            this.repo.ModificarCoche(coche);
            return RedirectToAction("Index");
        }
    }
}
