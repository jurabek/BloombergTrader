using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergTrader.Client.Model
{
    public class BloombergSymbol
    {
        public bool Selected {get; set;}

        public string Symbol { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string Type { get; set; }

        public string Industry { get; set; }

    }
}
