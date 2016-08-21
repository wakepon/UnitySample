Shader "Hidden/BlurEffectConeTap" {
	Properties { _MainTex ("", any) = "" {} }
	CGINCLUDE
	#include "UnityCG.cginc"
	struct v2f {
		float4 pos : SV_POSITION;
		half2 taps[4] : TEXCOORD1; 
	};
	sampler2D _MainTex;
	half4 _MainTex_TexelSize;
	half4 _BlurOffsets;
	v2f vert( appdata_img v ) {
		v2f o; 
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

		half2 uv = v.texcoord - _BlurOffsets.xy * _MainTex_TexelSize.xy;

        //Offset分ずらしてタップ
		o.taps[0] = uv + _MainTex_TexelSize * _BlurOffsets.xy;
		o.taps[1] = uv - _MainTex_TexelSize * _BlurOffsets.xy;
		o.taps[2] = uv + _MainTex_TexelSize * _BlurOffsets.xy * half2(1,-1);
		o.taps[3] = uv - _MainTex_TexelSize * _BlurOffsets.xy * half2(1,-1);
		return o;
	}
	half4 frag(v2f i) : SV_Target {
        //ずらした位置の色を足して4で割る。
		half4 color = tex2D(_MainTex, i.taps[0]);
		color += tex2D(_MainTex, i.taps[1]);
		color += tex2D(_MainTex, i.taps[2]);
		color += tex2D(_MainTex, i.taps[3]);
		return color * 0.25;
	}
	ENDCG
	SubShader {
		 Pass {
			  ZTest Always Cull Off ZWrite Off

			  CGPROGRAM
			  #pragma vertex vert
			  #pragma fragment frag
			  ENDCG
		  }
	}
	Fallback off
}
