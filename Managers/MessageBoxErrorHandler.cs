using QM_ItemCreatorTool.Interfaces;
using System.Windows;

namespace QM_ItemCreatorTool.Managers
{
    public class MessageBoxErrorHandler : IErrorHandler
    {
        public void ThrowError(string title, Exception innerException)
        {
            MessageBox.Show(innerException.Message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ThrowInfo(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ThrowWarning(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}