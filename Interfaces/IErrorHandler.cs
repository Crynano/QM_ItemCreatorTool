namespace QM_ItemCreatorTool.Interfaces
{
    public interface IErrorHandler
    {
        void ThrowInfo(string title, string message);
        void ThrowWarning(string title, string message);
        void ThrowError(string title, Exception innerException);
    }
}