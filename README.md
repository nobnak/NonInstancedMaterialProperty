# Material Sharing Tool for GPU Instancing in Unity

## Usage
```C#
BasePicker picker;
if ((picker = GetComponent<BasePicker>()) == null)
  picker = gameObject.AddComponent<BasePicker>();
  
picker.Pick(new TextureProperty("_MainTex", mainTex), new TextureProperty("_SubTex", subTex));
```
