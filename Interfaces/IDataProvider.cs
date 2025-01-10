namespace QM_ItemCreatorTool.Interfaces
{
    public interface IDataProvider<T> where T : class
    {
        IEnumerable<T> GetData();
    }
}