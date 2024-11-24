using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieAppWeb.Models;

namespace MovieAppWeb.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;

  public HomeController(ILogger<HomeController> logger)
  {
    _logger = logger;
  }

  public class Numero{
    public int id {get; set;}
    public int numero {get; set;}

    public Numero(int id, int numero){
      this.id = id;
      this.numero = numero;
    }
  }

  public IActionResult Index()
  {

    // lista de numeros 
    List<int> numeros = new List<int>();

    //for y llenar lista vacia llamada _numeros
    
    for (int i= 0; i<100; i++){
      numeros.Add(i+1);
    }

    //metodo para filtrar numeros pares usando linq Método encadenado
    List<int> numerosPares = numeros.Where(x => x%2 == 0).ToList();

    // método para filtrar números pares usando linq Método de consulta
    var _numerosPares = from n in numeros where n % 2 == 0 select n;
    
    List<List<string>> lista_anidada = new List<List<string>>();
    List<string> listaFruta = new List<string>();

    listaFruta.Add("Manzana");
    listaFruta.Add("Pera");
    listaFruta.Add("Uva");

    List<string> listaVerduras = new List<string>();

    listaVerduras.Add("Lechuga");
    listaVerduras.Add("Tomate");
    listaVerduras.Add("Pepino");

    //añador juntando todo
    lista_anidada.Add(listaFruta);
    lista_anidada.Add(listaVerduras);

    //lista_anidada.Select(x => x.Select(y=>y));

    
    

    //return Ok(lista_anidada.SelectMany(x => x.Select(x => x)));

    return View();
  }

  public IActionResult Privacy()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
