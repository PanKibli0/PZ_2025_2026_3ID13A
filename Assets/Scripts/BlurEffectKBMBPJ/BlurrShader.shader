Shader "Custom/GlassesBlurShader"
{
    Properties
    {
        _BlurStrength ("Sila Rozmycia", Range(0, 0.05)) = 0.015
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline" }
        
        Pass
        {
            Name "GlassesBlurPass"
            ZWrite Off ZTest Always Blend Off Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D_X(_BlitTexture);
            SAMPLER(sampler_BlitTexture);
            
            float _BlurStrength;

            struct Attributes
            {
                uint vertexID : SV_VertexID;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                
                float x = -1.0 + float((input.vertexID & 1) << 2);
                float y = -1.0 + float((input.vertexID & 2) << 1);
                
                output.positionCS = float4(x, y, 0.0, 1.0);
                output.uv = float2((x + 1.0) * 0.5, (y + 1.0) * 0.5);

                #if UNITY_UV_STARTS_AT_TOP
                output.uv.y = 1.0 - output.uv.y;
                #endif

                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float2 uv = input.uv;
                
                half4 color = half4(0, 0, 0, 0);
                float totalWeight = 0.0;
                
                for (float x = -3.0; x <= 3.0; x += 1.0)
                {
                    for (float y = -3.0; y <= 3.0; y += 1.0)
                    {
                        float2 offset = float2(x, y) * _BlurStrength;
                        
                        float weight = exp(-(x*x + y*y) / 4.0);

                        color += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_BlitTexture, uv + offset) * weight;
                        
                        totalWeight += weight;
                    }
                }

                return color / totalWeight;
            }
            ENDHLSL
        }
    }
}