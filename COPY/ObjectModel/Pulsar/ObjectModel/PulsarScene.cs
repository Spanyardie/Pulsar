using Pulsar.Helpers;
using Pulsar.ObjectModel.Primitives;
using System.Diagnostics;
using System.Threading.Tasks;
using Urho;
using Urho.Physics;
using WirePlane = Pulsar.ObjectModel.Primitives.WirePlane;

namespace Pulsar.ObjectModel
{
    public class PulsarScene : Scene
    {
        public PulsarCamera SceneCamera { get; private set; }

        public Octree SceneOctree { get; private set; }

        public PhysicsWorld ScenePhysics { get; set; }

        public PulsarLight SceneLight { get; private set; }

        public StaticModel ScenePlane { get; private set; }

        private PulsarApplication _mainApplication;

        public DebugRenderer SceneDebugRenderer { get; set; }

        public Camera SceneDebugCamera { get; set; }

        public Node BoxNode { get; set; }

        private bool _inDesign = true;

        private readonly SelectedObjects _selectedObjects;

        private string _name;

        private static long _currentEntityIndex = 1;

        private PulsarComponent _createdEntity;

        public PulsarApplication MainApplication
        {
            get
            {
                return _mainApplication;
            }
            set
            {
                _mainApplication = value;
            }
        }

        public string SceneName
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

        public bool InDesign
        {
            get
            {
                return _inDesign;
            }

            set
            {
                _inDesign = value;
            }
        }

        public PulsarScene(string sceneName, PulsarApplication application) : base()
        {
            _mainApplication = application;
            _name = sceneName;

            InitiateDebug(true);
            CreateSceneOctree();
            CreateSceneCamera();
            CreateSceneLight();

            if (!_inDesign)
                InitiatePhysics();

            _selectedObjects = new SelectedObjects();

            ComponentAdded += PulsarScene_ComponentAdded;
        }

        private void PulsarScene_ComponentAdded(ComponentAddedEventArgs obj)
        {
            Debug.Print("Node '" + obj.Node.Name + "' added to scene");
        }

        private void InitiateDebug(bool v)
        {
            if (v)
            {
                if (SceneDebugRenderer == null)
                {
                    SceneDebugRenderer = CreateComponent<DebugRenderer>();
                    SceneDebugRenderer.Enabled = true;
                    MainApplication.Renderer.DrawDebugGeometry(true);
                }
                else
                {
                    SceneDebugRenderer.Enabled = true;
                }
            }
            else
            {
                if (SceneDebugRenderer != null)
                {
                    SceneDebugRenderer.Enabled = false;
                }
            }
        }

        private void InitiatePhysics()
        {
            ScenePhysics = CreateComponent<PhysicsWorld>();
            ScenePhysics.SetDebugRenderer(SceneDebugRenderer);
            ScenePhysics.Enabled = true;
        }

        private void CreateSceneLight()
        {
            SceneLight = new PulsarLight("MainDirectionalLight", this, SceneDebugRenderer)
            {
                PulsarComponentClass = PulsarComponent.ComponentClass.Node,
                PulsarComponentType = PulsarComponent.ComponentType.Light,
                InDesign = _inDesign
            };

            PulsarModel model = new PulsarModel(SceneLight.GetEntityName());
            SceneLight.BaseEntity.AddComponent(model);
            //baseEntity.AddComponent(model);
            model.SetModel("Models/Box.mdl");

            PulsarMaterial material = new PulsarMaterial(SceneLight.GetEntityName());
            SceneLight.BaseEntity.AddComponent(material);
            //baseEntity.AddComponent(material);
            material.SetMaterial("Materials/StoneTiled.xml");

            model.SetMaterial(material);
            SceneLight.CreateEntity();

            SceneLight.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
            SceneLight.SetPosition(new Vector3(0, 10, 0));
            
            SceneLight.Light.LightType = LightType.Directional;
            SceneLight.Light.Brightness = 1.1f;
            SceneLight.Light.CastShadows = true;

            SceneLight.SetExtendedProperties();
            SceneLight.BaseEntity.SetupProperties();
        }

        private void CreateSceneOctree()
        {
            SceneOctree = CreateComponent<Octree>();
        }

        private void CreateSceneCamera()
        {
            Debug.Print("PulsarScene.CreateSceneCamera - Creating new PulsarCamera");
            SceneCamera = new PulsarCamera("mainCamera", this, SceneDebugRenderer)
            {
                PulsarComponentClass = PulsarComponent.ComponentClass.Node,
                PulsarComponentType = PulsarComponent.ComponentType.Camera,
                IsMainCamera = true
            };

            SceneCamera.CreateEntity();

            SceneCamera.SetPosition(new Vector3(0, 10, -40));
            SceneCamera.Camera.Node.LookAt(Vector3.Zero, Vector3.Up);

            Debug.Print("PulsarScene.CreateSceneCamera - Set position of main camera to - " + SceneCamera.Node.Position.ToString());

            Debug.Print("PulsarScene.CreateSceneCamera - Calling camera BaseEntity.SetupProperties");
            SceneCamera.SetExtendedProperties();
            SceneCamera.BaseEntity.SetupProperties();

            SceneDebugRenderer.SetView(SceneCamera.Camera);
        }

        public void CreatePlane()
        {
            var planeNode = CreateChild("MainPlane");

            planeNode.Scale = new Vector3(30, 1, 30);

            ScenePlane = planeNode.CreateComponent<StaticModel>();

            ScenePlane.Model = MainApplication.ResourceCache.GetModel("Models/Plane.mdl");

            ScenePlane.SetMaterial(MainApplication.ResourceCache.GetMaterial("Materials/StoneTiled.xml"));

            //give it a rigidbody
            RigidBody planeRigidBody = planeNode.CreateComponent<RigidBody>();
            planeRigidBody.Mass = 0; //a mass of zero means it is a static entity
            planeRigidBody.UseGravity = false;

            //Make a collision box around the plane
            CollisionShape planeCollisionShape = planeNode.CreateComponent<CollisionShape>();
            planeCollisionShape.Size = new Vector3(30, 1, 30);

            DebugRenderer renderer = GetComponent<DebugRenderer>();
            if (renderer != null)
                ScenePlane.DrawDebugGeometry(renderer, true);
        }

        public void CreateMainSceneSkybox()
        {
            //var skyboxNode = CreateChild("MainSkybox");

            //var skyboxModel = skyboxNode.CreateComponent<Skybox>();

        }

        public Node GetMainCameraNode()
        {
            Node mainCameraNode = null;

            if (SceneCamera != null)
            {
                mainCameraNode = SceneCamera.Camera.Node;
            }

            return mainCameraNode;
        }

        public SelectedObjects SelectedSceneObjects
        {
            get
            {
                return _selectedObjects;
            }

            private set { }
        }

        public void CreateBox()
        {
            _createdEntity = new BoxEntity("box" + _currentEntityIndex++.ToString(), this, SceneDebugRenderer, _inDesign)
            {
                Scale = new Vector3(5.0f, 5.0f, 5.0f),
                PulsarComponentClass = PulsarComponent.ComponentClass.Node,
                PulsarComponentType = PulsarComponent.ComponentType.Box,
                InDesign = _inDesign
            };

            _createdEntity.BaseEntity.Name = ((BoxEntity)_createdEntity).GetEntityName();
            _createdEntity.BaseEntity.SetPosition(new Vector3(0, 3, 0));
            _createdEntity.BaseEntity.SetRotation(new Vector3(0, 0, 0));
            _createdEntity.BaseEntity.SetScale(((BoxEntity)_createdEntity).Scale);

            _createdEntity.BaseEntity.AddComponent(_createdEntity);

            PulsarModel model = new PulsarModel(((BoxEntity)_createdEntity).GetEntityName())
            {
                PulsarComponentClass = PulsarComponent.ComponentClass.Node,
                PulsarComponentType = PulsarComponent.ComponentType.Model
            };
            _createdEntity.BaseEntity.AddComponent(model);
            model.SetModel("Models/Box.mdl");

            PulsarMaterial material = new PulsarMaterial(((BoxEntity)_createdEntity).GetEntityName())
            {
                PulsarComponentClass = PulsarComponent.ComponentClass.Node,
                PulsarComponentType = PulsarComponent.ComponentType.Material
            };
            _createdEntity.BaseEntity.AddComponent(material);
            material.SetMaterial("Materials/StoneTiled.xml");

            model.SetMaterial(material);
            ((BoxEntity)_createdEntity).CreateEntity();

            _createdEntity.BaseEntity.SetupProperties();

            //ensure the gizmo is not visible on first creation
            _createdEntity.BaseEntity.GetGizmo().SetGizmoVisible(false);

            Gizmo gizmo = _createdEntity.BaseEntity.GetGizmo();
            if(gizmo != null)
            {
                gizmo.GizmoEnabled = false;
            }
            //_createdEntity.BaseEntity.GetGizmo().Enabled = false;

        }

        public void CreateSphere()
        {
            _createdEntity = new SphereEntity("sphere" + _currentEntityIndex++.ToString(), this, SceneDebugRenderer, InDesign)
            {
                Scale = new Vector3(5.0f, 5.0f, 5.0f),
                PulsarComponentClass = PulsarComponent.ComponentClass.Node,
                PulsarComponentType = PulsarComponent.ComponentType.Sphere,
                InDesign = _inDesign,
                Diameter = 5.0f
            };

            _createdEntity.BaseEntity.Name = ((SphereEntity)_createdEntity).GetEntityName();
            _createdEntity.BaseEntity.SetPosition(new Vector3(10, 3, 0));
            _createdEntity.BaseEntity.SetRotation(new Vector3(0, 0, 0));
            _createdEntity.BaseEntity.SetScale(((SphereEntity)_createdEntity).Scale);

            _createdEntity.BaseEntity.AddComponent(_createdEntity);

            PulsarModel model = new PulsarModel(((SphereEntity)_createdEntity).GetEntityName())
            {
                PulsarComponentClass = PulsarComponent.ComponentClass.Node,
                PulsarComponentType = PulsarComponent.ComponentType.Model
            };
            _createdEntity.BaseEntity.AddComponent(model);
            model.SetModel("Models/Sphere.mdl");

            PulsarMaterial material = new PulsarMaterial(((SphereEntity)_createdEntity).GetEntityName())
            {
                PulsarComponentClass = PulsarComponent.ComponentClass.Node,
                PulsarComponentType = PulsarComponent.ComponentType.Material
            };

            _createdEntity.BaseEntity.AddComponent(material);
            material.SetMaterial("Materials/StoneTiled.xml");

            model.SetMaterial(material);
            ((SphereEntity)_createdEntity).CreateEntity();

            _createdEntity.BaseEntity.SetupProperties();

            //ensure the gizmo is not visible on first creation
            _createdEntity.BaseEntity.GetGizmo().SetGizmoVisible(false);
        }

        public void CreateCylinder()
        {
            var entity = new CylinderEntity("cylinder" + _currentEntityIndex++.ToString())
            {
                PulsarComponentClass = PulsarComponent.ComponentClass.Node,
                PulsarComponentType = PulsarComponent.ComponentType.Cylinder,
                InDesign = _inDesign,
                Diameter = 5.0f,
                Height = 5.0f,
                Scale = new Vector3(5, 5, 5)
            };

            BaseEntity baseEntity = new BaseEntity
                                        (
                                            entity.GetEntityName(),
                                            this,
                                            SceneDebugRenderer,
                                            new Vector3(-5, 2.5f, 0),
                                            new Vector3(0, 0, 0),
                                            entity.Scale
                                        )
                                        { InDesign = _inDesign };
            baseEntity.AddComponent(entity);

            PulsarModel model = new PulsarModel(entity.GetEntityName());
            baseEntity.AddComponent(model);
            model.SetModel("Models/Cylinder.mdl");

            PulsarMaterial material = new PulsarMaterial(entity.GetEntityName());
            baseEntity.AddComponent(material);
            material.SetMaterial("Materials/StoneTiled.xml");

            model.SetMaterial(material);
            entity.CreateEntity();

            //ensure the gizmo is not visible on first creation
            baseEntity.GetGizmo().SetGizmoVisible(false);
        }

        public void CreateCone()
        {
            var entity = new ConeEntity("cone" + _currentEntityIndex++.ToString())
            {
                PulsarComponentClass = PulsarComponent.ComponentClass.Node,
                PulsarComponentType = PulsarComponent.ComponentType.Cone,
                InDesign = _inDesign,
                Diameter = 5.0f,
                Height = 5.0f,
                Scale = new Vector3(5, 5, 5)
            };

            BaseEntity baseEntity = new BaseEntity
                                        (
                                            entity.GetEntityName(),
                                            this,
                                            SceneDebugRenderer,
                                            new Vector3(10, 2.5f, 0),
                                            new Vector3(0, 0, 0),
                                            entity.Scale
                                        )
                                        { InDesign = _inDesign };
            baseEntity.AddComponent(entity);

            PulsarModel model = new PulsarModel(entity.GetEntityName());
            baseEntity.AddComponent(model);
            model.SetModel("Models/Cone.mdl");

            PulsarMaterial material = new PulsarMaterial(entity.GetEntityName());
            baseEntity.AddComponent(material);
            material.SetMaterial("Materials/StoneTiled.xml");

            model.SetMaterial(material);
            entity.CreateEntity();

            //ensure the gizmo is not visible on first creation
            baseEntity.GetGizmo().SetGizmoVisible(false);
        }

        public void CreateWirePlane()
        {
            var entity = new WirePlane("wirePlane" + _currentEntityIndex++.ToString(), this)
            {
                InDesign = _inDesign
            };
            entity.DebugRenderer = SceneDebugRenderer;
            entity.Position = new Vector3(0, 0, 0);
            entity.Size = 50;
            entity.Scale = 3.0f;
            entity.Color = Color.Transparent;
            entity.CreateEntity();
            entity.ViewMask = 0x80000000;

        }

        public PulsarApplication GetApplication()
        {
            return MainApplication;
        }

    }
}
