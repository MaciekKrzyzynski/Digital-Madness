Shader "Unlit/Grid"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {}
        [HDR]_GridColour ("Grid Colour", Color) = (.255,.0,.0,1)
        _GridSize ("Grid Size", Range(0.01, 1.0)) = 0.1
        _GridLineThickness ("Grid Line Thickness", Range(0.00001, 0.010)) = 0.003
        _Intensity ("Emission Intensity", Range(-5,15)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

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
            float4 _MainTex_ST;
            float4 _GridColour;
            float _GridSize;
            float _GridLineThickness;
            float _Intensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float GridTest(float2 r) {
                float result;

                for (float i = 0.0; i <= 1; i += _GridSize) {
                    for (int j = 0; j < 2; j++) {
                        result += 1.0 - smoothstep(0.0, _GridLineThickness,abs(r[j] - i));
                    }
                }

                return result;
            }

            float4 frag(v2f i) : SV_Target
            {
                float4 gridColour = (_GridColour * GridTest(i.uv) * _Intensity) + tex2D(_MainTex, i.uv);
                gridColour = float4(gridColour.r, gridColour.g, gridColour.b, 1);
                return float4(gridColour);
            }
            ENDHLSL
        }
    }
}