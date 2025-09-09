using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweedale_GameProg_VariablesHudV1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name;
            float health = 100;
            float armor = 0;
            int score = 0;
            bool departed = false;
            var rand = new Random();
            double randomNumber;

            bool piano = false;
            float kills = 0;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("Enter your name: ");

            Console.ForegroundColor = ConsoleColor.White;

            name = Console.ReadLine();
            Console.Clear();

            while (health > 0) 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0,0}{1,20}{2,20}{3,20}", "Character: " + name, "Health: " + health + "/100", "Armor: " + armor, "Score: " + score);

                Console.ForegroundColor = ConsoleColor.White;

                if (!departed)
                {
                    Console.WriteLine("Today is the day you set off on a journey of chance, fate and terror... Off with you!");
                    departed = true;
                }
                else
                {
                    randomNumber = rand.NextDouble();
                    //Console.WriteLine(randomNumber);

                    if (randomNumber < 0.1)
                    {
                        Console.WriteLine("You found a pile of gold on the ground left from a previous traveller! Yoink!");
                        Console.WriteLine("Score went up by 100!");
                        score += 100;
                    }
                    else if (randomNumber < 0.3)
                    {
                        Console.WriteLine("Drats! A beast from the woods charged at you and managed to scratch you up.");
                        Console.WriteLine("You took {0,0} damage but gained 25 score!", Math.Max((10 - armor), 0));
                        health -= Math.Max((10 - armor), 0);
                        score += 25;
                        kills++;
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
                        health -= Math.Max((20 - armor), 0);
                        score += 150;
                        kills++;
                    }
                    else if (randomNumber < 0.75)
                    {
                        Console.WriteLine("An uneventful night, but you feel worn down from your travels. Hunger takes its toll.");
                        Console.WriteLine("Your health depletes by 5...");
                        health -= 5;
                    }
                    else if (randomNumber < 0.9)
                    {
                        Console.WriteLine("You manage to forage some food from the surrounding bushes, but it doesn't taste very good.");
                        Console.WriteLine("Your health is restored by 10.");
                        health += 10;
                    }
                    else if (randomNumber < 0.95)
                    {
                        Console.WriteLine("By the Gods, you've stumbled upon an inn that's willing to give you shelter! You eat like an animal and sleep like a log.");
                        Console.WriteLine("Your health is fully restored!");
                        health = 100;
                    }
                    else 
                    {
                        Console.WriteLine("A giant piano fell from the sky and landed on your head.");
                        Console.WriteLine("You lost 100 health!!! Idiot!");
                        health -= 100;
                        piano = true;
                        if (health > 0) 
                        {
                            Console.Write("\nWait... you survived that??? You absolute legend. You open up the piano and stuffed inside you find ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("1000 GOLD!");
                        }
                    }

                }

                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();

            }

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


            float killsMultiplier = 1 + (kills / 10);

            Console.WriteLine($"Kills Multiplier: x{killsMultiplier} ({kills})");

            float finalScore = score * pianoMultiplier * killsMultiplier;

            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine($"Final Score: {Math.Floor(finalScore)}");


            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
