using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NonInstancedMaterialProperty {

    public class BasePicker : MonoBehaviour {
        [SerializeField] protected Material baseMaterial;

        protected Token<Material> sharedMaterialToken;

        #region Unity
        protected virtual void OnDisable() {
            ReleaseToken();
        }
        #endregion

        public virtual void Pick(params MaterialProperty[] selectedProperties) {
            ReleaseToken();

            var keyData = new object[selectedProperties.Length + 1];
            System.Array.Copy(selectedProperties, 0, keyData, 1, selectedProperties.Length);
            keyData[0] = BaseMaterial;

            var key = new Tuple(keyData);
            sharedMaterialToken = MaterialHolder.Instance.Retain(key, () => GenerateVariation(selectedProperties));
            Apply(sharedMaterialToken.Value);
        }
        
        protected virtual void ReleaseToken() {
            if (sharedMaterialToken != null) {
                sharedMaterialToken.Dispose();
                sharedMaterialToken = null;
            }
        }
        protected virtual Material BaseMaterial {
            get {
                if (baseMaterial == null)
                    baseMaterial = GetComponent<Renderer>().sharedMaterial;
                return baseMaterial;
            }
        }
        protected virtual void Apply(Material mat) {
            var renderer = GetComponent<Renderer>();
            if (renderer != null)
                renderer.sharedMaterial = mat;
        }
        protected virtual Material GenerateVariation(params MaterialProperty[] selectedProperties) {
            var variation = Instantiate(BaseMaterial);
            foreach (var p in selectedProperties)
                p.Set(variation);
            return variation;
        }
    }
}
