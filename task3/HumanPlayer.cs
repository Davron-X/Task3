using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public class HumanPlayer : IPlayer
    {
        private readonly DiceManager diceManager;
        private readonly DiceProbabilityCalculator probabilityCalculator;
        private readonly DiceProbabilityTableRenderer tableRenderer;

        public int Score { get;private set; }
        public Dice Dice { get; private set; }

        public HumanPlayer(Dice dice, DiceManager diceManager)
        {
            Dice = dice;
            this.diceManager = diceManager;
            probabilityCalculator = new(diceManager);
            tableRenderer = new(probabilityCalculator);
        }

        public HumanPlayer(DiceManager diceConfiguration):this(new Dice(),diceConfiguration){ }

        public (int, string) MakeChoice(int ceil = 6,bool isPrint=true)
        {
            while (true)
            {
                if (isPrint)
                    PrintValidSelections(ceil);
                Console.Write("Your selection:");
                string? choice = Console.ReadLine();
                if (int.TryParse(choice, out int userChoice))
                {
                    if (userChoice >= ceil)
                        throw new ArgumentException("Invalid input data");
                    return (userChoice, null);
                }
                else
                {
                    if (choice?.ToUpper() == "X")
                    {
                        throw new GameExitException();
                    }
                    else if (choice == "?")
                    {
                        tableRenderer.Render();
                    }
                    else
                    {
                        throw new ArgumentException("Invalid input data");
                    }
                }
            }
        }

        private void PrintValidSelections(int ceil)
        {
            for (int i = 0; i < ceil; i++)
            {
                Console.WriteLine($"{i} - {i}");
            }
            Console.WriteLine("X - exit \n? - help ");
        }

        public void ChooseDice(bool isFirst)
        {
            if (isFirst)
                Console.WriteLine("You make the first move, choose your dice:");
            else
                Console.WriteLine("Choose your dice:");
            diceManager.PrintAllDice();
            (int humanChoice, _) = this.MakeChoice(diceManager.AvailableCount,isPrint:false);
            this.Dice = diceManager.Take(humanChoice);
            Console.WriteLine($"You choose the [{this.Dice}] dice.");
        }

        public void ThrowDice(IPlayer bot)
        {
            Console.WriteLine("It's time for your throw.");
            (int botChoice,string SecretKeyText) = bot.MakeChoice(6);
            int humanChoice = ChooseNumberModulo(this);
            Console.WriteLine($"My number is {botChoice} (KEY={SecretKeyText})");
            CalculateResult(botChoice, humanChoice);
        }
      
        private int ChooseNumberModulo(IPlayer human)
        {
            Console.WriteLine($"Add your number modulo {Dice.Modulo}.");           
            (int humanChoice, _) = human.MakeChoice(Dice.Modulo);
            return humanChoice;
        }

        private void CalculateResult(int botChoice, int humanChoice)
        {
            int humanMod = (botChoice + humanChoice) % Dice.Modulo;
            Console.WriteLine($"The result is {botChoice} + {humanChoice} = {humanMod} (mod {Dice.Modulo}).");
            Console.WriteLine($"You throw is {this.Dice.Values[humanMod]}.");
            Score = this.Dice.Values[humanMod];
        }
    }
}
