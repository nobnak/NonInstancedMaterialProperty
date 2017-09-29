using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NonInstancedMaterialProperty {

    public class Token<T> : System.IDisposable {
        protected IRetainer retainer;
        public bool Disposed { get; private set; }

        public Token(IRetainer retainer) {
            this.retainer = retainer;
            this.Disposed = false;
        }

        ~Token() {
            if (!Disposed) {
                Dispose();
                Debug.LogError("Call Dispose instead");
            }

        }

        public T Value { get { return retainer.Value; } }
        public void Dispose() {
            if (!Disposed) {
                Disposed = true;
                retainer.Release();
            }
        }

        public interface IRetainer {
            T Value { get; }
            void Release();
        }
    }
}
