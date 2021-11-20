using System.Collections.Generic;
using System.Linq;

namespace Graphs.Testing
{
    public class Town
    {
        public string Name { get; set; }

        public bool HasGasStation { get; set; }

        public int FuelPrice { get; set; }

        public bool HasCarService { get; set; }

        public int ServicePrice { get; set; }

        public bool HasBuyers { get; set; }

        public int StocksPriceBuy { get; set; }

        public bool HasSellers { get; set; }

        public int StocksPriceSell { get; set; }

        public List<TownConnection> Connections { get; set; }

        public override string ToString()
        {
            string gasStationPriceFormat = this.HasGasStation ? $" Fuel Price: ${this.FuelPrice}" : "";
            string carServicePriceFormat = this.HasCarService ? $" Service Price: ${this.ServicePrice}" : "";
            string buyStocksPriceFormat = this.HasBuyers ? $" Buying at: ${this.StocksPriceBuy}" : "";
            string sellStocksPriceFormat = this.HasSellers ? $" Sell at: ${this.StocksPriceSell}" : "";

            return $"Town: {this.Name}\r\n" +
                   $"Gas Station: {this.HasGasStation}{gasStationPriceFormat}\r\n" +
                   $"Car Service: {this.HasCarService}{carServicePriceFormat}\r\n" +
                   $"Buyers: {this.HasBuyers}{buyStocksPriceFormat}\r\n" +
                   $"Sellers: {this.HasSellers}{sellStocksPriceFormat}\r\n" +
                   $"Connected Towns: {string.Join(", ", this.Connections.Select(connection => connection.Destination.Name + $" ({connection.Distance})"))}";
        }
    }
}
