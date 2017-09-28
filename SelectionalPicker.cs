using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NonInstancedMaterialProperty {

    public class SelectionalPicker : BasePicker {
        [SerializeField]
        protected Selector[] properties;

        public void Pick(params int[] propertyIndices) {
            if (propertyIndices.Length < properties.Length) {
                Debug.LogError("PropertyIncides array length is too short");
                return;
            }
            var variationalProperties = new MaterialProperty[properties.Length];
            for (var i = 0; i < properties.Length; i++)
                variationalProperties[i] = properties[i].GenerateProperty(propertyIndices[i]);
            Pick(variationalProperties);
        }

        public virtual int CountProperties() { return properties.Length;  }
        public virtual int CountSelections(int propertyIndex) { return properties[propertyIndex].CountValues; }
    }
}
