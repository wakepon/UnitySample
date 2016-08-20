Shader "Hidden/WindImageEffectShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_WindTex ("Wind Texture", 2D) = "white" {}
		_WindSubTex ("Wind Sub Texture", 2D) = "white" {}
        _translate ("translate", Float) = 0
        _translateSub ("translate sub", Float) = 0
        _rotCos ("rotCos", Float) = 0
        _rotSin ("rotSin", Float) = 0
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
				float2 uv_wind : TEXCOORD1;
				float2 uv_wind_sub : TEXCOORD2;
				float4 vertex : SV_POSITION;
			};

            Float     _rotCos;
            Float     _rotSin;
            Float     _translate;
            Float     _translateSub;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				o.uv_wind = v.uv;
				o.uv_wind.y *= _ScreenParams.y / _ScreenParams.x;
                o.uv_wind = float2( _rotCos * o.uv_wind.x - _rotSin * o.uv_wind.y, _rotSin * o.uv_wind.x + _rotCos * o.uv_wind.y );

				o.uv_wind_sub = o.uv_wind;

				o.uv_wind.x += _translate;
				o.uv_wind_sub.x += _translateSub;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _WindTex;
			sampler2D _WindSubTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 wind = tex2D(_WindTex, i.uv_wind);
				fixed4 wind_sub = tex2D(_WindSubTex, i.uv_wind_sub);
				// just invert the colors
				/* col = 1 - col; */
				/* return col; */
                /* return col + float4( 1.0, 1.0, 1.0, 1.0 ) * wind.a * clamp( 2 * ( wind_sub.r - 0.5 ), 0.0, 1.0 ); */
                return col + float4( 1.0, 1.0, 1.0, 1.0 ) * wind.a * 0.2;
			}
			ENDCG
		}
	}
}

