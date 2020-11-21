using Urho;

namespace Pulsar.ObjectModel
{
    public class MaterialTemp : Component
    {
        public StaticModel SceneNodeModel { get; set; }

        private Material _tempMaterial;
        private readonly PulsarApplication _application;
        public string Name { get; set; }

        public MaterialTemp() { }

        public MaterialTemp(StaticModel sceneModel, PulsarApplication application)
        {
            SceneNodeModel = sceneModel;
            _application = application;
        }

        public void SetTransparentMaterial()
        {
            //grab the existing material
            _tempMaterial = SceneNodeModel.Material;
            //temporarily replace it with the transparent one
            Material material = _application.ResourceCache.GetMaterial("Materials/GreenTransparent.xml");
            if (material != null)
            {
                material.Name = "greenTransparent";
                SceneNodeModel.SetMaterial(material);
            }
        }

        public void UpdateMaterialTemp(Material material)
        {
            _tempMaterial = material;
        }

        public void ResetMaterial()
        {
            SceneNodeModel.SetMaterial(_tempMaterial);
        }
    }
}
