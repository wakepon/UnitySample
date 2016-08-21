﻿Shader "Hidden/ToyCameraImageEffectShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BlurTex ("Blur Texture", 2D) = "white" {}
		_FilterTex ("Filter Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
        Fog { Mode Off }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _BlurTex;
			sampler2D _FilterTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 org = tex2D(_MainTex, i.uv);
				fixed4 blur = tex2D(_BlurTex, i.uv);
				fixed4 filter = tex2D(_FilterTex, i.uv);
                return lerp( org, blur, filter.r );
			}
			ENDCG
		}
	}
}

