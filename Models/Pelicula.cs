using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MovieAppWeb.Models;

public class Pelicula
{
    public int ID { get; set; }

    [Required(ErrorMessage = "Debe ingresar un Titulo")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "Debe ingresar un director")]
    public string Director { get; set; }

    [Required(ErrorMessage = "Debe ingresar un pais")]
    public string Pais { get; set; }

    [Required(ErrorMessage = "Ingrese un año")]
    public int? Año { get; set; }

    [Required(ErrorMessage = "Especifique el genero")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "Ingrese la duracion")]
    public int? Duracion { get; set; }

    [Required(ErrorMessage = "Debe ingresar una clasificacion")]
    public string Clasificacion { get; set; }

    [Required(ErrorMessage = "Debe ingresar un idioma")]
    public string Idioma { get; set; }

    [Required(ErrorMessage = "Debe ingresar una sinopsis")]
    public string Sinopsis { get; set; }
    public string Imagen { get; set; }
    public string Trailer { get; set; }

    [ValidateNever]
    public Usuario Usuario { get; set; }
    public int UsuarioId { get; set; }

    [ValidateNever]
    public List<Reseña> Reseñas { get; set; }
}
