Shader "Custom/Health bar"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (0, 0.5, 1, 1)
        _BackgroundColor ("Background Color", Color) = (0.2, 0.2, 0.2, 1)
        _Health ("Health", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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

            sampler2D _MainTex;
            float4 _MainColor;
            float4 _BackgroundColor;
            float _Health;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                if (i.uv.x < _Health)
                {
                    return _MainColor;
                }
                
                return _BackgroundColor;
            }
            ENDCG
        }
    }
}