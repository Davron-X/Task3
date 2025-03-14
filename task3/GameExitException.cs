using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public class GameExitException:Exception
    {
        public GameExitException(string message = "Game over") : base(message) { }
    }
}
