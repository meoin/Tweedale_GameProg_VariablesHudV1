using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

enum Weapons 
{
    Stick,
    Sword,
    Axe,
    Spear,
    Mace,
    Greathammer,
    Bow,
    BFG
}

namespace Tweedale_GameProg_VariablesHudV1
{
    internal class Program
    {
        static string name;
        static float health = 100;
        static float armor = 0;
        static Weapons weapon = Weapons.Stick;
        static int score = 0;

        static bool departed = false;
        static bool piano = false;
        static float days = 0;

        static Random rand = new Random();

        static void ShowHUD()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0,0}{1,30}{2,15}{3,20}{4,10}{5,10}", "Character: " + name, "Health: " + HealthStatus(health), "Armor: " + armor, "Weapon: " + weapon, "Day " + days, "Score: " + score);

            Console.ForegroundColor = ConsoleColor.White;
        }

        static void TakeDamage(int damage, bool armorMatters) 
        {
            if (armorMatters)
            {
                health -= Math.Max((damage - armor), 0);
            }
            else 
            {
                health -= damage;
            }
        }

        static void Heal(int healing) 
        {
            health = Math.Min(health + healing, 100);
        }

        static string HealthStatus(float hp) 
        {
            if (hp >= 100)
            {
                return "Perfect Health";
            }
            else if (hp > 75)
            {
                return "Healthy";
            }
            else if (hp > 50) 
            {
                return "Hurt";
            }
            else if (hp > 10)
            {
                return "Badly Hurt";
            }
            else
            {
                return "Brink of Death";
            }
        }

        static void AddScore(int pointsEarned) 
        {
            score += pointsEarned;
        }

        static void GetFinalScore() 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You died!!");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Score: {score}");

            float pianoMultiplier = 1;
            if (piano == true)
            {
                Console.WriteLine("Piano Multiplier: 1.5x");
                pianoMultiplier = 1.5f;
            }


            float daysMultiplier = 1 + (days / 20);
            Console.WriteLine($"Days Multiplier: x{daysMultiplier} ({days})");

            float finalScore = score * pianoMultiplier * daysMultiplier;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Final Score: {Math.Floor(finalScore)}");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nWould you like to play again?");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Enter 1 for YES and any other key for NO: ");
            var input = Console.ReadKey().KeyChar;
            Console.ForegroundColor = ConsoleColor.White;

            if (input == '1') 
            {
                BeginGame();
            }
        }

        static Weapons GetRandomWeapon() 
        {
            int weaponId = rand.Next(1, 7);
            
            Weapons selectedWeapon = (Weapons)weaponId;

            return selectedWeapon;
        }

        static void ChangeWeapon(Weapons newWeapon) 
        {
            weapon = newWeapon;
        }

        static void RandomDayEvent(double randomNumber)
        {
            if (randomNumber < 0.1)
            {
                Console.WriteLine("You found a pile of gold on the ground left from a previous traveller! Yoink!");
                Console.WriteLine("Score went up by 100!");
                AddScore(100);
            }
            else if (randomNumber < 0.3)
            {
                Console.WriteLine("Drats! A beast from the woods charged at you and managed to scratch you up.");
                Console.WriteLine("You took {0,0} damage but gained 25 score!", Math.Max((10 - armor), 0));
                TakeDamage(10, true);
                AddScore(25);
            }
            else if (randomNumber < 0.35)
            {
                Console.WriteLine("Oh baby, what is this I see? A little extra leather to covet thee?");
                Console.WriteLine("Advise your foes to flee, as your armor went up not by one but three!");
                Console.WriteLine("Actually, sorry, it just went by 1. I just wanted to keep rhyming.");
                armor += 1;
            }
            else if (randomNumber < 0.5)
            {
                Console.WriteLine("By Jove, a gang of bandits have ambushed you from the night.");
                Console.WriteLine("You managed to take them out and leave with their loot, but it wasn't easy.");
                Console.WriteLine("You took {0,0} damage and gained 150 score!", Math.Max((20 - armor), 0));
                TakeDamage(20, true);
                AddScore(150);
            }
            else if (randomNumber < 0.65)
            {
                Console.WriteLine("An uneventful night, but you feel worn down from your travels. Hunger takes its toll.");
                Console.WriteLine("Your health depletes by 5...");
                TakeDamage(5, false);
            }
            else if (randomNumber < 0.75)
            {
                Console.WriteLine("You manage to forage some food from the surrounding bushes, but it doesn't taste very good.");
                Console.WriteLine("Your health is restored by 10.");
                Heal(10);
            }
            else if (randomNumber < 0.8)
            {
                Console.WriteLine("By the Gods, you've stumbled upon an inn that's willing to give you shelter! You eat like an animal and sleep like a log.");
                Console.WriteLine("Your health is fully restored!");
                Heal(100);
            }
            else if (randomNumber < 0.99)
            {
                // Find a new weapon
                Weapons foundWeapon = GetRandomWeapon();

                Console.WriteLine($"On the ground you find a {foundWeapon} in perfectly good condition!");
                Console.WriteLine($"Would you like to exchange your {weapon} for the {foundWeapon}?");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Enter 1 for YES and any other key for NO: ");
                var input = Console.ReadKey().KeyChar;
                Console.ForegroundColor = ConsoleColor.White;

                if (input == '1')
                {
                    
                    Console.WriteLine($"\nYou leave behind your {weapon} to arm yourself with the powerful {foundWeapon} and carry on forth!");
                    ChangeWeapon(foundWeapon);
                }
                else 
                {
                    Console.WriteLine($"\nYou don't need this {foundWeapon} where you're going. You choose to leave it behind and stick with your existing {weapon}.");
                }
            }
            else
            {
                Console.WriteLine("A giant piano fell from the sky and landed on your head.");
                Console.WriteLine("You lost 100 health!!! Idiot!");
                TakeDamage(100, false);
                piano = true;
                if (health > 0)
                {
                    Console.Write("\nWait... you survived that??? You absolute legend. You open up the piano and stuffed inside you find ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("1000 GOLD!");
                    AddScore(1000);
                }
            }
        }

        static void BeginGame() 
        {
            Console.Clear();

            health = 100;
            armor = 0;
            weapon = Weapons.Stick;
            score = 0;
            days = 0;
            departed = false;
            piano = false;

            while (health > 0)
            {

                ShowHUD();

                days++;

                if (!departed)
                {
                    Console.WriteLine("Today is the day you set off on a journey of chance, fate and terror... Off with you!");
                    departed = true;
                }
                else
                {

                    RandomDayEvent(rand.NextDouble());

                }

                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();

            }

            GetFinalScore();
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter your name: ");
            Console.ForegroundColor = ConsoleColor.White;

            name = Console.ReadLine();

            BeginGame();
        }
    }
}


