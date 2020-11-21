using Pulsar.ExceptionsHandling;
using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Primitives;
using System.Diagnostics;
using Urho;

namespace Pulsar.Helpers
{
    public static class GizmoHelper
    {
        public static void SetAsSelected(Node node, Gizmo gizmo, PulsarScene scene)
        {
            StaticModel nodeModel = node.GetComponent<StaticModel>();
            if (nodeModel != null && scene != null)
            {
                PulsarApplication pulsarApplication = scene.GetApplication();
                if (pulsarApplication != null)
                {
                    MaterialTemp tempMaterial = new MaterialTemp(nodeModel, pulsarApplication);
                    if (tempMaterial != null)
                    {
                        tempMaterial.Name = "materialTemp";

                        try
                        {
                            tempMaterial.SetTransparentMaterial();
                        }
                        catch(PulsarGizmoException gizmoException)
                        {
                            gizmoException.Source = "[GizmoHelper:SetAsSelected]";
                            throw gizmoException;
                        }

                        node.AddComponent(tempMaterial);
                        if (gizmo != null)
                        {
                            gizmo.Node.Position = node.Position;
                            gizmo.SetGizmoVisible(true);
                            gizmo.Node.Enabled = true;
                        }
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
                Debug.Print("GizmoHelper.UnSelect - calling matTemp.ResetMaterial...");
                matTemp.ResetMaterial();
                Debug.Print("GizmoHelper.UnSelect - calling node.RemoveComponent with param matTemp...");
                node.RemoveComponent(matTemp);
                if (gizmo != null)
                {
                    Debug.Print("GizmoHelper.UnSelect - Calling gizmo.SetGizmoVisible(false)...");
                    gizmo.SetGizmoVisible(false);

                    gizmo.GizmoEnabled = false;

                    Debug.Print("GizmoHelper.UnSelect - Called gizmo.SetGizmoVisible with value 'false' for gizmo '" + gizmo.Name + "'");
                    gizmo.Node.Enabled = false;
                    Debug.Print("GizmoHelper.UnSelect - Set gizmo.Node.Enabled to value " + gizmo.Node.Enabled.ToString() + " for gizmo '" + gizmo.Name + "'");
                }
                else
                {
                    Debug.Print("GizmoHelper.UnSelect - Unable to process as gizmo was null!");
                }
            }
            else
            {
                Debug.Print("GizmoHelper.UnSelect - Unable to process as temporary material was null!");
            }
        }
    }
}
