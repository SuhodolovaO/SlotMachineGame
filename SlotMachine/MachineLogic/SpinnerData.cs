using SlotMachineGame.MachineLogic.Models;

namespace SlotMachineGame.MachineLogic
{
    internal record SpinnerData(int Id, char CurrentSymbol, bool IsSpinning)
    {
        public static SpinnerData Create(ISpinner spinner)
        {
            return new SpinnerData(spinner.Id, spinner.CurrentSymbol, spinner.IsSpinning);
        }
    }
}
