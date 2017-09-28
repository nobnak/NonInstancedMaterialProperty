using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NonInstancedMaterialProperty {

    [System.Serializable]
    public class Selector {
        public enum ValueTypeEnum { Texture = 0 }

        public string name;
        public ValueTypeEnum valueType;

        public Texture[] textureValues;

        public MaterialProperty GenerateProperty(int valueIndex) {
            return new TextureProperty(name, textureValues[valueIndex]);
        }
        public int CountValues {
            get {
                return textureValues.Length;
            }
        }
    }
}
