
namespace task3
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            try
            {
                Game game = new(args);
                game.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
