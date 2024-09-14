using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning_Dotnet.Models
{
    public class UserJobInfo
    {
        public int UserId { get; set; }
        public string JobTitle { get; set; } = String.Empty;
        public string Department { get; set; } = String.Empty;
    }
}