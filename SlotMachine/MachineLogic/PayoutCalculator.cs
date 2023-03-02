using SlotMachineGame.Exceptions;

namespace SlotMachineGame.MachineLogic
{
    internal static class PayoutCalculator
    {
        public static int Calculate(int creditsPlayed, IEnumerable<char> spinnersSymbols)
        {
            if (spinnersSymbols.Contains('C'))
            {
                return GetCherryPayout(creditsPlayed);
            }
            else if (spinnersSymbols.All(s => s == '7'))
            {
                return GetSevensPayout(creditsPlayed);
            }
            else if (spinnersSymbols.All(s => s == '3'))
            {
                return GetTripleBarPayout(creditsPlayed);
            }
            else if (spinnersSymbols.All(s => s == '2'))
            {
                return GetDoubleBarPayout(creditsPlayed);
            }
            else if (spinnersSymbols.All(s => s == '1'))
            {
                return GetBarPayout(creditsPlayed);
            }
            else if (spinnersSymbols.All(s => "123".Contains(s)))
            {
                return GetAnyBarPayout(creditsPlayed);
            }

            return 0;
        }

        private static int GetCherryPayout(int creditsPlayed)
        {
            return creditsPlayed switch
            {
                1 => 2,
                2 => 4,
                3 => 6,
                _ => throw new IncorrectCreditsPlayedException()
            };
        }

        private static int GetAnyBarPayout(int creditsPlayed)
        {
            return creditsPlayed switch
            {
                1 => 5,
                2 => 10,
                3 => 15,
                _ => throw new IncorrectCreditsPlayedException()
            };
        }

        private static int GetBarPayout(int creditsPlayed)
        {
            return creditsPlayed switch
            {
                1 => 25,
                2 => 50,
                3 => 75,
                _ => throw new IncorrectCreditsPlayedException()
            };
        }

        private static int GetDoubleBarPayout(int creditsPlayed)
        {
            return creditsPlayed switch
            {
                1 => 50,
                2 => 100,
                3 => 150,
                _ => throw new IncorrectCreditsPlayedException()
            };
        }

        private static int GetTripleBarPayout(int creditsPlayed)
        {
            return creditsPlayed switch
            {
                1 => 100,
                2 => 200,
                3 => 300,
                _ => throw new IncorrectCreditsPlayedException()
            };
        }

        private static int GetSevensPayout(int creditsPlayed)
        {
            return creditsPlayed switch
            {
                1 => 300,
                2 => 600,
                3 => 1500,
                _ => throw new IncorrectCreditsPlayedException()
            };
        }
    }
}
