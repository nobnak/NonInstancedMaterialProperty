using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NonInstancedMaterialProperty {

    public class RandomTextureSetter : MonoBehaviour {
 
        [SerializeField]
        protected SelectionalPicker picker;

        #region Unity
        protected virtual void OnEnable() {
            var picker = Picker;
            var selections = new int[picker.CountProperties()];
            for (var i = 0; i < selections.Length; i++)
                selections[i] = Random.Range(0, picker.CountSelections(i));
            picker.Pick(selections);
        }
        #endregion

        public virtual SelectionalPicker Picker {
            get {
                if (picker == null)
                    picker = GetComponent<SelectionalPicker>();
                return picker;
            }
        }
    }
}
