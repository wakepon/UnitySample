using UnityEngine;

public class ToyCameraImageEffect : MonoBehaviour
{
    private Material material;
    private Material blurMaterial;

    //ぼかし回数
    [Range(0,10)]
    public int iterations = 3;

    //ブラーのずらし量
    [Range(0.0f,1.0f)]
    public float blurSpread = 0.6f;

    protected virtual void Awake()
    {
        {
            string shaderName = "Hidden/ToyCameraImageEffectShader";
            Shader shader = Shader.Find( shaderName );
            Debug.Assert( shader != null );
            material = new Material( shader );
        }

        {
            string shaderName = "Hidden/BlurEffectConeTap";
            Shader shader = Shader.Find( shaderName );
            Debug.Assert( shader != null );
            blurMaterial = new Material( shader );
        }
    }

    void Update()
    {
    }

    // Performs one blur iteration.
    void fourTapCone (RenderTexture src, RenderTexture dest, int iteration)
    {
        float off = 0.5f + iteration*blurSpread;
        Graphics.BlitMultiTap (src, dest, blurMaterial,
                new Vector2(-off, -off),
                new Vector2(-off,  off),
                new Vector2( off,  off),
                new Vector2( off, -off)
                );
    }

    // Downsamples the texture to a quarter resolution.
    void downSample4x (RenderTexture src, RenderTexture dest)
    {
        float off = 1.0f;
        Graphics.BlitMultiTap (src, dest, blurMaterial,
                new Vector2(-off, -off),
                new Vector2(-off,  off),
                new Vector2( off,  off),
                new Vector2( off, -off)
                );
    }

    void OnRenderImage( RenderTexture src, RenderTexture dest )
    {
        //ぼかしの画を作るために4分の一サイズの小さい一時バッファを作ります。
        int rtW = src.width/4;
        int rtH = src.height/4;
        RenderTexture buffer = RenderTexture.GetTemporary(rtW, rtH, 0);

        downSample4x (src, buffer);

        // Blur the small texture
        for(int i = 0; i < iterations; i++)
        {
            RenderTexture buffer2 = RenderTexture.GetTemporary(rtW, rtH, 0);
            fourTapCone (buffer, buffer2, i);
            RenderTexture.ReleaseTemporary(buffer);
            buffer = buffer2;
        }

        material.SetTexture( "_BlurTex", buffer );

        /* Graphics.Blit( buffer, dest ); */
        Graphics.Blit( src, dest, material );

        RenderTexture.ReleaseTemporary(buffer);
    }

}
