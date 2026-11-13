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
                float3 pos : TEXCOORD1;
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
                v2f output;
                output.vertex = TransformObjectToHClip(v.vertex.xyz);
                output.uv = TRANSFORM_TEX(v.uv, _MainTex);
                output.pos = v.vertex.xyz;
                return output;
            }

            float GridTest(float2 t_uv) {
                float result = 0.0;

                for (float i = 0.0; i <= 1; i += _GridSize) {
                    for (int j = 0; j < 2; j++) {
                        result += 1.0 - smoothstep(0.0, _GridLineThickness, abs(t_uv[j] - i));
                    }
                }

                return result;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 dir = normalize(i.pos);
                float2 sphereUV = float2(degrees(atan2(dir.z, dir.x)) / 360  + 0.5, degrees(asin(dir.y)) / 360 + 0.5);

                float4 gridColour = (_GridColour * GridTest(sphereUV) * _Intensity) + tex2D(_MainTex, i.uv);
                gridColour = float4(gridColour.r, gridColour.g, gridColour.b, 1);
                return float4(gridColour);
            }
            ENDHLSL
        }
    }
}
