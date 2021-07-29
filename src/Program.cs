using System;
using System.Collections.Generic;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string characterName = GetCharacterName();
            Character playerCharacter = new Character(characterName);
            Console.WriteLine("Character name is " + playerCharacter.GetName());
            playerCharacter.GenerateStats();
            Console.WriteLine("Strength: " + playerCharacter.Strength);
            Console.WriteLine("HP: " + playerCharacter.CurrentHealthPoints);

            playerCharacter.Items.Add(new HealthPotion());
            playerCharacter.Items.Add(new HealthPotion());
            playerCharacter.Items.Add(new HealthPotion());

            playerCharacter.ConsumeEverything();

            Console.WriteLine("Difficulty is: " + GameSettings.Difficulty);
            
            Console.WriteLine("HP: " + playerCharacter.CurrentHealthPoints);
        }

        static string GetCharacterName()
        {
            Console.WriteLine("What is your name?");
            return Console.ReadLine();
        }
    }

    static class GameSettings
    {
        public static string Difficulty = "easy";
    }

    interface IConsumable
    {
        void Consume(Character thisCharacter);
        string Name();
    }
    class HealthPotion : IConsumable
    {
        public void Consume(Character thisCharacter)
        {
            thisCharacter.GainHp(50);
        }

        public string Name()
        {
            return "Health potion";
        }
    }
    class Character 
    {
        private string characterName = "Jack";
        int characterLevel = 1;
        bool isInGodMode = false;

        public List<IConsumable> Items = new List<IConsumable>();

        public int Strength;
        int MaxHealthPoints;
        public int CurrentHealthPoints;

        public bool AreYouDead()
        {
            return CurrentHealthPoints <= 0;
        }

        public void GainHp(int health)
        {
            CurrentHealthPoints = Math.Min(MaxHealthPoints, CurrentHealthPoints + health);
        }
        public string GetName()
        {
            return characterName;
        }

        public void ConsumeEverything()
        {
            foreach(IConsumable item in Items) 
            {
                item.Consume(this);
            }
            Items.Clear();
        }

        public void GenerateStats()
        {
            var rand = new Random();
            Strength = rand.Next(10, 20);
            MaxHealthPoints = rand.Next(500, 1000);
            CurrentHealthPoints = 1;
        }

        public Character(string name)
        {
            characterName = name;
        }
    }
}
