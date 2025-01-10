using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QM_ItemCreatorTool.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Property failed was: " + propertyName);
            }
        }

        public virtual Task LoadAsync() => Task.CompletedTask;
    }

    public class ViewModelBase<T> : ViewModelBase where T : class
    {
        protected readonly T _model;
        public ViewModelBase(T model)
        {
            _model = model;
        }
        public T GetModel => _model;
    }
}