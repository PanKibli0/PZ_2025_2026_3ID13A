
Shader "StaticTV"
{
    Properties
    {
        _MainTex ("Base Texture (Optional)", 2D) = "black" {}
        _NoiseScale ("Noise Scale (Resolution)", Float) = 10.0
        _Speed ("Flicker Speed", Float) = 10.0
        _ColorTint ("Tint Color", Color) = (1, 1, 1, 1)
        _Intensity ("Static Intensity", Range(0, 1)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

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
            
            float _NoiseScale;
            float _Speed;
            float4 _ColorTint;
            float _Intensity;

            float random(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453123);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 baseColor = tex2D(_MainTex, i.uv);
                float2 noiseUV = i.uv * _NoiseScale;
                float timeOffset = _Time.y * _Speed;
                float noiseVal = random(noiseUV + float2(timeOffset, -timeOffset));
                fixed4 staticColor = fixed4(noiseVal, noiseVal, noiseVal, 1.0) * _ColorTint;
                return lerp(baseColor, staticColor, _Intensity);
            }
            ENDCG
        }
    }
}

