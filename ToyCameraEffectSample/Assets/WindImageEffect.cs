using UnityEngine;

public class WindImageEffect : CustomImageEffect
{
    [SerializeField]
    [Range(0, 360)]
    private float rotation;

    [SerializeField]
    private float translateSpeed = 2.0f;

    [SerializeField]
    [Range(0, 2.0f)]
    private float density = 0.4f;

    [SerializeField]
    private Color windColor = Color.white;

    private float translate = 0.0f;

    public override string ShaderName
    {
        get { return "Hidden/WindImageEffectShader"; }
    }

    void Update()
    {
        translate += translateSpeed * Time.deltaTime;
    }

    protected override void UpdateMaterial()
    {
        float rad = Mathf.Deg2Rad * rotation;
        //shader側に値を渡す
        Material.SetFloat( "_translate", translate );
        Material.SetFloat( "_density", density );
        Material.SetColor( "_windColor", windColor );
        Material.SetFloat( "_rotCos", Mathf.Cos( rad ) );
        Material.SetFloat( "_rotSin", Mathf.Sin( rad ) );
    }
}
