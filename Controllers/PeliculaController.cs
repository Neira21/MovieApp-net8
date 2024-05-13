using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Logging;
using MovieAppWeb.Models;
using System.Collections.Generic;
using MovieAppWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MovieAppWeb.Controllers;

public class PeliculaController : Controller
{

  private readonly ILogger<HomeController> _logger;
  private readonly MovieContext _context;
  private UserManager<IdentityUser> _um;
  private SignInManager<IdentityUser> _sim;
  public PeliculaController(ILogger<HomeController> logger,
      MovieContext context, UserManager<IdentityUser> um, SignInManager<IdentityUser> sim)
  {
    _logger = logger;
    _context = context;
    _um = um;
    _sim = sim;
  }
  public IActionResult AgregarPelicula()
  {
    return View();
  }

  [HttpPost]
  public IActionResult AgregarPelicula(Pelicula objPelicula)
  {
    objPelicula.UsuarioId = (int)_context.Usuarios.Where(p => p.Username.Equals(User.Identity.Name)).ToList().First().Id;
    if (ModelState.IsValid)
    {
      if (objPelicula.Trailer.Length > 42)
      {
        objPelicula.Trailer = ConvertirPelicula(objPelicula.Trailer);
      }
      _context.Add(objPelicula);
      _context.SaveChanges();
      return RedirectToAction("Catalogo");
    }
    else
      return View(objPelicula);


    /*
    TempData["Nombre de pelicula"]=objPelicula.Titulo;  
    TempData["Nombre del Director"]=objPelicula.Director;
    TempData["Año"]=objPelicula.Año;
    */
  }
  public string ConvertirPelicula(string urlpelicula)
  {
    urlpelicula = urlpelicula.Substring(32, 11);
    var peliculaurl = "https://www.youtube.com/embed/" + urlpelicula;
    return peliculaurl;
  }

  public IActionResult EditarPelicula(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }
    var pelicula = _context.Peliculas.Find(id);
    if (pelicula == null)
    {
      return NotFound();
    }
    return View(pelicula);
  }

  [HttpPost]
  public IActionResult EditarPelicula(int id, Pelicula objPelicula)
  {
    objPelicula.UsuarioId = (int)_context.Usuarios.Where(p => p.Username.Equals(User.Identity.Name)).ToList().First().Id;
    if (ModelState.IsValid)
    {
      if (objPelicula.Trailer.Length > 42)
      {
        objPelicula.Trailer = ConvertirPelicula(objPelicula.Trailer);
      }
      _context.Update(objPelicula);
      _context.SaveChanges();
      return RedirectToAction("Catalogo");
    }
    return View();
  }

  public IActionResult BorrarPelicula(int? id)
  {
    var pelicula = _context.Peliculas.Find(id);
    _context.Peliculas.Remove(pelicula);
    _context.SaveChanges();
    return RedirectToAction(nameof(Catalogo));
  }
  public IActionResult Catalogo()
  {
    var listPelicula = _context.Peliculas.OrderBy(s => s.ID).ToList();
    return View(listPelicula);
  }

  public async Task<IActionResult> VistaPelicula(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var pelicula = await _context.Peliculas.FindAsync(id);
    if (pelicula == null)
    {
      return NotFound();
    }

    var resenas = await _context.Reseñas
                                .Where(x => x.PeliculaId == id)
                                .OrderBy(x => x.Id)
                                .ToListAsync();

    if (resenas.Any())
    {
      ViewBag.resenas = true;
      Random rnd = new Random();
      int minId = resenas.First().Id;
      int maxId = resenas.Last().Id;
      int nrouser = rnd.Next(minId, maxId + 1);

      var selectedResena = await _context.Reseñas.FindAsync(nrouser);
      if (selectedResena != null)
      {
        ViewBag.fecha = selectedResena.Fecha.ToString("dd/MM/yyyy");
        ViewBag.estrellitas = selectedResena.Estrellas;
        ViewBag.resenausuario = selectedResena.Comentario;

        var usuario = await _context.Usuarios.FindAsync(selectedResena.UsuarioId);
        ViewBag.nombreusuario = usuario?.Username;
      }
    }
    else
    {
      ViewBag.resenas = false;
    }

    return View(pelicula);
  }


  // public IActionResult VistaPelicula(int? id)
  // {
  //     var pelicula = _context.Peliculas.Find(id);
  //     ViewBag.resenas = false;
  //     if (_context.Reseñas.Where(x => x.PeliculaId.Equals(id)).Count() > 0)
  //     {
  //         ViewBag.resenas = true;
  //         Random rnd = new Random();
  //         int nrouser = rnd.Next(_context.Reseñas.Where(x=>x.PeliculaId.Equals(id)).OrderBy(x=>x.Id).ToList().First().Id,_context.Reseñas.Where(x=>x.PeliculaId.Equals(id)).OrderBy(x => x.Id).ToList().Last().Id);
  //         ViewBag.fecha = _context.Reseñas.Find(nrouser).Fecha.ToString();
  //         ViewBag.estrellitas = _context.Reseñas.Where(x=>x.PeliculaId.Equals(id)).FirstOrDefault(x=>x.Id.Equals(nrouser)).Estrellas;
  //         ViewBag.nombreusuario = _context.Usuarios.Where(x => x.Id.Equals(_context.Reseñas.Find(nrouser).UsuarioId)).First().Username;
  //         ViewBag.resenausuario = _context.Reseñas.Where(x=>x.PeliculaId.Equals(id)).FirstOrDefault(x=>x.Id.Equals(nrouser)).Comentario;
  //         ViewBag.fecha = _context.Reseñas.Where(x=>x.PeliculaId.Equals(id)).FirstOrDefault(x=>x.Id.Equals(nrouser)).Fecha;
  //     }
  //     if(pelicula == null){
  //         return NotFound();
  //     }
  //     return View(pelicula);
  // }
  [HttpPost]
  public IActionResult Catalogo(string idfiltro, string filtro)
  {
    var listClientes = _context.Peliculas.OrderBy(s => s.ID).ToList();
    if (idfiltro == "titulo")
    {
      listClientes = _context.Peliculas.Where(c => c.Titulo.ToUpper().Contains(filtro.ToUpper())).OrderBy(s => s.ID).ToList();
    }
    else if (idfiltro == "director")
    {
      listClientes = _context.Peliculas.Where(c => c.Director.ToUpper().Contains(filtro.ToUpper())).OrderBy(s => s.ID).ToList();
    }
    else
    {
      listClientes = _context.Peliculas.Where(c => c.Genero.ToUpper().Contains(filtro.ToUpper())).OrderBy(s => s.ID).ToList();
    }
    // var listClientes=_context.Clientes.Where(c => c.Nombre.ToUpper().Contains(filtro.ToUpper())).OrderBy(s=>s.Id) .ToList();
    return View(listClientes);
  }
  public IActionResult MisPeliculas()
  {
    var listapeliculas = _context.Peliculas.OrderBy(x => x.ID).Where(p => p.Usuario.Username.Equals(User.Identity.Name)).ToList();
    return View(listapeliculas);
  }
  [HttpPost]
  public IActionResult MisPeliculas(string idfiltro, string filtro)
  {
    var listapeliculas = _context.Peliculas.OrderBy(x => x.ID).Where(p => p.Usuario.Username.Equals(User.Identity.Name)).ToList();
    if (idfiltro == "titulo")
    {
      listapeliculas = _context.Peliculas.Where(c => c.Titulo.ToUpper().Contains(filtro.ToUpper())).OrderBy(s => s.ID).ToList();
    }
    else if (idfiltro == "director")
    {
      listapeliculas = _context.Peliculas.Where(c => c.Director.ToUpper().Contains(filtro.ToUpper())).OrderBy(s => s.ID).ToList();
    }
    else
    {
      listapeliculas = _context.Peliculas.Where(c => c.Genero.ToUpper().Contains(filtro.ToUpper())).OrderBy(s => s.ID).ToList();
    }
    return View(listapeliculas);
  }


}
