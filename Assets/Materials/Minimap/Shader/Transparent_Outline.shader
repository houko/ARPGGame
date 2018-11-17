// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Outline/Transparent" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" { }
		_SliceTex ("Base (RGB)", 2D) = "white" { }
		_Cube ("Cubemap", CUBE) = "" {}
		_SliceColor ("Main Color", Color) = (.5,.5,.5,1)
		_SliceGuide ("Slice Guide (RGB)", 2D) = "white" {}
		_SliceValue ("Slice Value", Range(0.0, 1)) = 0.5
     	_SliceAmount ("Slice Amount", Range(0.0, 1.5)) = 0.5
		_RimColor ("Rim Color", Color) = (0.97,0.88,1,0.75)
		_RimPower ("Rim Power", Float) = 2.5
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005

	}
	
CGINCLUDE
#include "UnityCG.cginc"

struct appdata {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
};

struct v2f {
	float4 pos : POSITION;
	float4 color : COLOR;
};

uniform float _Outline;
uniform float4 _OutlineColor;

v2f vert(appdata v) {
	// just make a copy of incoming vertex data but scaled according to normal direction
	v2f o;
	o.pos = UnityObjectToClipPos(v.vertex);
	
	float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
	float2 offset = TransformViewToProjection(norm.xy);

	o.pos.xy += offset * o.pos.z * _Outline;
	o.color = _OutlineColor;
	return o;
}
ENDCG

	SubShader {
		//Tags {"Queue" = "Geometry+100" }
		 Tags { "RenderType" = "Transparent" "Queue" = "Transparent" "Queue" = "Geometry+100" }
   		 Blend SrcAlpha OneMinusSrcAlpha

CGPROGRAM
#pragma surface surf Lambert

sampler2D _SliceTex;
fixed4 _SliceColor;
fixed4 _Color;

sampler2D _SliceGuide;
float _SliceAmount;
float _SliceValue;

struct Input {
	float2 uv_MainTex;
	float2 uv_SliceGuide;
    float _SliceAmount;
};

void surf (Input IN, inout SurfaceOutput o) {
    clip(tex2D (_SliceGuide, IN.uv_SliceGuide) - _SliceAmount*_SliceValue);
    //o.Albedo = c.rgb;
    o.Emission = _SliceColor.rgb;
    o.Alpha = _Color.a;
}
ENDCG


CGPROGRAM
#pragma surface surf Lambert

sampler2D _MainTex;
fixed4 _Color;
float4 _RimColor;
float _RimPower;
samplerCUBE _Cube;
sampler2D _SliceGuide;
float _SliceAmount;

struct Input {
	float2 uv_MainTex;
	float3 cubenormal : TEXCOORD1;
	float3 viewDir;
	float3 worldRefl;
	float2 uv_SliceGuide;
    float _SliceAmount;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	float3 cubes = texCUBE (_Cube, IN.worldRefl).rgb;
    half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
    clip(tex2D (_SliceGuide, IN.uv_SliceGuide) - _SliceAmount);
    //o.Albedo = c.rgb;
    o.Emission = (_RimColor.rgb * pow (rim, _RimPower))+(c.rgb)*cubes;
    o.Alpha = c.a;
}
ENDCG


		// note that a vertex shader is specified here but its using the one above
		Pass {
			Name "OUTLINE"
			//Tags { "LightMode" = "Always" }
			Tags {
			"Queue"="AlphaTest" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent"}

			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			//Offset 50,50

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			float _SliceAmount;
			half4 frag(v2f i) :COLOR {
			
				if(_SliceAmount > 0){
					i.color.a = 0;
				
				}else{
					i.color.a = i.color.a;
				}
				return i.color;
			}
			ENDCG
		}
	}
	
	SubShader {
		Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma exclude_renderers gles xbox360 ps3
			ENDCG
			SetTexture [_MainTex] { combine primary }
		}
	}
	
	Fallback "Diffuse"
}
