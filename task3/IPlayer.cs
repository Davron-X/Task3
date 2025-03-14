using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public interface IPlayer
    {
        public void ChooseDice(bool isFirst);
        public void ThrowDice(IPlayer player);
        public (int,string) MakeChoice(int ceil,bool isPrint=true);
        public Dice Dice { get; }
        public int Score { get; }
    }
}
