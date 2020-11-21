using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Primitives;
using System;
using System.Diagnostics;
using Urho;

namespace Pulsar.Helpers
{
    public static class GizmoHelper
    {
        public static void SetAsSelected(Node node, Gizmo gizmo, PulsarScene scene)
        {
            StaticModel nodeModel = node.GetComponent<StaticModel>();
            if (nodeModel != null)
            {
                MaterialTemp tempMaterial = new MaterialTemp(nodeModel, scene.GetApplication());
                if (tempMaterial != null)
                {
                    tempMaterial.Name = "materialTemp";
                    tempMaterial.SetTransparentMaterial();

                    node.AddComponent(tempMaterial);
                    if (gizmo != null)
                    {
                        gizmo.Node.Position = node.Position;
                        Debug.Print("GizmoHelper.SetAsSelected - Calling gizmo.SetGizmoVisible with value true for gizmo '" + gizmo.Name);
                        gizmo.SetGizmoVisible(true);
                        Debug.Print("GizmoHelper.SetAsSelected - Setting gizmo.Node.Enabled to true for gizmo '" + gizmo.Name);
                        gizmo.Node.Enabled = true;
                    }
                }
            }
        }

        public static void UpdateTemporaryMaterialStore(Node node, Material material)
        {
            if(node != null)
            {
                MaterialTemp materialTemp = node.GetComponent<MaterialTemp>();
                if(materialTemp != null)
                {
                    materialTemp.UpdateMaterialTemp(material);
                }
            }
        }

        public static void UnSelect(Node node, Gizmo gizmo)
        {
            MaterialTemp matTemp = node.GetComponent<MaterialTemp>();
            if (matTemp != null)
            {
                matTemp.ResetMaterial();
                node.RemoveComponent(matTemp);
                if (gizmo != null)
                {
                    gizmo.SetGizmoVisible(false);
                    Debug.Print("GizmoHelper.UnSelect - Called gizmo.SetGizmoVisible with value 'false' for gizmo '" + gizmo.Name + "'");
                    gizmo.Node.Enabled = false;
                    Debug.Print("GizmoHelper.UnSelect - Set gizmo.Node.Enabled to value " + gizmo.Node.Enabled.ToString() + " for gizmo '" + gizmo.Name + "'");
                }
            }
        }
    }
}
