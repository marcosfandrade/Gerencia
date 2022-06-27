using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerencia.API.Entities
{
    public class LoginHistoric
    {
        public int Id { get; set; }
        public DateTime LoginTime { get; set; }
        public int UserId { get; set; }
    }
}