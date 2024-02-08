﻿using System.Reflection.Metadata.Ecma335;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public ML.Rol Rol { get; set; }
        public List<object> Usuarios { get; set; }
    }
}