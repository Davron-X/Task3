using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public class DiceProbabilityTableRenderer
    {
        private readonly DiceProbabilityCalculator probabilityCalculator;

        public DiceProbabilityTableRenderer(DiceProbabilityCalculator probabilityCalculator)
        {
            this.probabilityCalculator = probabilityCalculator;
        }

        public  void Render()
        {
            List<List<string>> probabilities = probabilityCalculator.CalculateProbability();
            var table = new Table();
            table.Centered();
            table.Title("Probability of the win fоr the user:");
            foreach (var key in probabilities[0])
            {
                table.AddColumn($"[bold cyan]{key}[/]");
            }
            foreach (var row in probabilities.Skip(1))
            {
                table.AddRow(row.ToArray());
            }
            AnsiConsole.Write(table);
        }
    }
}
