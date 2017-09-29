# Material Sharing Tool for GPU Instancing in Unity

## Usage
```C#
BasePicker picker;
if ((picker = GetComponent<BasePicker>()) == null)
  picker = gameObject.AddComponent<BasePicker>();
  
picker.Pick(new TextureProperty("_MainTex", mainTex), new TextureProperty("_SubTex", subTex));
```

## Necessity
> Do not put non-instanced properties in the MaterialPropertyBlock, because this disables instancing. Instead, create different Materials for them.
[GPU instancing](https://docs.unity3d.com/Manual/GPUInstancing.html)
