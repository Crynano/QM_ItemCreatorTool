using QM_ItemCreatorTool.Interfaces;
using System.Windows;

namespace QM_ItemCreatorTool.Managers
{
    public class ConfirmationManager : IMessageBoxHandler
    {
        public bool ThrowWarningConfirmation(string title, string message)
        {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }
    }
}