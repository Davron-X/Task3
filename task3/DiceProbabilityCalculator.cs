using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public class DiceProbabilityCalculator
    {
        private readonly DiceManager diceConfiguration;

        public DiceProbabilityCalculator(DiceManager diceConfiguration)
        {
            this.diceConfiguration = diceConfiguration;
        }

        public List<List<string>> CalculateProbability()
        {
            List<List<string>> data = new();
            data.Add(new List<string>() { "User dice v" });
            for (int i = 0; i < diceConfiguration.DiceSets.Count; i++)
            {
                data[0].Add(diceConfiguration.DiceSets[i].ToString());
                data.Add(new List<string>() { diceConfiguration.DiceSets[i].ToString() });
                for (int j = 0; j < diceConfiguration.DiceSets.Count; j++)
                {   
                    double probality= CalculateWinningProbability(diceConfiguration.DiceSets[i], diceConfiguration.DiceSets[j]);
                    if(i==j)
                        data[i + 1].Add($"[green] - ({probality:F4})[/]");
                    else
                        data[i + 1].Add(probality.ToString("F4"));
                }
            }
            return data;
        }

        public  double CalculateWinningProbability(Dice diceA, Dice diceB)
        {
            double winA = 0;
            int totalCases = diceA.Count * diceB.Count;
            foreach (var x in diceA)
            {
                foreach (var y in diceB)
                {
                    if (x > y)
                        winA++;               
                }
            }
            return winA / totalCases;
        }
    }
}
