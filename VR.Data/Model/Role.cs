using Microsoft.AspNetCore.Identity;
using System;

namespace VR.Data.Model
{
    public class Role : IdentityRole<Guid>
    {
        public static readonly string Ministro = "Ministro";
        public static readonly string Agente = "Agente";
        public static readonly string Supervisor = "Supervisor";
        public static readonly string Admin = "Administrador";
        public Role()
        {
        }
    }
}