/**
* @file InteractiveWater2D
* @brief Shader 2D realizujący efekt wody
* @details Shader obsługuje proceduralne falowanie, kaustykę, interkację z kroplą wody oraz efekt wiru
* Przeznaczony do materiałów typu Transparent
*/

/// @cond HIDDEN_FROM_DOXYGEN 
Shader "Custom/InteractiveWater2D"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

        [Header(Kolory)]
        _BaseColor ("Glowny Kolor Wody", Color) = (0.1, 0.5, 0.8, 1)
        _CausticColor ("Kolor Jasnych Odblaskow", Color) = (1.0, 1.0, 1.0, 0.5)

        [Header(Ustawienia Wygladu)]
        _Speed ("Predkosc Falowania", Float) = 1.5
        _Scale ("Gestosc Wzoru Wody", Float) = 15.0
        _Distortion ("Falowanie Krawedzi", Float) = 0.05

        [Header(Interakcja Kropla )]
        _DropCenter ("Srodek Kropli (UV)", Vector) = (0.5, 0.5, 0, 0)
        _DropRadius ("Promien Fali", Float) = 0.0
        _DropStrength ("Sila Znieksztalcenia Kropli", Float) = 0.0

        [Header(Interakcja)]
        _DrainCenter ("Srodek Splywu (UV)", Vector) = (0.5, 0.5, 0, 0)
        _DrainStrength ("Sila Wiru", Float) = 0.0
    }
    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha 
        Cull Off 
        ZWrite Off 
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            /// @endcond


            /**
            * @struct appdata_t
            * @brief Struktura danych wejściowych wierzchołka z unity
            */            
            struct appdata_t
            {
                float4 vertex   : POSITION; ///< Lokalna pozycja wierzchołka
                float4 color    : COLOR;    ///< Kolor przypisany do wierzchołka
                float2 texcoord : TEXCOORD0;///< Współrzędne tekstury
            };

            /**
            * @struct v2f
            * @brief Struktura danych przekazywanych z shadera wierzchołków do shadera fragmentów
            */
            struct v2f
            {
                float4 vertex   : SV_POSITION;  ///< Przekształcona pozycja wierzchołka w przestrzeni ekranu
                fixed4 color    : COLOR;        ///< Przekształcony kolor wierzchołka
                float2 texcoord : TEXCOORD0;    ///< Przekazane współrzędne tekstury (UV)
            };

            /// @brief Główna tekstura przekazwaywana przez SpriteRenderer
            sampler2D _MainTex;
            /// @brief Główny, bazowy kolor wody (RGBA)
            fixed4 _BaseColor;
            /// @brief Kolor i intensywność (alpha) jasnych odblasków (kaustyki)
            fixed4 _CausticColor;
            /// @brief Prędkość animacji falowania w czasie
            float _Speed;
            /// @brief Gęstość i skala wzoru wody
            float _Scale;
            /// @brief Siła falowania krawędzi (Vertex displacement)
            float _Distortion;

            /// @brief Współrzędne UV środka uderzenia kropli
            float4 _DropCenter;
            /// @brief Aktualny promień rozchodzącej się fali po kropli
            float _DropRadius;
            /// @brief Siła zniekształcenia obrazu wywołana przez falę
            float _DropStrength;
            
            /// @brief Współrzędne UV środka wiru
            float4 _DrainCenter;
            /// @brief Siła zasysania i obrotu wiru
            float _DrainStrength;

            /**
            * @brief Shader wierzchołkowy (vertex)
            * @details Odpowiada za transforamcję pozycji wierzchołków do przestzreni rzutowania
            * oraz dodaje do nich falowanie na krawędziach (Vertex displacement) zależne od czasu
            * * @param IN Dane wejściowe wierzchołka z silnika
            * @return Zwraca przetowrzoną strukturę 'v2f'
            */
            v2f vert(appdata_t IN)
            {
                v2f OUT;
                
                float time = _Time.y * _Speed;
                float waveX = sin(IN.vertex.y * 5.0 + time) * _Distortion;
                float waveY = cos(IN.vertex.x * 5.0 + time) * _Distortion;
                
                IN.vertex.x += waveX;
                IN.vertex.y += waveY;

                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color;
                return OUT;
            }

            /**
            * @brief Shader fragmentów (Fragment/Pixel shader)
            * @details Oblicza ostateczny kolor każdego piksela. Realizuje zniekształcenia przestszeni UV 
            * dla efektu wiru oraz rozchodzącej się kropli. Generuje proceduralne fale (kaustykę) poprzez 
            * sumowanie funkcji trygonometrycznych
            * * @param IN Dane interpolowane z shadera wierzchołkowego
            * @return Zwraca końcowy kolor piksela (RGBA)
            */
            fixed4 frag(v2f IN) : SV_Target
            {
                float2 uv = IN.texcoord;
                float t = _Time.y * _Speed;

                float2 drainOffset = uv - _DrainCenter.xy;
                float drainDist = length(drainOffset);
                
                float swirlAngle = _DrainStrength * exp(-drainDist * 5.0); 
                float s = sin(swirlAngle);
                float c = cos(swirlAngle);
                
                uv = _DrainCenter.xy + float2(c * drainOffset.x - s * drainOffset.y, s * drainOffset.x + c * drainOffset.y);

                float dropDist = distance(uv, _DropCenter.xy);
                
                float waveRing = 1.0 - smoothstep(0.0, 0.1, abs(dropDist - _DropRadius));
                
                float2 dropDir = normalize(uv - _DropCenter.xy);
                if (dropDist > 0.001) { 
                    uv += dropDir * waveRing * _DropStrength;
                }

                float2 p = uv * _Scale;
                
                float wave1 = sin(p.x + t) * cos(p.y + t * 0.4);
                float wave2 = sin(p.x - t * 0.6) * cos(p.y + t * 0.3);
                float wave3 = (1 / tan(p.y - t * 0.6));
                float combinedWaves = wave1 + wave2 + wave3;

                float caustics = smoothstep(0.4, 0.6, abs(combinedWaves));

                fixed4 texColor = tex2D(_MainTex, uv);
                fixed4 finalColor = _BaseColor * texColor * IN.color;

                finalColor.rgb += _CausticColor.rgb * caustics * _CausticColor.a;
                finalColor.a = _BaseColor.a * texColor.a * IN.color.a;

                float drainHole = smoothstep(0.02, 0.1, drainDist + (1.0 - saturate(_DrainStrength * 0.1)));
                finalColor.a *= drainHole;

                return finalColor;
            }
            /// @cond HIDDEN_FROM_DOXYGEN
            ENDCG
        }
    }
}
/// @endcond