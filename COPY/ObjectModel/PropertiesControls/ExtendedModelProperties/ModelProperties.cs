using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Urho;

namespace ExtendedModelProperties
{
    public partial class ModelProperties: UserControl
    {
        //TODO: Add bounding box property

        public event EventHandler ModelChanged;
        public event EventHandler MaterialChanged;
        public event EventHandler LODChanged;
        public event EventHandler RemoveModel;
        public event EventHandler WindowRolled;

        private const int MINIMUM_HEIGHT = 33;
        private const int MAXIMUM_HEIGHT = 134;
        public int MinimumHeight 
        { 
            get
            {
                return MINIMUM_HEIGHT;
            }
        }

        public int MaximumHeight
        {
            get
            {
                return MAXIMUM_HEIGHT;
            }
        }

        private Node _node;
        public Node Node 
        { 
            get
            {
                return _node;
            }
            set
            {
                _node = value;
            }
        }

        private string _modelNodeName;
        public string ModelNodeName 
        { 
            get
            {
                return _modelNodeName;
            }
            set
            {
                _modelNodeName = value;
            }
        }

        private enum FileType
        {
            Model = 0,
            Material
        }

        public enum WindowRoll
        {
            RollUp = 0,
            RollDown
        }
        private WindowRoll _windowRoll = WindowRoll.RollUp;

        private FileType _fileType;

        private string _assetsFolder;
        public string AssetsFolder 
        { 
            get
            {
                return _assetsFolder;
            }
            set
            {
                _assetsFolder = value;

                openFileDialog.InitialDirectory = value;
            }
        }

        private string _modelFilePath;
        public string ModelFilePath 
        { 
            get
            {
                return _modelFilePath;
            }
            set
            {
                _modelFilePath = value;
                txtModelName.Text = value.Substring(value.LastIndexOf('/') + 1);
            }
        }

        private string _materialFilePath;
        public string MaterialFilePath 
        { 
            get
            {
                return _materialFilePath;
            }
            set
            {
                _materialFilePath = value;
                txtMaterialName.Text = value.Substring(value.LastIndexOf('/') + 1);
            }
        }

        public ModelProperties()
        {
            InitializeComponent();

            txtModelName.DragDrop += ModelName_DragDrop;
            txtModelName.DragEnter += ModelName_DragEnter;
            txtMaterialName.DragDrop += MaterialName_DragDrop;
            txtMaterialName.DragEnter += MaterialName_DragEnter;
            openFileDialog.FileOk += OpenFileDialog_FileOk;
            Load += ModelProperties_Load;
        }

        private void MaterialName_DragEnter(object sender, DragEventArgs e)
        {
            DragDropAsset dropAsset = new DragDropAsset();
            Type type = dropAsset.GetType();

            DragDropAsset data = (DragDropAsset)e.Data.GetData(type);

            if(data.Type == DragDropAsset.AssetType.Material)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void ModelName_DragEnter(object sender, DragEventArgs e)
        {
            DragDropAsset dropAsset = new DragDropAsset();
            Type type = dropAsset.GetType();

            DragDropAsset data = (DragDropAsset)e.Data.GetData(type);

            if (data.Type == DragDropAsset.AssetType.Model)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void MaterialName_DragDrop(object sender, DragEventArgs e)
        {
            Type type = new DragDropAsset().GetType();

            DragDropAsset asset = (DragDropAsset)e.Data.GetData(type);
            
            if (asset != null)
            {
                if (asset.Type == DragDropAsset.AssetType.Material)
                {
                    _fileType = FileType.Model;
                    string filePath = asset.FileName;

                    _materialFilePath = filePath;

                    txtMaterialName.Text = filePath.Substring(filePath.LastIndexOf('\\') + 1);

                    MaterialChangedEventArgs materialChangedEventArgs = new MaterialChangedEventArgs
                    {
                        MaterialFilePath = filePath
                    };
                    OnMaterialChanged(sender, materialChangedEventArgs);
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ModelProperties_Load(object sender, EventArgs e)
        {
            ToolTip modelToolTip = new ToolTip
            {
                ShowAlways = true
            };
            modelToolTip.SetToolTip(txtModelName, "Selected model name");

            ToolTip materialToolTip = new ToolTip
            {
                ShowAlways = true
            };
            materialToolTip.SetToolTip(txtMaterialName, "Selected material name");

            ToolTip fileModelButtonToolTip = new ToolTip
            {
                ShowAlways = true
            };
            fileModelButtonToolTip.SetToolTip(btnModelFile, "Click to select model from a dialog");

            ToolTip fileMaterialButtonToolTip = new ToolTip
            {
                ShowAlways = true
            };
            fileMaterialButtonToolTip.SetToolTip(btnMaterialFile, "Click to select material from a dialog");
        }

        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            switch (_fileType)
            {
                case FileType.Model:
                    txtModelName.Text = openFileDialog.SafeFileName;
                    _modelFilePath = openFileDialog.FileName;
                    ModelChangedEventArgs modelChangedEventArgs = new ModelChangedEventArgs
                    {
                        ModelFilePath = openFileDialog.SafeFileName
                    };
                    OnModelChanged(sender, modelChangedEventArgs);
                    break;
                case FileType.Material:
                    txtMaterialName.Text = openFileDialog.SafeFileName;
                    _materialFilePath = openFileDialog.FileName;
                    MaterialChangedEventArgs materialChangedEventArgs = new MaterialChangedEventArgs
                    {
                        MaterialFilePath = openFileDialog.SafeFileName
                    };
                    OnMaterialChanged(sender, materialChangedEventArgs);
                    break;
            }
        }

        private void ModelName_DragDrop(object sender, DragEventArgs e)
        {
            Type type = new DragDropAsset().GetType();

            DragDropAsset dropAsset = (DragDropAsset)e.Data.GetData(type);

            if (dropAsset != null)
            {
                if (dropAsset.Type == DragDropAsset.AssetType.Model)
                {
                    _fileType = FileType.Model;
                    string filePath = dropAsset.FileName;

                    _modelFilePath = filePath;

                    txtModelName.Text = filePath.Substring(filePath.LastIndexOf('\\') + 1);

                    ModelChangedEventArgs modelChangedEventArgs = new ModelChangedEventArgs
                    {
                        ModelFilePath = filePath
                    };
                    OnModelChanged(sender, modelChangedEventArgs);
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ModelRemoveEventArgs modelRemoveEventArgs = new ModelRemoveEventArgs
            {
                ModelNodeName = _modelNodeName,
                Node = _node
            };
            OnRemoveModel(sender, modelRemoveEventArgs);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }

        private void btnModelFile_Click(object sender, EventArgs e)
        {
            string selectedPath = "c:";

            _fileType = FileType.Model;

            //if (!string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                selectedPath = _assetsFolder + "\\Models";

            openFileDialog.InitialDirectory = selectedPath;
            openFileDialog.Title = "Select a model";
            openFileDialog.FileName = "";
            openFileDialog.ShowDialog(this);
        }

        private void btnMaterialFile_Click(object sender, EventArgs e)
        {
            string selectedPath = "c:";

            _fileType = FileType.Material;

            //if (!string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            selectedPath = _assetsFolder + "\\Materials";

            openFileDialog.InitialDirectory = selectedPath;
            openFileDialog.Title = "Select a material";
            openFileDialog.FileName = "";
            openFileDialog.ShowDialog(this);
        }

        private void btnViewMaterial_Click(object sender, EventArgs e)
        {

        }

        public virtual void OnModelChanged(object sender, ModelChangedEventArgs e)
        {
            ModelChanged?.Invoke(sender, e);
        }

        public virtual void OnMaterialChanged(object sender, MaterialChangedEventArgs e)
        {
            MaterialChanged?.Invoke(sender, e);
        }

        public virtual void OnRemoveModel(object sender, ModelRemoveEventArgs e)
        {
            RemoveModel?.Invoke(sender, e);
        }

        public virtual void OnWindowRolled(WindowRollEventArgs e)
        {
            WindowRolled?.Invoke(this, e);
        }

        private void ModelProperties_Resize(object sender, EventArgs e)
        {
            if(Size.Width >= 312)
            {
                Point btnRemoveLocation = btnRemove.Location;
                btnRemoveLocation.X = Size.Width - 47;
                btnRemove.Location = btnRemoveLocation;
                Point btnSettingsLocation = btnSettings.Location;
                btnSettingsLocation.X = Size.Width - 86;
                btnSettings.Location = btnSettingsLocation;
                Size headerSize = lblHeader.Size;
                headerSize.Width = Size.Width;
                lblHeader.Size = headerSize;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if(_windowRoll == WindowRoll.RollUp)
            {
                Height = 33;
                _windowRoll = WindowRoll.RollDown;
                btnView.Text = "v";
                WindowRollEventArgs eventArgs = new WindowRollEventArgs
                {
                    WindowRoll = WindowRoll.RollUp
                };
                OnWindowRolled(eventArgs);
            }
            else
            {
                Height = 134;
                _windowRoll = WindowRoll.RollUp;
                btnView.Text = "^";
                WindowRollEventArgs eventArgs = new WindowRollEventArgs
                {
                    WindowRoll = WindowRoll.RollDown
                };
                OnWindowRolled(eventArgs);
            }
        }
    }
}
