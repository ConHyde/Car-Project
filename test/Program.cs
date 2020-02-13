using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var menuSelected = InitialiseMenu();

            OpenMenu(menuSelected);

            Car fordFiesta = new Car("Ford Fiesta", 4, 100, 40, 8.0, true, "REGNO", 10000, 12500,"White", 15.5, 3, true);
            Car fordMondeo = new Car("Ford Mondeo", 4, 110, 50, 7.5, true, "REGNO", 10000, 15000, "Blue", 13.5, 5, true);
            Car fordRanger = new Car("Ford Ranger", 6, 95, 0, 11, true, "REGNO", 10000, 20000, "Black", 0, 4, false);

            var cars = new List<Car>();

            cars.Add(fordFiesta);
            cars.Add(fordMondeo);
            cars.Add(fordRanger);

            //SpeedCamera(cars, 40);
            //Race(cars);
        }

        public static int InitialiseMenu()
        {
            bool menuChosen = false;
            int option = 0;

            ClearConsole();
            Console.WriteLine("Please type the number for the option you require!");

            Dictionary<int, string> optionsDic = new Dictionary<int, string>();

            optionsDic.Add(1, "Car Valuation");
            optionsDic.Add(2, "Car Builder");

            foreach (KeyValuePair<int, string> kvp in optionsDic)
            {
                Console.WriteLine("{0}. {1}", kvp.Key, kvp.Value);
            }

            while (!menuChosen)
            {

                if (Int32.TryParse(Console.ReadLine(), out int result) && optionsDic.ContainsKey(result))
                {
                    option = result;
                    menuChosen = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid menu number!");
                }
            }
            return option;
        }

        public static void OpenMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    InitialiseValuation();
                    break;
                case 2:
                    InitialiseCarBuilder();
                    break;
                default:
                    ClearConsole(); InitialiseMenu();
                    break;
            }
        }


        public static void InitialiseValuation()
        {
            Car car = new Car();

            ClearConsole();
            Console.WriteLine("Welcome to your free car valuation!");

            Console.WriteLine("Please enter your registration!");
            car.Registration = Console.ReadLine();

            Console.WriteLine("Please enter your Mileage!");
            car.Mileage = Int32.Parse(Console.ReadLine());
        }

        public static void InitialiseCarBuilder()
        {
            Car car = new Car();
        }


        public static void PrepareCars(List<Car> cars)
        {
            foreach (Car car in cars)
            {
                car.UpdateReadyToMove();
            }
        }

        public static void ClearConsole()
        {
            Console.Clear();

            Console.WriteLine("------------------------------");
            Console.WriteLine("       The Car Project!");
            Console.WriteLine("------------------------------");
        }

        public static void Race(List<Car> cars)
        {
            PrepareCars(cars);

            bool raceComplete = false;
            bool carsReady = true;

                foreach (Car car in cars)
                {
                    if (!car.ReadyToMove)
                    {
                        carsReady = false;
                        break;
                    }

                }

                if (!carsReady)
                {
                    Console.WriteLine("The cars are not ready! Checking with the drivers...");

                    foreach (Car car in cars)
                    {
                        if (!car.DoorsClosed)
                        {
                            car.CloseDoors();

                            Console.WriteLine("Closing doors on the " + car.Make + "...");

                            car.UpdateReadyToMove();
                        }
                    }

                }

            while (!raceComplete)
            {
                string carList = null;

                cars.ForEach(c => carList += c.Make + ", ");

                Console.WriteLine("The race is on between the " + carList);
                Console.WriteLine("----------------------");

                cars.OrderBy(c => c.TimeToSixty);



                Console.WriteLine("The winner of the race is " + cars[0].Make);

                raceComplete = true;
            }

        }

        public static void SpeedCamera(List<Car> cars, int speedLimit)
        {
            var speedingCars = new List<Car>();

            foreach(Car car in cars)
            {
                if(car.IsSpeeding(car.CurrentSpeed, speedLimit))
                {
                    speedingCars.Add(car);
                }

            }

            if (speedingCars.Count > 0)
            {
                foreach (Car car in speedingCars)
                {
                    Console.WriteLine("The " + car.Make + " was caught speeding!");
                }
            }

        }
    }

    public sealed class Car : Vehicle
    {

        public Car() { }

        public Car(string make, int wheels, double topSpeed, double currentSpeed, double timeToSixty,
                        bool readyToMove, string registration, int mileage, double value, string colour)
                : base(make, wheels, topSpeed, currentSpeed, timeToSixty, readyToMove, registration, mileage, value, colour)
        {
          
        }

        public Car(string make, int wheels, double topSpeed, double currentSpeed, double timeToSixty, bool readyToMove,
                            string registration, int mileage, double value, string colour, double spoilerHeight, int doors, bool doorsClosed)
                : this(make, wheels, topSpeed, currentSpeed, timeToSixty, readyToMove, registration, mileage, value, colour)
        {
            SpoilerHeight = spoilerHeight;
            Doors = doors;
            DoorsClosed = doorsClosed;

            UpdateReadyToMove();
        }

        public double SpoilerHeight { get; set; }
        public double Doors { get; set; }
        public bool DoorsClosed { get; set; }

        public void OpenDoors()
        {
            DoorsClosed = false;
        }

        public void CloseDoors()
        {
            DoorsClosed = true;
        }

        public override void UpdateReadyToMove()
        {
            ReadyToMove = DoorsClosed ? true : false;
        }
    }

    public abstract class Vehicle
    {
        public string Make { get; set; }
        public Int64 Wheels { get; set; }
        public double TopSpeed { get; set; }
        public double CurrentSpeed { get; set; }
        public double TimeToSixty { get; set; }
        public bool ReadyToMove { get; set; }
        public string Registration {get; set;}
        public int Mileage { get; set; }
        public double Value { get; set; }
        public string Colour { get; set; }

        public Vehicle() { }

        public Vehicle(string make, int wheels, double topspeed, double currentSpeed,
                        double timeToSixty, bool readyToMove, string registration, int mileage, double value, string colour)
        {
            Make = make;
            Wheels = wheels;
            TopSpeed = topspeed;
            CurrentSpeed = currentSpeed;
            TimeToSixty = timeToSixty;
            ReadyToMove = readyToMove;
            Registration = registration;
            Mileage = mileage;
            Value = value;
            Colour = colour;


        }
        // Overload Constructor To Just Get Valuation
        public Vehicle(string registration, int mileage)
        {
            Registration = registration;
            Mileage = mileage;
        }


        public bool IsSpeeding(double speed, double speedLimit)
        {
            bool result = false;

            if (speed > ((speedLimit * 1.1) + 2))
            {
                result = true;
            }

            return result;

        }

        public void Setpeed(double newSpeed)
        {
            CurrentSpeed = newSpeed;
        }

        public bool CheckReadyToMove()
        {
            return ReadyToMove;
        }

        public abstract void UpdateReadyToMove();
    }
}
 