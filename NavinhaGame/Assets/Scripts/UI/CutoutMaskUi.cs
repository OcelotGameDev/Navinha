using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CutoutMaskUi : Image
{
    private static readonly int stencilComp = Shader.PropertyToID("_StencilComp");

    public override Material materialForRendering
    {
        get
        {
            Material material = new Material(base.materialForRendering);
            material.SetInt(stencilComp, (int)CompareFunction.NotEqual);
            return material;
        }
    } 
}
