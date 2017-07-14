using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergTrader.Client.GUI
{
    public class Settings : ISettings
    {
        public string ServerHost
        {
            get; set;
        }

        public int ServerPort
        {
            get; set;
        }
    }
}
