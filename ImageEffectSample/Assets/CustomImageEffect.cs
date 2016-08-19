using UnityEngine;

[RequireComponent(typeof(Camera))]
public abstract class CustomImageEffect : MonoBehaviour
{
    private Material material;

    public abstract string ShaderName { get; }

    protected Material Material { get { return material; } }

    protected virtual void Awake()
    {
        Shader shader = Shader.Find( ShaderName );
        Debug.Assert( shader != null );

        material = new Material( shader );
    }

    protected virtual void OnRenderImage( RenderTexture src, RenderTexture dest )
    {
        UpdateMaterial();

        Graphics.Blit( src, dest, material );
    }

    protected abstract void UpdateMaterial();
}
