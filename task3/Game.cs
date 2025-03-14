using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public class Game
    {
        private readonly DiceManager diceManager;
        private readonly IPlayer bot;
        private readonly IPlayer human;

        public Game(string[] dice)
        {
            diceManager = new(dice);
            bot = new BotPlayer(diceManager);
            human = new HumanPlayer(diceManager);           
        }      

        public  void Start()
        {
            (int botChoice, int humanChoice) = ChooseFirstPlayer();
            if (botChoice != humanChoice)
            {
                 bot.ChooseDice(true);
                 human.ChooseDice(false);
                 bot.ThrowDice(human);
                 human.ThrowDice(bot);
            }
            else
            {
                human.ChooseDice(true);
                bot.ChooseDice(false);
                human.ThrowDice(bot);
                bot.ThrowDice(human);
            }
            Console.WriteLine(bot.Score < human.Score ? $"You win ({bot.Score} < {human.Score})!" : bot.Score > human.Score ? $"Bot win ({bot.Score} > {human.Score})!"
                : $"Draw ({bot.Score} == {human.Score})");
        }
       
        private (int, int) ChooseFirstPlayer()
        {
            Console.WriteLine("Let's determine who makes the first move.");
            (int botChoice, string SecretKeyText) = bot.MakeChoice(2);
            Console.WriteLine("Try to guess my selection.");
            (int humanChoice, _) = human.MakeChoice(2);
            Console.WriteLine($"My selection: {botChoice} (KEY={SecretKeyText})");
            return (botChoice, humanChoice);
        }
    }
}
