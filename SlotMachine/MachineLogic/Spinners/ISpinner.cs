namespace SlotMachineGame.MachineLogic.Models
{
    internal interface ISpinner
    {
        public int Id { get; }
        public char CurrentSymbol { get; }
        public bool IsSpinning { get; }

        public void StartSpin(int RotationsCount);
        public void Rotate();
    }
}
