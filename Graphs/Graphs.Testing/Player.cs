namespace Graphs.Testing
{
    public class Player
    {
        public int Stocks { get; set; }

        public int Money { get; set; }

        public int CarIntegrity { get; set; } // 0 - 100

        public int CarFuel { get; set; } // 0 - 100

        public override string ToString()
        {
            return $"Stocks: {this.Stocks}\r\n" +
                $"Money: {this.Money}\r\n" +
                $"Integrity: {this.CarIntegrity}\r\n" +
                $"Fuel: {this.CarFuel}";
        }
    }
}
