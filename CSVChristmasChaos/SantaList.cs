using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVChristmasChaos
{
    [Serializable]
    public class SantaList
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gift { get; set; }
        public string Country { get; set; }
        public string ElfName { get; set; }
        public string NaughtyOrNice { get; set; }
    }
}
