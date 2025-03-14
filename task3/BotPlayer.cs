using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public class BotPlayer:IPlayer
    {
        private readonly DiceManager diceManager;

        public int Score { get;private set; }
        public Dice Dice { get; private set; }
        
        public BotPlayer(Dice dice, DiceManager diceManager)
        {
            Dice = dice;
            this.diceManager = diceManager;
        }
        public BotPlayer(DiceManager diceManager) :this(new Dice(), diceManager) { }
       
        public (int,string) MakeChoice(int ceil=6,bool IsPrint=true)
        {
            Console.WriteLine($"I selected a random value in the range 0..{ceil-1}");
            byte[] secretKey = SecretKeyGenerator.GenerateSecretKey();
            string SecretKeyText = SecretKeyGenerator.ConvertKeyToString(secretKey);
            int pcSelection = RandomGenerator.GenerateRandomNumber(ceil);
            string hmac = HmacGenerator.GenerateHmac(secretKey, pcSelection);
            Console.WriteLine($"(HMAC={hmac})");
            return (pcSelection, SecretKeyText);
        }
        public void ChooseDice(bool isFirst)
        {
            this.Dice = diceManager.Take(RandomGenerator.GenerateRandomNumber(diceManager.AvailableCount));
            if (isFirst)
                Console.WriteLine($"I make the first move and choose the [{this.Dice}] dice.");
            else
                Console.WriteLine($"I choose the [{this.Dice}] dice.");

        }
        public void ThrowDice(IPlayer human)
        {
            Console.WriteLine("It's time for my throw.");
            (int botChoice, string SecretKeyText) = this.MakeChoice(Dice.Modulo);
            int humanChoice = ChooseNumberModulo(human);
            Console.WriteLine($"My number is {botChoice} (KEY={SecretKeyText})");
            CalculateResult(botChoice, humanChoice);           
        }
        private void CalculateResult(int botChoice,int humanChoice)
        {
            int botMod = (botChoice + humanChoice) % Dice.Modulo;
            Console.WriteLine($"The result is {botChoice} + {humanChoice} = {botMod} (mod {Dice.Modulo}).");
            Console.WriteLine($"My throw is {this.Dice.Values[botMod]}.");
            Score = this.Dice.Values[botMod];
        }
        private int ChooseNumberModulo(IPlayer human)
        {
            Console.WriteLine($"Add your number modulo {Dice.Modulo}.");           
            (int humanChoice, _)= human.MakeChoice(Dice.Modulo);
            return humanChoice;
        }

    }
}
