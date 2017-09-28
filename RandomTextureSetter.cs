using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NonInstancedMaterialProperty {

    public class RandomTextureSetter : MonoBehaviour {
 
        [SerializeField]
        MaterialPicker picker;

        #region Unity
        void OnEnable() {
            var picker = Picker;
            var selections = new int[picker.CountProperties()];
            for (var i = 0; i < selections.Length; i++)
                selections[i] = Random.Range(0, picker.CountSelections(i));
            picker.Pick(selections);
        }
        #endregion

        public MaterialPicker Picker {
            get {
                if (picker == null)
                    picker = GetComponent<MaterialPicker>();
                return picker;
            }
        }
    }
}
