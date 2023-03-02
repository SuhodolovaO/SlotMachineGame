using SlotMachineGame.UI;

namespace SlotMachineGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new ConsoleGameInterface();
            game.StartGame();
        }
    }
}