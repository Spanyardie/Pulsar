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
            if (_selectedObjects.Count == 0)
            {
                return;
            }

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
                                baseEntity.UnSelect();
                            }
                        }
                    }
                }
            }
        }
    }
}
