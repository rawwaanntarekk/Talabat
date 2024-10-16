using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Errors
{
    internal class ModelError
    {
        public required string Field { get; set; }
        public required IEnumerable<string> Errors { get; set; }
    }
}
