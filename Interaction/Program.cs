using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Interaction
{
    class Program
    {
        

        private static void Main(string[] args)
        {
            InteractionRoom();
        }


        private static void InteractionRoom()
        {
            while (true)
            {

                Console.WriteLine("<-------------------------------->\n" +
                                  "Hello.\n" +
                                  "You are standing in the road.\n" +
                                  "You see vagrant.\n" +
                                  "What wold you do?\n" +
                                  "Press 1 to talk to vagrant\n" +
                                  "Press 2 to explore the surroundings\n" +
                                  "<-------------------------------->");
                
                var _room = new Room();
                _room.Init();
                string input = Console.ReadLine();
                var regex = new Regex(@"^\d{1,2}$");

                if (!regex.IsMatch(input ?? throw new InvalidOperationException()))
                {
                    Console.WriteLine("Incorrect input!");
                    continue;
                }

                var correctInput = int.Parse(input);
                switch (correctInput)
                {
                    case 1:
                        _room.CurrentRoad.TalkToVagrant();
                        continue;
                    case 2:
                        _room.CurrentRoad.ExploreTheSurroundings();
                        continue;
                }

                break;
            }
        }
    }

    public class Person
    {
        public List<Coin> PersonCoins = new List<Coin>();
        public List<Medallion> PersonMedallions = new List<Medallion>();
    }

    public class Room
    {

        private static Person _person;
        private static Vagrant _vagrant;
        private static string _descriptionOfEnvirement = "Description";
        public Road CurrentRoad;

        public void Init()
        {
            _person ??= new Person();
            _vagrant ??= new Vagrant();
            CurrentRoad = new Road(_descriptionOfEnvirement, _person, _vagrant);
        }
    }

    public class Road
    {
        public Road(string descriptionOfEnvironment, Person person, Vagrant vagrant)
        {
            _descriptionOfEnvironment = descriptionOfEnvironment;
            _person = person;
            _vagrant = vagrant;
        }

        private readonly Vagrant _vagrant;
        private readonly Person _person;
        private const int COINS_COUNT = 50;
        private readonly string _descriptionOfEnvironment;


        public void TalkToVagrant()
        {
            if (!_person.PersonCoins.Any())
            {
                for (int i = 0; i < COINS_COUNT; i++)
                {
                    var coin = _vagrant.VagrantCoins.First();
                    _vagrant.VagrantCoins.Remove(coin);
                    _person.PersonCoins.Add(coin);
                }

                var medallion = _vagrant.VagrantMedallions.First();
                _vagrant.VagrantMedallions.Remove(medallion);
                _person.PersonMedallions.Add(medallion);
                Console.WriteLine("<-------------------------------->\n" +
                                  "You receive " + COINS_COUNT + " coins, and medallion\n" +
                                  "<-------------------------------->");
            }
            else
            {
                Console.WriteLine("Nothing happen");
            }
        }

        public void ExploreTheSurroundings()
        {
            Console.WriteLine(_descriptionOfEnvironment);
        }
    }

    public class Vagrant
    {
        public Vagrant()
        {
            for (int i = 0; i < 999; i++)
            {
                var coin = new Coin();
                var medallion = new Medallion();
                VagrantCoins.Add(coin);
                VagrantMedallions.Add(medallion);
            }
        }
        public List<Coin> VagrantCoins = new List<Coin>();
        public List<Medallion> VagrantMedallions = new List<Medallion>();
        public bool IsCommunicated;

    }

    public class Medallion
    {
    }

    public class Coin
    {
    }
}