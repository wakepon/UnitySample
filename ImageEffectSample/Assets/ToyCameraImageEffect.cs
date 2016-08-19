using UnityEngine;

public class ToyCameraImageEffect : CustomImageEffect
{
    [SerializeField]
    [Range(1, 100)]
    private float m_Size;

    public override string ShaderName
    {
        get { return "Hidden/ToyCameraImageEffectShader"; }
    }

    protected override void UpdateMaterial()
    {
        /* Material.SetFloat("_Size", m_Size); */
    }
}
