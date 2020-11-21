using ExtendedModelProperties;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Pulsar
{
    public class PulsarAssets
    {
        private ListView _modelsListView;
        public ListView ModelsListView 
        { 
            get
            {
                return _modelsListView;
            }
            set
            {
                _modelsListView = value;
            }
        }

        private ListView _materialsListView;
        public ListView MaterialsListView 
        {
            get
            {
                return _materialsListView;
            }
            set
            {
                _materialsListView = value;
            }
        }

        private ListView _animationsListView;
        public ListView AnimationsListView 
        { 
            get
            {
                return _animationsListView;
            }
            set
            {
                _animationsListView = value;
            }
        }

        private ListView _texturesListView;
        public ListView TexturesListView 
        {
            get
            {
                return _texturesListView;
            }
            set
            {
                _texturesListView = value;
            }
        }

        private ListView _fontsListView;
        public ListView FontsListView 
        {
            get
            {
                return _fontsListView;
            }
            set
            {
                _fontsListView = value;
            }
        }

        private FileSystemWatcher _assetsWatcher;
        public FileSystemWatcher AssetsWatcher 
        { 
            get
            {
                return _assetsWatcher;
            }
            set
            {
                _assetsWatcher = value;
            }
        }

        public PulsarAssets(ListView modelsListView, ListView materialsListView, ListView animationsListView, ListView fontsListView, ListView texturesListView, FileSystemWatcher assetsWatcher)
        {
            _modelsListView = modelsListView;
            _materialsListView = materialsListView;
            _animationsListView = animationsListView;
            _fontsListView = fontsListView;
            _texturesListView = texturesListView;
            _assetsWatcher = assetsWatcher;

            _modelsListView.MouseDown += ModelsListView_MouseDown;
            _materialsListView.MouseDown += MaterialsListView_MouseDown;
        }

        private void MaterialsListView_MouseDown(object sender, MouseEventArgs e)
        {
            //what have we clicked on, if anything
            ListViewItem listViewItem = _materialsListView.GetItemAt(e.X, e.Y);
            if (listViewItem != null)
            {
                DragDropAsset dropAsset = new DragDropAsset
                {
                    Type = DragDropAsset.AssetType.Material,
                    FileName = (string)listViewItem.Tag
                };
                DataObject data = new DataObject(dropAsset);

                _materialsListView.DoDragDrop(data, DragDropEffects.Copy);
            }
        }

        private void ModelsListView_MouseDown(object sender, MouseEventArgs e)
        {
            //what have we clicked on, if anything
            ListViewItem listViewItem = _modelsListView.GetItemAt(e.X, e.Y);
            if (listViewItem != null)
            {
                DragDropAsset dropAsset = new DragDropAsset
                {
                    Type = DragDropAsset.AssetType.Model,
                    FileName = (string)listViewItem.Tag
                };
                DataObject data = new DataObject(dropAsset);

                _modelsListView.DoDragDrop(data, DragDropEffects.Copy);
            }
        }

        public void LoadAssets()
        {
            //Cycle through each of the folders, find the corresponding ListItem for folder and add subitems
            //to it that are the files
            //the folder path is in the watcher

            var assetsPath = _assetsWatcher.Path;
            var root = new DirectoryInfo(assetsPath);

            if (root != null)
            {
                var directories = root.EnumerateDirectories();
                if (directories != null)
                {
                    if (directories.Count() > 0)
                    {
                        foreach (DirectoryInfo directory in directories)
                        {
                            if (directory != null)
                            {
                                ProcessFilesForDirectory(directory);
                            }
                        }
                    }
                }
            }
        }

        private void ProcessFilesForDirectory(DirectoryInfo directory)
        {

            ListView listView = GetListViewOfDirectory(directory.Name, out int listViewDirectoryImageIndex);

            if (listView != null)
            {
                //now find the files in the directory
                var files = directory.EnumerateFiles();
                if (files != null)
                {
                    if (files.Count() > 0)
                    {
                        foreach (FileInfo file in files)
                        {
                            //add it to the LIstViewItem subItems collection
                            var listViewItem = listView.Items.Add(file.Name);
                            if (listViewItem != null)
                            {
                                if (listViewDirectoryImageIndex > 0)
                                    listViewItem.ImageIndex = listViewDirectoryImageIndex;
                                listViewItem.Tag = file.Name;
                            }
                        }
                    }
                }
            }
        }

        private ListView GetListViewOfDirectory(string directoryName, out int listViewDirectoryImageIndex)
        {
            ListView listView = null;
            listViewDirectoryImageIndex = -1;

            switch (directoryName)
            {
                case "Animations":
                    listView = _animationsListView;
                    listViewDirectoryImageIndex = 1;
                    break;
                case "Fonts":
                    listView = _fontsListView;
                    listViewDirectoryImageIndex = 2;
                    break;
                case "Materials":
                    listView = _materialsListView;
                    listViewDirectoryImageIndex = 3;
                    break;
                case "Models":
                    listView = _modelsListView;
                    listViewDirectoryImageIndex = 4;
                    break;
                case "Textures":
                    listView = _texturesListView;
                    listViewDirectoryImageIndex = 5;
                    break;
            }
            return listView;
        }

        private void AssetWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            //this waits on changes anywhere in the Assets folder - atm for testing this is just the project assets folder

        }
    }
}
