using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NonInstancedMaterialProperty {

    public class MaterialHolder {

        protected Dictionary<Tuple, Retainer<Material>> retainedMaterials;

        public MaterialHolder() {
            retainedMaterials = new Dictionary<Tuple, Retainer<Material>>();
        }
        
        public Token<Material> Retain(Tuple key, System.Func<Material> valueGenerator) {
            Retainer<Material> ret;
            if (!retainedMaterials.TryGetValue(key, out ret))
                ret = retainedMaterials[key] = new Retainer<Material>(valueGenerator(), GenerateDisposer(key));
            return ret.Retain();
        }

        protected System.Action<Material> GenerateDisposer(Tuple key) {
            return (mat) => {
                ObjectDestructor.Destroy(mat);
                retainedMaterials.Remove(key);
            };
        }
    }
}
