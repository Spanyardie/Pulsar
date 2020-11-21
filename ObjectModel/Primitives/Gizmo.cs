using System.Diagnostics;
using Urho;

namespace Pulsar.ObjectModel.Primitives
{
    public class Gizmo : PulsarComponent
    {
        #region Private variables
        private Node _gizmoNode;
        private Node _gizmoGeometryNode;
        private PulsarScene _scene;
        private string _name;
        private Vector3 _size;
        private Vector3 _position;
        private GizmoMode _gizmoMode = GizmoMode.Translate;
        #endregion

        #region Enumeration
        public enum GizmoMode
        {
            Translate = 0,
            Rotate,
            Scale
        }
        #endregion

        #region Constructors
        public Gizmo() : base() { }

        public Gizmo(PulsarScene scene)
        {
            _scene = scene;
        }

        public Gizmo(string name, PulsarScene scene, Node gizmoNode)
        {
            _name = name;
            _scene = scene;
            _gizmoNode = gizmoNode;
        }

        public Gizmo(string name, PulsarScene scene, Node gizmoNode, BaseEntity baseEntity)
        {
            _name = name;
            _scene = scene;
            _gizmoNode = gizmoNode;
            BaseEntity = baseEntity;
        }
        #endregion

        #region Public accessors
        public Vector3 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                if (_gizmoNode != null)
                {
                    _gizmoNode.Position = value;
                }
            }
        }

        public new Node Node
        {
            get
            {
                return _gizmoNode;
            }
            set
            {
                _gizmoNode = value;
            }
        }

        public new PulsarScene Scene
        {
            get
            {
                return _scene;
            }
            set
            {
                _scene = value;
            }
        }

        public Vector3 Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public bool GizmoEnabled
        {
            get
            {
                return Enabled;
            }
            set
            {
                Enabled = value;
                _gizmoGeometryNode.Enabled = value;
                Debug.Print("Gizmo.GizmoEnabled - Gizmo enabled set to " + value.ToString() + "!");
            }
        }
        #endregion

        #region Public methods
        public GizmoMode GetGizmoMode()
        {
            return _gizmoMode;
        }

        public void SetGizmoMode(GizmoMode gizmoMode)
        {
            _gizmoMode = gizmoMode;
        }

        public void Initialise()
        {
            PulsarComponentClass = ComponentClass.Node;
            PulsarComponentType = ComponentType.Gizmo;

            if (Scene != null)
            {
                if (Node != null)
                {
                    _gizmoGeometryNode = Node.CreateChild("gizmoGeometry_" + Node.Name);

                    CustomGeometry customGeometry = _gizmoGeometryNode.CreateComponent<CustomGeometry>();
                    customGeometry.BeginGeometry(0, PrimitiveType.LineList);
                    var material = new Material();
                    material.SetTechnique(0, CoreAssets.Techniques.NoTextureUnlitVCol, 1, 100);
                    material.RenderOrder = 255;
                    customGeometry.SetMaterial(material);

                    //x
                    customGeometry.DefineVertex(Vector3.Zero);
                    customGeometry.DefineColor(Color.Red);
                    customGeometry.DefineVertex(Vector3.UnitX * (_size.X /* * 5f */));
                    customGeometry.DefineColor(Color.Red);
                    //y
                    customGeometry.DefineVertex(Vector3.Zero);
                    customGeometry.DefineColor(Color.Green);
                    customGeometry.DefineVertex(Vector3.UnitY * (_size.Y /* * 5f */));
                    customGeometry.DefineColor(Color.Green);
                    //z
                    customGeometry.DefineVertex(Vector3.Zero);
                    customGeometry.DefineColor(Color.Blue);
                    customGeometry.DefineVertex(Vector3.UnitZ * (_size.Z /* * 5f */));
                    customGeometry.DefineColor(Color.Blue);

                    customGeometry.Commit();

                    var yNode = _gizmoGeometryNode.CreateChild("yTranslateGizmoNode_" + Node.Name);
                    var xNode = _gizmoGeometryNode.CreateChild("zTranslateGizmoNode_" + Node.Name);
                    var zNode = _gizmoGeometryNode.CreateChild("xTranslateGizmoNode_" + Node.Name);

                    var model = yNode.CreateComponent<StaticModel>();
                    model.Model = _scene.GetApplication().ResourceCache.GetModel("Models/Axis.mdl");
                    model.SetMaterial(_scene.GetApplication().ResourceCache.GetMaterial("Materials/GizmoGreen.xml"));
                    model.GetMaterial().RenderOrder = 255;
                    var gizmoGeometryNodePosition = yNode.Position;
                    gizmoGeometryNodePosition.Y += (_size.Y / 2);
                    yNode.Position = gizmoGeometryNodePosition;
                    yNode.Scale = Node.Scale / 2;

                    model = zNode.CreateComponent<StaticModel>();
                    model.Model = _scene.GetApplication().ResourceCache.GetModel("Models/Axis.mdl");
                    model.SetMaterial(_scene.GetApplication().ResourceCache.GetMaterial("Materials/GizmoRed.xml"));
                    model.GetMaterial().RenderOrder = 255;
                    zNode.Rotate(new Quaternion(0, 0, -90));
                    gizmoGeometryNodePosition = zNode.Position;
                    gizmoGeometryNodePosition.X += (_size.X / 2);
                    zNode.Position = gizmoGeometryNodePosition;
                    zNode.Scale = Node.Scale / 2;

                    model = xNode.CreateComponent<StaticModel>();
                    model.Model = _scene.GetApplication().ResourceCache.GetModel("Models/Axis.mdl");
                    model.SetMaterial(_scene.GetApplication().ResourceCache.GetMaterial("Materials/GizmoBlue.xml"));
                    model.GetMaterial().RenderOrder = 255;
                    xNode.Rotate(new Quaternion(90, 0, 0));
                    gizmoGeometryNodePosition = xNode.Position;
                    gizmoGeometryNodePosition.Z += (_size.Z / 2);
                    xNode.Position = gizmoGeometryNodePosition;
                    xNode.Scale = Node.Scale / 2;

                    _gizmoGeometryNode.Enabled = false;
                }
            }
        }

        public void RemoveGeometry()
        {
            CustomGeometry geom = _gizmoGeometryNode.GetComponent<CustomGeometry>();
            if (geom != null)
            {
                _gizmoGeometryNode.RemoveComponent(geom);

                var gizmoName = GetGizmoNodeNameByGizmoMode();
                var xNode = _gizmoGeometryNode.GetChild("x" + gizmoName);
                var yNode = _gizmoGeometryNode.GetChild("y" + gizmoName);
                var zNode = _gizmoGeometryNode.GetChild("z" + gizmoName);

                if (xNode != null) { xNode.Remove(); }
                if (yNode != null) { yNode.Remove(); }
                if (zNode != null) { zNode.Remove(); }
            }
        }

        public void SetGizmoVisible(bool isVisible)
        {
            Debug.Print("Gizmo.SetGizmoVisible - entered with isVisible value of '" + isVisible.ToString() + "'");
            var gizmoName = GetGizmoNodeNameByGizmoMode();
            Debug.Print("Gizmo.SetGizmoVisible - Found gizmo name of '" + gizmoName + "'");

            var xNode = _gizmoGeometryNode.GetChild("x" + gizmoName);
            var yNode = _gizmoGeometryNode.GetChild("y" + gizmoName);
            var zNode = _gizmoGeometryNode.GetChild("z" + gizmoName);
            var customGeometry = _gizmoGeometryNode.GetComponent<CustomGeometry>();

            if (customGeometry != null) customGeometry.Enabled = isVisible;

            if (xNode != null)
            {
                Debug.Print("Gizmo.SetGizmoVisible - setting xNode (" + xNode.Name + ") Enabled to value '" + isVisible.ToString() + "'");
                xNode.Enabled = isVisible; 
            }
            else
            {
                Debug.Print("Gizmo.SetGizmoVisible - xNode was NULL!");
            }

            if (yNode != null)
            {
                Debug.Print("Gizmo.SetGizmoVisible - setting yNode (" + yNode.Name + ") Enabled to value '" + isVisible.ToString() + "'");
                yNode.Enabled = isVisible;
            }
            else
            {
                Debug.Print("Gizmo.SetGizmoVisible - yNode was NULL!");
            }

            if (zNode != null)
            {
                Debug.Print("Gizmo.SetGizmoVisible - setting zNode (" + zNode.Name + ") Enabled to value '" + isVisible.ToString() + "'");
                zNode.Enabled = isVisible;
            }
            else
            {
                Debug.Print("Gizmo.SetGizmoVisible - zNode was NULL!");
            }

            _gizmoGeometryNode.Enabled = isVisible;
        }
        #endregion

        #region Private and protected methods
        private string GetGizmoNodeNameByGizmoMode()
        {
            switch (_gizmoMode)
            {
                case GizmoMode.Translate:
                    return "TranslateGizmoNode_" + Node.Name;

                case GizmoMode.Rotate:
                    return "RotateGizmoNode_" + Node.Name;

                case GizmoMode.Scale:
                    return "ScaleGizmoNode_" + Node.Name;
            }
            return "";
        }

        protected override void Dispose(bool disposing)
        {
            _gizmoGeometryNode?.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}
