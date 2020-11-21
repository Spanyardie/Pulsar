using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using ExtendedModelProperties;

namespace Pulsar.ObjectModel.PropertiesModel
{
    public class ModelProperties : PulsarComponent
    {
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
            }
        }

        private string _modelName;
        public string ModelName 
        { 
            get
            {
                return _modelName;
            }
            set
            {
                _modelName = value;
            }
        }

        private string _materialName;
        public string MaterialName 
        { 
            get
            {
                return _materialName;
            }
            set
            {
                _materialName = value;
            }
        }

        public virtual new Node Node { get; set; }

        private PulsarApplication _pulsarApplication;
        public virtual PulsarApplication PulsarApplication
        {
            get
            {
                return _pulsarApplication;
            }
            set
            {
                _pulsarApplication = value;
                //RegisterForMessages();
            }
        }

        public virtual new PulsarScene Scene { get; set; }

        private ExtendedModelProperties.ModelProperties _container;
        public ExtendedModelProperties.ModelProperties Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        private int _componentHeight;

        public virtual int ComponentHeight
        {
            get
            {
                return _componentHeight;
            }
            set
            {
                _componentHeight = value;
            }
        }

        public bool UpdateComplete { get; set; }


    }
}
