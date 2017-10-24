using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NonInstancedMaterialProperty {

    public class SelectionalPicker : BasePicker {
        [SerializeField]
        protected Selector[] properties;

        public void Pick(params int[] propertyIndices) {
            var variationalProperties = new MaterialProperty[properties.Length];
            var pairCount = Mathf.Min(propertyIndices.Length, properties.Length);

            var p = default(int);
            for (var i = 0; i < pairCount; i++) {
                p = propertyIndices[i];
                variationalProperties[i] = properties[i].GenerateProperty(p);
            }

            for (var i = pairCount; i < properties.Length; i++)
                variationalProperties[i] = properties[i].GenerateProperty(p);


            Pick(variationalProperties);
        }

        public virtual int CountProperties() { return properties.Length;  }
        public virtual int CountSelections(int propertyIndex) { return properties[propertyIndex].CountValues; }
    }
}
