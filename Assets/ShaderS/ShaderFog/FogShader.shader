Shader "UI/FogShader"
{
    Properties
    {
        _FogColor ("Fog Color", Color) = (0,0,0,0.92)
        _PlayerPos ("Player Pos", Vector) = (0.5,0.5,0,0)

        _Radius ("Light Radius", Float) = 0.2
        _Softness ("Softness", Float) = 0.25

        _MaxVision ("Max Vision Distance", Float) = 0.6
        _HardCutoff ("Hard Cutoff Distance", Float) = 0.85
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

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

            fixed4 _FogColor;
            float4 _PlayerPos;

            float _Radius;
            float _Softness;
            float _MaxVision;
            float _HardCutoff;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // korekcja proporcji (ko³o zamiast elipsy)
                float aspect = _ScreenParams.x / _ScreenParams.y;
                uv.x *= aspect;

                float2 playerPos = _PlayerPos.xy;
                playerPos.x *= aspect;

                float dist = distance(uv, playerPos);

                // =========================
                // 1. inner light (œrodek)
                // =========================
                float inner = 1.0 - smoothstep(_Radius, _Radius + _Softness, dist);

                // =========================
                // 2. globalna mg³a (im dalej tym ciemniej)
                // =========================
                float fog = smoothstep(_Radius, _MaxVision, dist);

                // =========================
                // 3. hard cutoff (nag³a ciemnoœæ)
                // =========================
                float hard = step(_HardCutoff, dist);

                // =========================
                // FINAL
                // =========================
                float visibility = inner * (1.0 - fog);

                // po cutoffie pe³na ciemnoœæ
                visibility = lerp(visibility, 0.0, hard);

                fixed4 col = _FogColor;

                // alpha = ile widzisz (0 = czarno, 1 = widocznie)
                col.a = 1.0 - visibility;

                return col;
            }
            ENDCG
        }
    }
}