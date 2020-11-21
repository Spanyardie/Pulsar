using Urho;

namespace Pulsar.ObjectModel
{
    public class SelectedObject
    {
        private Drawable _selectedDrawable;
        private Node _selectedNode;
        private Node _parentNode;
        private string _prefabName;
        private bool _markForDelete = false;
        private string _nodeClickedName = "";
        private bool _removeFromScene;

        public bool RemoveFromScene 
        { 
            get
            {
                return _removeFromScene;
            }
            set
            {
                _removeFromScene = value;
            }
        }

        public string NodeClickedName
        {
            get
            {
                return _nodeClickedName;
            }
            set
            {
                _nodeClickedName = value;
            }
        }

        public bool MarkForDelete
        {
            get
            {
                return _markForDelete;
            }
            set
            {
                _markForDelete = value;
            }
        }

        public Drawable Drawable
        {
            get
            {
                return _selectedDrawable;
            }

            set
            {
                _selectedDrawable = value;
            }
        }

        public Node SelectedNode
        {
            get
            {
                return _selectedNode;
            }

            set
            {
                _selectedNode = value;
            }
        }

        public Node ParentNode
        {
            get
            {
                return _parentNode;
            }

            set
            {
                _parentNode = value;
            }
        }

        public string PrefabName
        {
            get
            {
                return _prefabName;
            }

            set
            {
                _prefabName = value;
            }
        }

        public BoundingBox GetBoundingBox()
        {
            Drawable drawable = _selectedNode.GetComponent<Drawable>();
            return drawable.WorldBoundingBox;
        }

        public void DrawDebugGeometry(PulsarScene scene)
        {
            scene.SceneDebugRenderer.AddBoundingBox(GetBoundingBox(), Color.Red);
        }
    }
}
