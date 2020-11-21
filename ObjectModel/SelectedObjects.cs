using Pulsar.ObjectModel.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pulsar.ObjectModel
{
    public class SelectedObjects
    {
        private readonly List<SelectedObject> _selectedObjects;

        public SelectedObjects()
        {
            _selectedObjects = new List<SelectedObject>();
        }

        public void AddSelectedObject(SelectedObject selectedObject)
        {
            if (selectedObject != null)
            {
                var containedObject = _selectedObjects.Find(node => (!node.SelectedNode.IsDeleted && node.SelectedNode.Name == selectedObject.SelectedNode.Name));
                if (containedObject == null)
                {
                    _selectedObjects.Add(selectedObject);
                }
            }
        }

        public int SelectedObjectCount
        {
            get
            {
                return _selectedObjects.Count;
            }
        }

        public List<SelectedObject> ObjectList
        {
            get
            {
                return _selectedObjects;
            }

            private set { }
        }

        public void Clear()
        {
            _selectedObjects.Clear();
        }

        public void ResetGizmosAndMaterials()
        {
            Debug.Print("ResetGizmosAndMaterials - entered...");
            if (_selectedObjects.Count == 0)
            {
                Debug.Print("ResetGizmosAndMaterials - returning because _selectedObjects.Count was zero!");
                return;
            }

            Debug.Print("ResetGizmosAndMaterials - Processing " + _selectedObjects.Count.ToString() + " selected objects...");
            foreach (SelectedObject selectedObject in _selectedObjects)
            {
                if (selectedObject != null)
                {
                    if (!selectedObject.SelectedNode.IsDeleted)
                    {
                        PulsarModel pulsarModel = selectedObject.SelectedNode.GetComponent<PulsarModel>();
                        if (pulsarModel != null)
                        {
                            BaseEntity baseEntity = pulsarModel.GetBaseEntity();
                            if (baseEntity != null)
                            {
                                Debug.Print("ResetGizmosAndMaterials - Calling baseEntity.UnSelect...");
                                baseEntity.UnSelect();
                            }
                            else
                            {
                                Debug.Print("ResetGizmosAndMaterials - baseEntity of pulsarModel " + pulsarModel.Node.Name + " was null - unable to UnSelect!!!");
                            }
                        }
                        else
                        {
                            Debug.Print("ResetGizmosAndMaterials - pulsarModel of " + selectedObject.SelectedNode.Name + " was null!");
                        }
                    }
                    else
                    {
                        Debug.Print("ResetGizmosAndMaterials - selectedObject.SelectedNode was deleted!");
                    }
                }
                else
                {
                    Debug.Print("ResetGizmosAndMaterials - selectedObject was null!");
                }
            }
        }
    }
}
