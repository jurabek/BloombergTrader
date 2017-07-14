using BloombergTrader.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergTrader.Client.ViewModels
{
    public interface IOneWayPriceViewModel
    {
        Direction Direction { get; }
        string BigFigures { get; }
        string Pips { get; }
        string TenthOfPip { get; }
    }

    public class OneWayPriceViewModel : IOneWayPriceViewModel
    {
        public OneWayPriceViewModel(Direction direction, string pips)
        {
            Direction = direction;
            Pips = pips;
        }

        public string BigFigures
        {
            get; private set;
        }

        public Direction Direction
        {
            get; private set;
        }

        public string Pips
        {
            get; private set;
        }

        public string TenthOfPip
        {
            get; private set;
        }
    }

}
