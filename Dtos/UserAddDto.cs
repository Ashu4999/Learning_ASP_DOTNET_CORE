using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning_Dotnet.Dtos
{
    public class UserAddDto
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Gender { get; set; } = String.Empty;
        public bool Active { get; set; }
    }
}