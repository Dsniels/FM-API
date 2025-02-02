﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Usuario : IdentityUser
    {
            
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public int Matricula {get; set;}
        public int HorarioID{get;set;}
        public bool Alumno {get; set;}
        public bool Admin {get; set;}
        

        
    }

    
}
