using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieAppWeb.Data;
using MovieAppWeb.Models;

namespace MovieAppWeb.Controllers;

    public class NoticiaController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly MovieContext _context;

        public NoticiaController(ILogger<HomeController> logger,
            MovieContext context)
        {
                _logger = logger;
                _context = context;
        }

        public IActionResult AgregarNoticia(){
                return View();
        }

        [HttpPost]
        public IActionResult AgregarNoticia (Noticia objNoticia)
        {
            objNoticia.UsuarioId = (int)_context.Usuarios.Where(p => p.Username.Equals(User.Identity.Name)).ToList().First().Id;
            if(ModelState.IsValid){
                _context.Add(objNoticia);
                _context.SaveChanges();
                return RedirectToAction("verNoticia");
            }
            
            return View();            
        }
         public IActionResult EditarNoticia(int? id)
        {
            if(id == null){
                return NotFound();
            }
            var noticia = _context.Noticias.Find(id);
            if(noticia == null){
                return NotFound();
            }
            return View(noticia);
        }

        [HttpPost]
        public IActionResult EditarNoticia(int id, Noticia objNoticia)
        {
            if (ModelState.IsValid)
            {
                objNoticia.UsuarioId = (int)_context.Usuarios.Where(p => p.Username.Equals(User.Identity.Name)).ToList().First().Id;
                _context.Update(objNoticia);
                _context.SaveChanges();
                return RedirectToAction("VerNoticia");
            }
            return View();
        }

        public IActionResult BorrarNoticia(int? id)
        {
            var noticia = _context.Noticias.Find(id);
            _context.Noticias.Remove(noticia);
            _context.SaveChanges();
            return RedirectToAction(nameof(VerNoticia));
        }

        public IActionResult VerNoticia(){
            var listNoticia=_context.Noticias.OrderBy(s=>s.ID) .ToList();
            return View(listNoticia);
        }       
        public IActionResult VerNoticia2(){
                return View();
        }
        public IActionResult VerNoticia3(){
                return View();
        }
      
    }
