using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Testing
{
    class Program
    {
        static string[] randomTownNames = {
                "Woodhaerst",
                "Aeredale",
                "Erith",
                "Harthwaite",
                "Peltragow",
                "Yellowseed",
                "Aquarine",
                "Lingmell",
                "Roselake",
                "Pitmerden",
                "Perthlochry",
                "Streatham",
                "Damerel",
                "Helmfirth",
                "Halivaara",
                "Mournstead",
                "Pathstow",
                "Quan Ma",
                "Dalmerlington",
                "Arcton"};

        static List<Town> graph = new List<Town>();

        static int currentTown;

        static int totalStocksSold = 0;

        static void GenerateRandomGraph()
        {
            Random dice = new Random();

            for (int i = 0; i < randomTownNames.Length; i++)
            {
                int gasStationRNG = dice.Next(0, 100);
                int carServiceRNG = dice.Next(0, 100);
                int stocksBuyRNG = dice.Next(0, 100);
                int stocksSellRNG = dice.Next(0, 100);

                bool hasGasStation = false;
                bool hasCarService = false;
                bool hasStocksBuy = false;
                bool hasStocksSell = false;
                int fuelPrice = 0;
                int servicePrice = 0;
                int stocksPriceBuy = 0;
                int stocksPriceSell = 0;

                if (gasStationRNG <= 50)
                {
                    hasGasStation = true;
                    fuelPrice = dice.Next(1, 25);
                }

                if (carServiceRNG <= 10)
                {
                    hasCarService = true;
                    servicePrice = dice.Next(1, 5);
                }

                if (stocksBuyRNG <= 40)
                {
                    hasStocksBuy = true;
                    stocksPriceBuy = dice.Next(1, 10);
                }

                if (stocksSellRNG >= 55)
                {
                    hasStocksSell = true;
                    stocksPriceSell = dice.Next(1, 10);
                }

                string townName = randomTownNames[i];

                graph.Add(new Town
                {
                    Name = townName,
                    HasBuyers = hasStocksBuy,
                    StocksPriceBuy = stocksPriceBuy,
                    HasSellers = hasStocksSell,
                    StocksPriceSell = stocksPriceSell,
                    HasCarService = hasCarService,
                    ServicePrice = servicePrice,
                    HasGasStation = hasGasStation,
                    FuelPrice = fuelPrice,
                    Connections = new List<TownConnection>()
                });
            }

            for (int i = 0; i < graph.Count; i++)
            {
                HashSet<int> currentConnections = new HashSet<int>();

                int randomConnections = dice.Next(1, 5);

                for (int j = 0; j < randomConnections; j++)
                {
                    int randomTownConnectionIndex = dice.Next(0, graph.Count);

                    while(randomTownConnectionIndex == i 
                        || currentConnections.Contains(randomTownConnectionIndex))
                    {
                        randomTownConnectionIndex = dice.Next(0, graph.Count);
                    }

                    currentConnections.Add(randomTownConnectionIndex);

                    graph[i].Connections.Add(new TownConnection
                    {
                        Distance = dice.Next(1, 100),
                        Destination = graph[randomTownConnectionIndex]
                    });
                }
            }
        }

        static int GenerateRandomCity()
        {
            return new Random().Next(0, randomTownNames.Length);
        }

        static int TravelToTown(string townName)
        {
            for (int i = 0; i < graph.Count; i++)
            {
                if(graph[i].Name == townName)
                {
                    return i;
                }
            }

            return -1;
        }

        static void Main(string[] args)
        {
            // Travel from town to town
            // Have special towns that have a gas station in them
            // Have special towns that you can get stocks from
            // Have special towns that you can sell stocks at
            // Have special towns that have a car service in them
            // Final goal: SURVIVE
            GenerateRandomGraph();

            currentTown = GenerateRandomCity();

            Console.WriteLine("Your starting city is: " + graph[currentTown].Name);

            Player player = new Player
            {
                CarFuel = 100,
                CarIntegrity = 100,
                Money = 0,
                Stocks = 50,
            };

            string inputLine = string.Empty;

            while((inputLine = Console.ReadLine()) != "EXIT")
            {
                string[] inputParams = inputLine.Split();

                switch(inputParams[0])
                {
                    case "check":
                        string parameter = inputParams[1];
                        if (parameter == "town") {
                            Console.WriteLine(graph[currentTown].ToString());
                        } 
                        else if(parameter == "player")
                        {
                            Console.WriteLine(player.ToString());
                        }
                        break;
                    case "travel":
                        string townName = inputParams[1];
                        TownConnection suppousedTownConnection =
                            graph[currentTown]
                            .Connections
                            .FirstOrDefault(connection => connection.Destination.Name == townName);

                        if (suppousedTownConnection != null)
                        {
                            currentTown = TravelToTown(townName);
                            player.CarFuel -= suppousedTownConnection.Distance / 10;
                        }
                        else
                        {
                            Console.WriteLine($"No direct connection to town {townName}...");
                        }
                        break;
                    case "sell":
                        if(graph[currentTown].HasBuyers)
                        {
                            int stocksToSell = int.Parse(inputParams[1]);
                            if (stocksToSell > player.Stocks)
                            {
                                Console.WriteLine("You don't have that much stocks...");
                            }
                            else
                            {
                                player.Stocks -= stocksToSell;
                                player.Money += graph[currentTown].StocksPriceBuy * stocksToSell;

                                totalStocksSold += stocksToSell;

                                Console.WriteLine($"You have sold {stocksToSell} stocks for ${graph[currentTown].StocksPriceBuy * stocksToSell}!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No buyers in this town...");
                        }

                        break;
                    case "buy":
                        if (graph[currentTown].HasSellers)
                        {
                            int stocksToBuy = int.Parse(inputParams[1]);
                            if (stocksToBuy * graph[currentTown].StocksPriceSell > player.Money)
                            {
                                Console.WriteLine("You are too poor...");
                            }
                            else
                            {
                                player.Stocks += stocksToBuy;
                                player.Money -= graph[currentTown].StocksPriceSell * stocksToBuy;

                                Console.WriteLine($"You have bought {stocksToBuy} stocks for ${graph[currentTown].StocksPriceSell * stocksToBuy}!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No sellers in this town...");
                        }

                        break;
                    case "load":
                        if (graph[currentTown].HasGasStation)
                        {
                            int fuelToLoad = int.Parse(inputParams[1]);

                            int totalPrice = fuelToLoad * graph[currentTown].FuelPrice;

                            if(totalPrice > player.Money)
                            {
                                Console.WriteLine("You are too poor...");
                            }
                            else
                            {
                                player.CarFuel += fuelToLoad;
                                player.Money -= fuelToLoad * graph[currentTown].FuelPrice;

                                Console.WriteLine($"You have loaded {fuelToLoad} liters of fuel for ${fuelToLoad * graph[currentTown].FuelPrice}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No gas station in this town...");
                        }

                        break;
                    case "fix":
                        if (graph[currentTown].HasCarService)
                        {
                            // TODO: Fix car
                        }
                        else
                        {
                            Console.WriteLine("No car service in this town...");
                        }

                        break;
                    case "clear":
                        Console.Clear();
                        break;
                }
                // TODO: PLAY
            }

            Console.WriteLine($"High score: {totalStocksSold}");
        }
    }
}
