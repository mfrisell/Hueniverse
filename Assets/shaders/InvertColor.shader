// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/InvertColor"
{
    Properties
    {
        _Factor ("P value", Range(-6, 6)) = 0
		_Center ("Center", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
       
        GrabPass { }
       
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
 
            #include "UnityCG.cginc"
 
			
			static const float PI = 3.14159265f;

            struct appdata
            {
                float4 vertex : POSITION;
            };
 
            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 uv : TEXCOORD1;
            };
 
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = ComputeGrabScreenPos(o.pos);
                return o;
            }
           
            sampler2D _GrabTexture;
            float4 _GrabTexture_TexelSize;
            float _Factor;
			float4 _Center;
 
            half4 frag (v2f i) : SV_Target
            {
                half4 pixelCol = half4(0, 0, 0, 0);
				pixelCol = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uv)); 
                return 1 - pixelCol;
            }
            ENDCG
        }
    }
}