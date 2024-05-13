using Microsoft.AspNetCore.Mvc;
 
namespace MovieAppWeb.Controllers;

  public class Quiz2Controller : Controller
  {
      public IActionResult Preguntas(){
          return View();
      }
  }
