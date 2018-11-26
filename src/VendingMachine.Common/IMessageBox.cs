namespace VendingMachine.Common
{
    /// <summary>
    /// Interface that can be implemented to provide a display message service
    /// </summary>
    public interface IMessageBox
    {
        void DisplayMessage(string message);
    }
}