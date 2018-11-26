namespace VendingMachine.Client
{
    using System.Windows.Forms;

    using VendingMachine.Common;

    /// <summary>
    /// This is an implementaiton of message bok. This uses the windows message box.
    /// </summary>
    public class WindowsMessageBox : IMessageBox
    {
        public void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}