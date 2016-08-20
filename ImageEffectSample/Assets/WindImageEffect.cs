using UnityEngine;

public class WindImageEffect : CustomImageEffect
{
    [SerializeField]
    [Range(0, 360)]
    private float rotation;

    private float translate = 0.0f;
    private float translateSub = 0.0f;

    [SerializeField]
    private float translateSpeed = 2.0f;

    [SerializeField]
    private float translateSubSpeed = 0.5f;

    public override string ShaderName
    {
        get { return "Hidden/WindImageEffectShader"; }
    }

    void Update()
    {
        translate += translateSpeed * Time.deltaTime;
        translateSub += translateSubSpeed * Time.deltaTime;
    }

    protected override void UpdateMaterial()
    {
        float rad = Mathf.Deg2Rad * rotation;
        Material.SetFloat( "_rotCos", Mathf.Cos( rad ) );
        Material.SetFloat( "_rotSin", Mathf.Sin( rad ) );
        Material.SetFloat( "_translate", translate );
        Material.SetFloat( "_translateSub", translateSub );
    }
}
