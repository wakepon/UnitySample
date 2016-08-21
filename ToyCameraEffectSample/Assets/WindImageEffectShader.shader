Shader "Hidden/WindImageEffectShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_WindTex ("Wind Texture", 2D) = "white" {}
        _translate ("Translate", Float) = 0
        _density ("Density", Float) = 0
        _windColor ("Wind Color", COLOR) = (1,1,1,1)
        _rotCos ("RotCos", Float) = 0
        _rotSin ("RotSin", Float) = 0
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
				float4 vertex : SV_POSITION;
			};

            Float     _rotCos;
            Float     _rotSin;
            Float     _translate;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				o.uv_wind = v.uv;
				o.uv_wind.y *= _ScreenParams.y / _ScreenParams.x;//アスペクト比の分補正
                o.uv_wind = float2( _rotCos * o.uv_wind.x - _rotSin * o.uv_wind.y, _rotSin * o.uv_wind.x + _rotCos * o.uv_wind.y );//回転行列を掛ける
				o.uv_wind.x += _translate;//移動
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _WindTex;
            Float     _density;
            fixed4    _windColor;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 wind = tex2D(_WindTex, i.uv_wind);
                float ratio = _density * wind.r;
                return lerp( col, _windColor, ratio );
			}
			ENDCG
		}
	}
}

