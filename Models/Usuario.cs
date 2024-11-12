using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieAppWeb.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Username { get; set; }

    [Required(ErrorMessage = "Por favor, ingrese una contraseña")]
    public string Pwd { get; set; }
    [Required(ErrorMessage = "Por favor, ingrese el número de documento")]
    [Display(Name = "Número de documento")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "Debe ingresar un número minimo de 8 dígitos")]
    [RegularExpression(@"^([0-9]{8})$", ErrorMessage = "Ingresa un numero valido")]
    public string Dni { get; set; }
    [Required(ErrorMessage = "Por favor, Ingrese el nombre")]
    [StringLength(50, MinimumLength = 2)]
    public string Nombre { get; set; }
    [Required(ErrorMessage = "Por favor, ingrese el apellido")]
    [StringLength(50, MinimumLength = 2)]
    public string Apellido { get; set; }
    
    [Required(ErrorMessage = "Ingrese un email")]
    public string Email { get; set; }

    [ValidateNever]
    public List<Pelicula> Peliculas { get; set; }

    [ValidateNever]
    public List<Noticia> Noticias { get; set; }

    [ValidateNever]
    public List<Reseña> Reseñas { get; set; }
}
