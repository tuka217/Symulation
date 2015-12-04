Shader "Custom/CycleColor" {
     Properties {
         _Color ("Color1", Color) = ( 1.0, 0.0, 0.0, 1.0 )
         _Color2 ("Color2", Color) = ( 0.0, 0.0, 1.0, 1.0 )
         _Speed ("Speed", float) = 0.2
     }
        SubShader {
         Pass {
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
 
             #include "UnityCG.cginc"
             
             uniform fixed4 _Color;
             uniform fixed4 _Color2;
             uniform float  _Speed;
 
             struct vertOut {
                 float4 pos : SV_POSITION;
                 fixed4 color : COLOR0;
             };
 
             vertOut vert(appdata_base v) {
                 vertOut o;
                 o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
                 o.color = lerp(_Color, _Color2, abs(fmod(_Time.a * _Speed, 2.0) - 1.0));
                 return o; 
             }
 
             fixed4 frag(vertOut i) : COLOR0 {
                    return i.color;
             }
 
             ENDCG
         }
     }
 }    