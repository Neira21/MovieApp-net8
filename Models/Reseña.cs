using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MovieAppWeb.Models;

public class Reseña
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Ingresa un comentario")]
    public string Comentario { get; set; }
    [Required(ErrorMessage = "Ingresa una calificacion")]
    public int? Estrellas { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime Fecha { get; set; }

    [ValidateNever]
    public Pelicula Pelicula { get; set; }
    public int PeliculaId { get; set; }

    [ValidateNever]
    public Usuario Usuario { get; set; }
    public int UsuarioId { get; set; }
}
