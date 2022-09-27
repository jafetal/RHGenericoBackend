using System;
using System.Collections.Generic;

namespace SistemaGenericoRH.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string Email { get; set; } = null!;
        public string User1 { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool Status { get; set; }
        public string Gender { get; set; } = null!;
        public DateTime CreateDate { get; set; }
    }
}
