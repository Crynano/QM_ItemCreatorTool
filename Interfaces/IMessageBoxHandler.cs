namespace QM_ItemCreatorTool.Interfaces
{
    public interface IMessageBoxHandler
    {
        bool ThrowWarningConfirmation(string title, string message);
        void ThrowInfo(string title, string message);
    }
}