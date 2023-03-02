namespace SlotMachineGame.Exceptions
{
    internal class IncorrectCreditsPlayedException : Exception
    {
        public IncorrectCreditsPlayedException()
            :base("Incorrect credits played!")
        {
        }
    }
}
