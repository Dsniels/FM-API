using System;

namespace apisiase.Dto;

public class UsuarioDto
{
    public string Nombre {get; set;}
    public string PrimerApellido {get; set;}
    public string SegundoApellido {get; set;}
    public int Matricula {get; set;}
    public bool Admin {get; set; } 
    public bool Alumno {get; set; } 
    public string Email {get; set;}
    public string Id {get; set;}
    public string Token {get; set;}


}
