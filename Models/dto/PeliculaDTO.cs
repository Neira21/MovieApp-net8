namespace MovieAppWeb.Models.dto;

public class PeliculaDTO
{
    public string Titulo { get; set; } = "";
    public int? Año { get; set; }
    public string Genero { get; set; } = "";
    public string Sinopsis { get; set; } = "";
    public IFormFile? ImagenFile { get; set; }
    public string Trailer { get; set; } = "";
}