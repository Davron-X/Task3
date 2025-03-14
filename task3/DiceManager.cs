using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public class DiceManager
    {
        public List<Dice> DiceSets { get; private set; }

        public List<Dice> AvailableDice { get; private set; }

        public int AvailableCount => AvailableDice.Count;

        public DiceManager(string[] dice)
        {
            DiceSets = ParseDice(dice);
            AvailableDice = new(DiceSets);
        }

        public Dice Take(int index)
        {
            if (DiceSets.Count == 0)
                throw new InvalidOperationException("DiceCongiguration is empty.");           
            Dice dice = AvailableDice[index];
            if (!AvailableDice.Contains(dice))            
                throw new InvalidOperationException("This dice was taken");            
            AvailableDice.RemoveAt(index);
            return dice;
        }

        private List<Dice> ParseDice(string[] dice)
        {
            if (dice.Length<3)
            {
                throw new ArgumentException("there must be more than 2 dice");
            }
            List<Dice> parsedDice = new();
            foreach (var diceText in dice)
            {               
                parsedDice.Add(new Dice(diceText.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList()));
            }
            return parsedDice;
        }

        public void PrintAllDice()
        {
            for (int i = 0; i < AvailableCount; i++)
            {
                Console.WriteLine($"{i} - {AvailableDice[i]}");
            }
            Console.WriteLine("X - exit \n? - help");
        }      
    }
}
