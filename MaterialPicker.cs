using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NonInstancedMaterialProperty {

    public class MaterialPicker : MonoBehaviour {
        [SerializeField]
        Property[] properties;
        [SerializeField]
        Material baseMaterial;

        Token<Material> sharedMaterialToken;

        #region Unity
        private void OnDisable() {
            if (sharedMaterialToken != null) {
                sharedMaterialToken.Dispose();
                sharedMaterialToken = null;
            }
        }
        #endregion

        public void Pick(params int[] propertyIndices) {
            if (propertyIndices.Length < properties.Length) {
                Debug.LogError("PropertyIncides array length is too short");
                return;
            }

            var values = new object[properties.Length];
            for (var i = 0; i < properties.Length; i++)
                values[i] = properties[i].Get(propertyIndices[i]);

            var key = new Tuple(values);
            sharedMaterialToken = MaterialHolder.Instance.Retain(key, () => GenerateVariation(propertyIndices));
            Apply(sharedMaterialToken.Value);
        }

        public int CountProperties() { return properties.Length; }
        public int CountSelections(int propertyIndex) { return properties[propertyIndex].CountValues; }

        protected Material BaseMaterial {
            get {
                if (baseMaterial == null)
                    baseMaterial = GetComponent<Renderer>().sharedMaterial;
                return baseMaterial;
            }
        }
        protected void Apply(Material mat) {
            var renderer = GetComponent<Renderer>();
            if (renderer != null)
                renderer.sharedMaterial = mat;
        }
        protected Material GenerateVariation(params int[] propertyIndices) {
            var variation = Instantiate(BaseMaterial);
            for (var i = 0; i < propertyIndices.Length; i++)
                properties[i].Set(variation, propertyIndices[i]);
            return variation;
        }
    }
}
