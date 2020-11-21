using System.Diagnostics;
using Urho;

namespace Pulsar.ObjectModel.Primitives
{
    public class Gizmo : PulsarComponent
    {
        private Node _gizmoNode;
        private Node _gizmoGeometryNode;
        private PulsarScene _scene;
        private string _name;
        private Vector3 _size;
        private Vector3 _position;
        private GizmoMode _gizmoMode = GizmoMode.Translate;

        public enum GizmoMode
        {
            Translate = 0,
            Rotate,
            Scale
        }

        //public BaseEntity BaseEntity { get; set; }

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
                    _gizmoGeometryNode = Node.CreateChild("gizmoGeometry");
                    CustomGeometry geom = _gizmoGeometryNode.CreateComponent<CustomGeometry>();
                    geom.BeginGeometry(0, PrimitiveType.LineList);
                    var material = new Material();
                    material.SetTechnique(0, CoreAssets.Techniques.NoTextureUnlitVCol, 1, 1);
                    material.RenderOrder = 255;
                    geom.SetMaterial(material);

                    //x
                    geom.DefineVertex(Vector3.Zero);
                    geom.DefineColor(Color.Red);
                    geom.DefineVertex(Vector3.UnitX);// * (_size.X * 0.5f));
                    geom.DefineColor(Color.Red);
                    //y
                    geom.DefineVertex(Vector3.Zero);
                    geom.DefineColor(Color.Green);
                    geom.DefineVertex(Vector3.UnitY);// * (_size.Y * 0.5f));
                    geom.DefineColor(Color.Green);
                    //z
                    geom.DefineVertex(Vector3.Zero);
                    geom.DefineColor(Color.Blue);
                    geom.DefineVertex(Vector3.UnitZ);// * (_size.Z * 0.5f));
                    geom.DefineColor(Color.Blue);

                    geom.Commit();

                    var yNode = _gizmoGeometryNode.CreateChild("yTranslateGizmoNode");
                    var xNode = _gizmoGeometryNode.CreateChild("zTranslateGizmoNode");
                    var zNode = _gizmoGeometryNode.CreateChild("xTranslateGizmoNode");

                    var model = yNode.CreateComponent<StaticModel>();
                    model.Model = _scene.GetApplication().ResourceCache.GetModel("Models/Axis.mdl");
                    model.SetMaterial(_scene.GetApplication().ResourceCache.GetMaterial("Materials/GizmoGreen.xml"));
                    model.GetMaterial().RenderOrder = 255;
                    var gizmoGeometryNodePosition = yNode.Position;
                    gizmoGeometryNodePosition.Y += (_size.Y / 2);
                    yNode.Position = gizmoGeometryNodePosition;

                    model = zNode.CreateComponent<StaticModel>();
                    model.Model = _scene.GetApplication().ResourceCache.GetModel("Models/Axis.mdl");
                    model.SetMaterial(_scene.GetApplication().ResourceCache.GetMaterial("Materials/GizmoRed.xml"));
                    model.GetMaterial().RenderOrder = 255;
                    zNode.Rotate(new Quaternion(0, 0, -90));
                    gizmoGeometryNodePosition = zNode.Position;
                    gizmoGeometryNodePosition.X += (_size.X / 2);
                    zNode.Position = gizmoGeometryNodePosition;

                    model = xNode.CreateComponent<StaticModel>();
                    model.Model = _scene.GetApplication().ResourceCache.GetModel("Models/Axis.mdl");
                    model.SetMaterial(_scene.GetApplication().ResourceCache.GetMaterial("Materials/GizmoBlue.xml"));
                    model.GetMaterial().RenderOrder = 255;
                    xNode.Rotate(new Quaternion(90, 0, 0));
                    gizmoGeometryNodePosition = xNode.Position;
                    gizmoGeometryNodePosition.Z += (_size.Z / 2);
                    xNode.Position = gizmoGeometryNodePosition;

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

        private string GetGizmoNodeNameByGizmoMode()
        {
            switch (_gizmoMode)
            {
                case GizmoMode.Translate:
                    return "TranslateGizmoNode";

                case GizmoMode.Rotate:
                    return "RotateGizmoNode";

                case GizmoMode.Scale:
                    return "ScaleGizmoNode";
            }
            return "";
        }

        public void SetGizmoVisible(bool isVisible)
        {
            var gizmoName = GetGizmoNodeNameByGizmoMode();

            var xNode = _gizmoGeometryNode.GetChild("x" + gizmoName);
            var yNode = _gizmoGeometryNode.GetChild("y" + gizmoName);
            var zNode = _gizmoGeometryNode.GetChild("z" + gizmoName);
            var customGeometry = _gizmoGeometryNode.GetComponent<CustomGeometry>();

            if (customGeometry != null) customGeometry.Enabled = isVisible;

            if (xNode != null) xNode.Enabled = isVisible;

            if (yNode != null) yNode.Enabled = isVisible;

            if (zNode != null) zNode.Enabled = isVisible;
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
                Debug.Print("Gizmo enabled set to " + value.ToString() + "!");
            }
        }
    }
}
