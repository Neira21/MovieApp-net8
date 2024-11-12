using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MovieAppWeb.Models;

public class Pelicula
{
    public int ID { get; set; }

    [Required(ErrorMessage = "Debe ingresar un Titulo")]
    public string Titulo { get; set; } = "";

    [Required(ErrorMessage = "Ingrese un año")]
    public int? Año { get; set; }

    [Required(ErrorMessage = "Especifique el genero")]
    public string Genero { get; set; } = "";

    [Required(ErrorMessage = "Debe ingresar una sinopsis")]
    public string Sinopsis { get; set; } = "";
    public string Imagen { get; set; } = "";
    public string Trailer { get; set; } = "";

    // Hacer que la propiedad Usuario sea opcional
    public Usuario? Usuario { get; set; }
    public int UsuarioId { get; set; }

    public List<Reseña> Reseñas { get; set; } = [];
}
