﻿Shader "Custom/TextureShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
		_Color("Color", Color) = (255,255,255,255)
    }
    
	SubShader
	{
		Pass
		{
			CGPROGRAM

			//Function Defines

			#pragma vertex vert

			#pragma fragment frag

			//Includes

			#include"UnityCG.cginc"

			//Structures

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			//Imports

			float4 _Color;
			sampler2D _MainTex;

			//Vertex Function

			v2f vert(appdata IN)
			{
				v2f OUT;

				OUT.pos = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;

				return OUT;
			}

			//Fragment Function

			fixed4 frag(v2f IN) : SV_Target
			{
				float4 texColor = tex2D(_MainTex, IN.uv);
				return texColor *  _Color;
			}

			ENDCG
		}
	}
}