using SlotMachineGame.MachineLogic.Models;

namespace SlotMachineGame.MachineLogic
{
    internal class SlotsInitializer
    {
        private readonly int _spinnersCount = 3;

        public List<ISpinner> GetSpinners()
        {
            var result = new List<ISpinner>();

            for (var i = 0; i < _spinnersCount; i++)
            {
                result.Add(InitNewSpinner(i));
            }

            return result;
        }

        private Spinner InitNewSpinner(int id)
        {
            var initString = "7322111C";
            var randomString = "BBBBBBBBBBBBBBBBB".ToCharArray();

            var rand = new Random();
            int index = 1;

            var randomArray = initString.OrderBy(s => rand.Next(initString.Length)).ToArray();

            foreach (var symbol in randomArray)
            {
                randomString[index] = symbol;
                index += 2;
            }

            return new Spinner(id, new string(randomString));
        }
    }
}
