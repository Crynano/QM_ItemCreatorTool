using Microsoft.Win32;

namespace QM_ItemCreatorTool.Managers
{
    public static class FolderExplorerManager
    {
        public static string? GetPathToFolder(string title = "Mod folder selection")
        {
            var loadFolderDialog = new OpenFolderDialog()
            {
                Multiselect = false,
                Title = title,
                ValidateNames = true
            };
            if (loadFolderDialog.ShowDialog() == true)
            {
                return loadFolderDialog.FolderName;
            }
            return null;
        }

        public static string? GetPathToFile(string title, string filter = "Text files (*.txt)|*.txt|Json Files (*.json)|*.json")
        {
            var loadFolderDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Title = title,
                ValidateNames = true,
                Filter = filter
            };
            if (loadFolderDialog.ShowDialog() == true)
            {
                return loadFolderDialog.FileName;
            }
            return null;
        }
    }
}