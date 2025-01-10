using System.Windows;

namespace Spellweaver.Commands
{
    public class CustomCommands
    {
        private readonly static DelegateCommand add;
        static CustomCommands()
        {
            add = new DelegateCommand(ShowMessageBox);
        }
        static void ShowMessageBox(object? parameter)
        {
            MessageBox.Show("Hello!");
        }
        public static DelegateCommand AddCommand => add;
    }
}
