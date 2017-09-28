using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NonInstancedMaterialProperty {
    
    public abstract class MaterialProperty {
        public readonly string name;
        public abstract object Value { get; }

        public MaterialProperty(string name) {
            this.name = name;
        }

        public abstract void Set(Material mat); 

        public override int GetHashCode() {
            return name.GetHashCode() ^ Value.GetHashCode();
        }
        public override bool Equals(object obj) {
            MaterialProperty b;
            return (b = obj as MaterialProperty) != null && name == b.name && Value == b.Value;
        }
    }

    public class NullProperty : MaterialProperty {
        protected object value;

        public NullProperty(string name, object value) : base(name) {
            this.value = value;
        }

        public override object Value { get { return value; } }
        public override void Set(Material mat) {}
    }

    public class TextureProperty : MaterialProperty {
        protected Texture textureValue;

        public TextureProperty(string name, Texture value) : base(name) {
            this.textureValue = value;
        }

        public override object Value {
            get { return textureValue; }
        }

        public override void Set(Material mat) {
            mat.SetTexture(name, textureValue);
        }
    }
}
