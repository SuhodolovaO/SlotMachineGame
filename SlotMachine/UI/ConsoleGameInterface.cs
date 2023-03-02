using SlotMachineGame.MachineLogic;

namespace SlotMachineGame.UI
{
    internal class ConsoleGameInterface : IGameInterface
    {
        private const int _defaultTopPosition = 11;
        private const int _defaultLeftPosition = 1;

        private const int _spinnersTopPosition = 6;
        private const int _spinnersLeftPosition = 3;

        private const int _lastResultTopPosition = 8;
        private const int _playingCreditsTopPosition = 9;
        private const int _totalCreditsTopPosition = 10;

        private SlotMachine _slotMachine { get; set; }
        private Dictionary<int, int> _spinnerPositions { get; set; }

        public void StartGame()
        {
            Init();
            RunGame();
        }

        private void Init()
        {
            _slotMachine = new SlotMachine();
            _slotMachine.Init();

            Console.WriteLine("S - Max Credits in the Slot Machine and Spin");
            Console.WriteLine("A - Add 1 credit to the Slot Machine");
            Console.WriteLine("D - Spin");
            Console.WriteLine("Q - Payout (Quit)");

            DrawSpinners(_slotMachine.Spinners);
            DrawTotalCredits();
            MoveToDefaultPosition();
        }

        private void RunGame()
        {
            while (true)
            {
                try
                {

                    var keyPressed = Console.ReadKey();

                    switch (keyPressed.Key)
                    {
                        case ConsoleKey.A:
                            _slotMachine.AddPlayingCredit();
                            DrawPlayingCredits();
                            break;
                        case ConsoleKey.S:
                            ClearPayout();
                            _slotMachine.SetMaxPlayingCredits();
                            DrawPlayingCredits();
                            _slotMachine.Play(RefreshSpinners);
                            DrawPayout();
                            DrawPlayingCredits();
                            DrawTotalCredits();
                            break;
                        case ConsoleKey.D:
                            ClearPayout();
                            _slotMachine.Play(RefreshSpinners);
                            DrawPayout();
                            DrawPlayingCredits();
                            DrawTotalCredits();
                            break;
                        case ConsoleKey.Q:
                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            break;
                    }
                }
                catch (Exception e)
                {
                    DrawErrorMessage(e.Message);
                }
            }
        }

        private void MoveToDefaultPosition()
        {
            Console.SetCursorPosition(_defaultLeftPosition, _defaultTopPosition);
            Console.Write("   ");
            Console.SetCursorPosition(_defaultLeftPosition, _defaultTopPosition);
        }

        private void DrawSpinners(IEnumerable<SpinnerData> spinners)
        {
            _spinnerPositions = new Dictionary<int, int>();
            Console.SetCursorPosition(_spinnersLeftPosition, _spinnersTopPosition);

            foreach (var spinner in spinners)
            {
                _spinnerPositions.Add(spinner.Id, Console.CursorLeft);
                Console.Write($"{spinner.CurrentSymbol} | ");
            }

            MoveToDefaultPosition();
        }

        private void RefreshSpinners(IEnumerable<SpinnerData> spinners)
        {
            foreach (var spinner in spinners)
            {
                var leftPosition = _spinnerPositions[spinner.Id];
                Console.SetCursorPosition(leftPosition, _spinnersTopPosition);
                Console.Write($"{spinner.CurrentSymbol} | ");
            }
            MoveToDefaultPosition();
        }

        private void ClearPayout()
        {
            Console.SetCursorPosition(_defaultLeftPosition, _lastResultTopPosition);
            Console.Write($"Your payout...           ");
            MoveToDefaultPosition();
        }

        private void DrawPayout()
        {
            Console.SetCursorPosition(_defaultLeftPosition, _lastResultTopPosition);
            Console.Write($"Your payout: {_slotMachine.LastPayout}     ");
            MoveToDefaultPosition();
        }

        private void DrawPlayingCredits()
        {
            Console.SetCursorPosition(_defaultLeftPosition, _playingCreditsTopPosition);
            Console.Write($"Playing credits: {_slotMachine.PlayingCredits}");
            MoveToDefaultPosition();
        }

        private void DrawTotalCredits()
        {
            Console.SetCursorPosition(_defaultLeftPosition, _totalCreditsTopPosition);
            Console.Write($"Total credits: {_slotMachine.TotalCredits}   ");
            MoveToDefaultPosition();
        }

        private void DrawErrorMessage(string errorMessage)
        {
            Console.SetCursorPosition(_defaultLeftPosition, _lastResultTopPosition);
            Console.Write(errorMessage);
            MoveToDefaultPosition();
        }
    }
}
