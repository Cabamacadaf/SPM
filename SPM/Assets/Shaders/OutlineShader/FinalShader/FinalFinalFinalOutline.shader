﻿Shader "Custom/FinalFinalFinalOutline"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        //_Metallic ("Metallic", Range(0,1)) = 0.0

		_EmissionColor("Emission Color", Color) = (0,0,0)
		_EmissionMap("Emission", 2D) = "white" { }

		_OutlineWidth("Outline Width", Range(1.0, 10.0)) = 1.1

		_BlurRadius("Outline Blur Radius", Range(0.0, 20.0)) = 1
		_Intensity("Outline Blur Intensity", Range(0.0, 1.0)) = 0.01

		_DistortColor("Outline Color", Color) = (1,1,1,1)
		_BumpAmt("Outline Distortion", Range(0,128)) = 10
		_DistortTex("Outline Distort Texture (RGB)", 2D) = "white" {}
		_DistortBumpMap("Outline Distort Bump Map", 2D) = "bump" {}
    }

    SubShader
    {
		Tags
		{
			"Queue" = "Transparent"
		}

		GrabPass{}
		UsePass "Custom/OutlineDistort/OUTLINEDISTORT"
		GrabPass{}
		UsePass "Custom/OutlineBlur/OUTLINEHORIZONTALBLUR"
		GrabPass{}
		UsePass "Custom/OutlineBlur/OUTLINEVERTICALBLUR"

        Tags { "RenderType"="Fade" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _EmissionMap;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_EmissionMap;
        };

        half _Glossiness;
        //half _Metallic;
        fixed4 _Color;
		fixed4 _EmissionColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
			//o.Emission = _EmissionColor * tex2D(_EmissionMap, IN.uv_EmissionMap).a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
