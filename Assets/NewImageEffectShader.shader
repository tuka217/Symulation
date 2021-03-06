﻿Shader "Hidden/NewImageEffectShader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_DefaultColor ("Default color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MaterialPosition ("Material position", Float) = -0.5
		_TextureOffset ("Texture offset", Float) = -1.22
		_Size ("Size", Float) = 0.5
		_Scale ("Scale", Float) = 100.0
	}
	SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

			uniform float4 _DefaultColor;
			uniform float _MaterialPosition;
			uniform float _TextureOffset;
			uniform float _Size;
			uniform float _Scale;
			uniform float4 _Colors[100];
			uniform float _PositionsX[100];

            struct vertexInput {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
            };

            struct fragmentInput{
                float4 position : SV_POSITION;
                float4 texcoord0 : TEXCOORD0;
            };

            fragmentInput vert(vertexInput i){
                fragmentInput o;
                o.position = mul (UNITY_MATRIX_MVP, i.vertex);
                o.texcoord0 = i.texcoord0;
                return o;
            }

            fixed4 frag(fragmentInput input) : SV_Target {
				if(abs(input.texcoord0.x + _MaterialPosition - _PositionsX[input.texcoord0.x]) < _Size) {
					return _Colors[input.texcoord0.x*_Scale + _TextureOffset];
				} else {
					return _DefaultColor;
				}
            }
            ENDCG
        }
    }
}
