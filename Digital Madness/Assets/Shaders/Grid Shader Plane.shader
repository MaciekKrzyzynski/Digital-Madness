Shader "Unlit/Grid"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {}
        _GridColour ("Grid Colour", Color) = (.255,.0,.0,1)
        _GridColour_2("Grid Colour 2", Color) = (.255,.0,.0,1)
        _GridColour_3 ("Grid Colour 3", Color) = (.255,.0,.0,1)
        _GridColour_4("Grid Colour 4", Color) = (.255,.0,.0,1)
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
            float4 _GridColour_2;
            float4 _GridColour_3;
            float4 _GridColour_4;
            float _GridSize;
            float _GridLineThickness;
            float _Intensity;

            v2f vert (appdata v)
            {
                v2f output;
                output.vertex = TransformObjectToHClip(v.vertex.xyz);
                output.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return output;
            }

            float CreateGrid(float2 t_uv) { 
                float result = 0.0;

                for (float i = 0.0; i <= 1; i += _GridSize) 
                {
                    for (int j = 0; j < 2; j++) // Do for both Axis
                    {
                        result += 1.0 - smoothstep(0.0, _GridLineThickness * (abs(sin(_Time.y / 5.0)) * 2.0 + 1.0), abs(t_uv[j] - i));
                    }
                }

                return result;
            }

            float4 frag(v2f i) : SV_Target
            {

                float4 gradientColor1 = lerp(_GridColour, _GridColour_2, smoothstep(-1, 1, i.uv.y));
                float4 gradientColor2 = lerp(_GridColour_3, _GridColour_4, smoothstep(-1, 1, i.uv.y));
                float4 gradientColorFull = lerp(gradientColor1, gradientColor2, smoothstep(-1, 1, i.uv.x));
                float4 gridColour = (gradientColorFull * CreateGrid(i.uv) * _Intensity) ;
               
                return gridColour;
            }
            ENDHLSL
        }
    }
}