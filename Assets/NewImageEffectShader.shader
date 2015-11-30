Shader "Hidden/NewImageEffectShader"
{
	Properties
	{
		_DefaultColor ("DefaultColor", Color) = (1.0, 1.0, 1.0, 1.0)
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_X ("X", Float) = 0
		_Color1 ("Color1", Color) = (1.0, 1.0, 1.0, 1.0)
		_X1 ("X1", Float) = 0
		_Color2 ("Color2", Color) = (1.0, 1.0, 1.0, 1.0)
		_X2 ("X2", Float) = 0
		_Color3 ("Color3", Color) = (1.0, 1.0, 1.0, 1.0)
		_X3 ("X3", Float) = 0
		_Color4 ("Color4", Color) = (1.0, 1.0, 1.0, 1.0)
		_X4 ("X4", Float) = 0
		_Resolution ("Resolution", Float) = 0
	}
	SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

			uniform float4 _DefaultColor;
			uniform float4 _Color;
			uniform float _X;
			uniform float4 _Color1;
			uniform float _X1;
			uniform float4 _Color2;
			uniform float _X2;
			uniform float4 _Color3;
			uniform float _X3;
			uniform float4 _Color4;
			uniform float _X4;
			uniform float _Resolution;

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

            fixed4 frag(fragmentInput i) : SV_Target {
				if(abs(i.position.x - _X) < 100/_Resolution) {
					return _Color;
				} else if(abs(i.position.x - _X1) < 100/_Resolution) {
					return _Color1;
				} else if(abs(i.position.x - _X2) < 100/_Resolution) {
					return _Color2;
				} else if(abs(i.position.x - _X3) < 100/_Resolution) {
					return _Color3;
				} else if(abs(i.position.x - _X4) < 100/_Resolution) {
					return _Color4;
				} 
                else {
					return _DefaultColor;
				}
            }
            ENDCG
        }
    }
}
