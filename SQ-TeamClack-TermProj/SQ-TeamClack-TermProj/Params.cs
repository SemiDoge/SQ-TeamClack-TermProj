using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQ_TeamClack_TermProject
{
    public struct contractParams
    {
        public string clientName { get; set; }
        public int jobType { get; set; }
        public int quantity { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }

        public int vanType { get; set; }

    }
}
