using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAppWeb.Data;
using MovieAppWeb.Models;

namespace MovieAppWeb.Controllers;

  public class ReseñaController : Controller
  {
      private readonly MovieContext _context;
      private UserManager<IdentityUser> _um;
      private SignInManager<IdentityUser> _sim;
      public ReseñaController(MovieContext context, UserManager<IdentityUser> um, SignInManager<IdentityUser> sim)
      {
          _context = context;
          _um = um;
          _sim = sim;
      }
      public IActionResult Resena(int? peli)
      {
          if(peli == null)
          {
              return NotFound();
          }
          var resena = _context.Peliculas.Find(peli);
          var infopeli = _context.Reseñas.Find(peli);
          // var infopeli2 = _context.Reseñas.Include(x=> x.Pelicula).Include(y=> y.Usuario).SingleOrDefault(z=> z.Pelicula.ID == peli);
          ViewBag.imgpeli = resena.Imagen;
          ViewBag.titulopeli = resena.Titulo;
          return View();
      }
      [HttpPost]
      public IActionResult resena(int peli, Reseña objResena)
      {
          var resena = _context.Peliculas.Find(peli);
          ViewBag.imgpeli = resena.Imagen;
          ViewBag.titulopeli = resena.Titulo;
          
          if(ModelState.IsValid)
          {
              objResena.Fecha = System.DateTime.Now;
              objResena.PeliculaId = peli;
              objResena.UsuarioId = (int)_context.Usuarios.Where(p => p.Username.Equals(User.Identity.Name)).ToList().First().Id;
              // return BadRequest(objResena);
              _context.Add(objResena);
              _context.SaveChanges();
              return RedirectToAction("Catalogo","Pelicula");
          }
          return View();
      }
      public IActionResult VerResenas(int peli)
      {
          var listaResenas = _context.Reseñas.Include(q=>q.Pelicula).Include(w=>w.Usuario).Where(p=>p.PeliculaId.Equals(peli)).OrderBy(x=>x.Id).ToList();
          var pelicula = _context.Peliculas.Find(peli);
          ViewBag.peli = pelicula.Titulo;
          return View(listaResenas);
      }
  }
