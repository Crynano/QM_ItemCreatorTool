using QM_ItemCreatorTool.Interfaces;
using System.Windows;

namespace QM_ItemCreatorTool.Managers
{
    public class BoxMessageManager : IMessageBoxHandler
    {
        public void ThrowInfo(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool ThrowWarningConfirmation(string title, string message)
        {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }
    }
}