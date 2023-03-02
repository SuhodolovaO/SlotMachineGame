using SlotMachineGame.Exceptions;
using SlotMachineGame.MachineLogic.Models;

namespace SlotMachineGame.MachineLogic
{
    internal class SlotMachine
    {
        private readonly SlotsInitializer _slotsInitializer;

        private const int _startRotationsCount = 3;
        private const int _minRotationsRandomizerStep = 3;
        private const int _maxRotationsRandomizerStep = 7;

        private const int _startCredits = 100;
        private const int _maxPlayingCredits = 3;

        private List<ISpinner> _spinners = new List<ISpinner>();

        public IReadOnlyCollection<SpinnerData> Spinners =>
            _spinners.Select(s => SpinnerData.Create(s)).ToArray();
      
        public int TotalCredits { get; private set; }
        public int PlayingCredits { get; private set; }
        public int LastPayout { get; private set; }

        public SlotMachine()
        {
            _slotsInitializer = new SlotsInitializer();
        }

        public void Init()
        {
            _spinners = _slotsInitializer.GetSpinners();
            TotalCredits = _startCredits;
        }

        public void Play(Action<IEnumerable<SpinnerData>> refreshInterface)
        {
            if (PlayingCredits == 0)
            {
                throw new IncorrectCreditsPlayedException();
            }

            var rand = new Random();
            var rotationsCount = _startRotationsCount;

            foreach (var spinner in _spinners)
            {
                rotationsCount += rand.Next(_minRotationsRandomizerStep, _maxRotationsRandomizerStep);
                spinner.StartSpin(rotationsCount);
            }

            while (_spinners.Any(x => x.IsSpinning))
            {
                _spinners.ForEach(x => x.Rotate());
                refreshInterface(Spinners);
                Thread.Sleep(250);
            }

            var spinnersSymbols = _spinners.Select(x => x.CurrentSymbol);
            LastPayout = PayoutCalculator.Calculate(PlayingCredits, spinnersSymbols);
            TotalCredits += LastPayout - PlayingCredits;
            PlayingCredits = 0;
        }

        public void AddPlayingCredit()
        {
            if (TotalCredits - PlayingCredits > 0)
            {
                PlayingCredits += PlayingCredits < _maxPlayingCredits ? 1 : 0;
            }
        }

        public void SetMaxPlayingCredits()
        {
            PlayingCredits = _maxPlayingCredits < TotalCredits ? _maxPlayingCredits : TotalCredits;
        }
    }
}
