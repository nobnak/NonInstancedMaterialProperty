using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace NonInstancedMaterialProperty {

    public class Retainer<T> : System.IDisposable {
        public T Value { get; private set; }

        protected System.Action<T> valueDisposer;
        protected int referenceCount = 0;
        protected bool disposed = false;

        public Retainer(T value, System.Action<T> valueDisposer) {
            this.Value = value;
            this.valueDisposer = valueDisposer;
        }

        public void Dispose() {
            if (!disposed) {
                disposed = true;
                valueDisposer(Value);
            }
        }

        public Token<T> Retain() {
            var t = new Token<T>(new Dash(this));
            ++referenceCount;
            return t;
        }

        protected void Release() {
            if (--referenceCount == 0)
                Dispose();
        }

        public class Dash : Token<T>.IRetainer {
            protected Retainer<T> retainer;

            public Dash(Retainer<T> retainer) {
                this.retainer = retainer;
            }
            public T Value {
                get { return retainer.Value; }
            }

            public void Release() {
                retainer.Release();
            }
        }
    }
}
