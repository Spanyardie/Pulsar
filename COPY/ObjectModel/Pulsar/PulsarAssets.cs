using ExtendedModelProperties;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Pulsar
{
    public partial class PulsarAssets : DockContent
    {
        public PulsarAssets()
        {
            InitializeComponent();
            Load += PulsarAssets_Load;
            modelsListView.MouseDown += ModelsListView_MouseDown;
            materialsListView.MouseDown += MaterialsListView_MouseDown;
        }

        private void MaterialsListView_MouseDown(object sender, MouseEventArgs e)
        {
            //what have we clicked on, if anything
            ListViewItem listViewItem = materialsListView.GetItemAt(e.X, e.Y);
            if (listViewItem != null)
            {
                DragDropAsset dropAsset = new DragDropAsset
                {
                    Type = DragDropAsset.AssetType.Material,
                    FileName = (string)listViewItem.Tag
                };
                DataObject data = new DataObject(dropAsset);

                materialsListView.DoDragDrop(data, DragDropEffects.Copy);
            }
        }

        private void ModelsListView_MouseDown(object sender, MouseEventArgs e)
        {
            //what have we clicked on, if anything
            ListViewItem listViewItem = modelsListView.GetItemAt(e.X, e.Y);
            if (listViewItem != null)
            {
                DragDropAsset dropAsset = new DragDropAsset
                {
                    Type = DragDropAsset.AssetType.Model,
                    FileName = (string)listViewItem.Tag
                };
                DataObject data = new DataObject(dropAsset);

                materialsListView.DoDragDrop(data, DragDropEffects.Copy);
            }
        }

        private void PulsarAssets_Load(object sender, EventArgs e)
        {
            //Cycle through each of the folders, find the corresponding ListItem for folder and add subitems
            //to it that are the files
            //the folder path is in the watcher

            var assetsPath = assetWatcher.Path;
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
                    listView = animationsListView;
                    listViewDirectoryImageIndex = 1;
                    break;
                case "Fonts":
                    listView = fontsListView;
                    listViewDirectoryImageIndex = 2;
                    break;
                case "Materials":
                    listView = materialsListView;
                    listViewDirectoryImageIndex = 3;
                    break;
                case "Models":
                    listView = modelsListView;
                    listViewDirectoryImageIndex = 4;
                    break;
                case "Textures":
                    listView = texturesListView;
                    listViewDirectoryImageIndex = 5;
                    break;
            }
            return listView;
        }

        private void AssetWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            //this waits on changes anywhere in the Assets folder - atm for testing this is just the project assets folder

        }

        private void PulsarAssets_Resize(object sender, EventArgs e)
        {
            AssetsControl.Width = Width - 20;
            AssetsControl.Height = Height - 20;
        }

        private void TabControl_Resize(object sender, EventArgs e)
        {
            animationsListView.Width = AssetsControl.Width - 15;
            animationsListView.Height = AssetsControl.Height - 34;

            fontsListView.Width = AssetsControl.Width - 15;
            fontsListView.Height = AssetsControl.Height - 34;

            materialsListView.Width = AssetsControl.Width - 15;
            materialsListView.Height = AssetsControl.Height - 34;

            modelsListView.Width = AssetsControl.Width - 15;
            modelsListView.Height = AssetsControl.Height - 34;

            texturesListView.Width = AssetsControl.Width - 15;
            texturesListView.Height = AssetsControl.Height - 34;
        }
    }
}
