namespace QM_ItemCreatorTool.Interfaces
{
    public interface IMessageBoxHandler
    {
        bool ThrowWarningConfirmation(string title, string message);
    }
}