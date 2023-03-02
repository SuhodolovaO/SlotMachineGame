namespace SlotMachineGame.MachineLogic.Models
{
    internal class Spinner : ISpinner
    {
        public int Id { get; private set; }
        public bool IsSpinning { get; private set; }

        public char CurrentSymbol
        {
            get
            {
                if (_currentPosition < _symbols.Length)
                {
                    return _symbols[_currentPosition];
                }
                else
                {
                    // throw Exception?
                    return '\0';
                }
            }
        }

        private string _symbols { get; set; }
        private int _currentPosition { get; set; }

        private int _rotationsCount { get; set; }

        public Spinner(int id, string symbols)
        {
            Id = id;
            _symbols = symbols;

            var rand = new Random();
            _currentPosition = rand.Next(_symbols.Length - 1);
        }

        public void StartSpin(int RotationsCount)
        {
            IsSpinning = true;
            _rotationsCount = RotationsCount;
        }

        public void Rotate()
        {
            if (!IsSpinning)
                return;

            if (_currentPosition < _symbols.Length - 1)
            {
                _currentPosition++;
            }
            else
            {
                _currentPosition = 0;
            }

            _rotationsCount--;

            if (_rotationsCount == 0)
            {
                IsSpinning = false;
            }
        }
    }
}
