Shader "Custom/WaterWithAlgae" {
    Properties {
        _MainTex ("Main Texture", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _AlgaeTex ("Algae Texture", 2D) = "white" {}
        _AlgaeColor ("Algae Color", Color) = (0.0, 1.0, 0.0, 1.0)
        _AlgaeDensity ("Algae Density", Range(0.0, 1.0)) = 0.5
        _WaveSpeed ("Wave Speed", Range(0.0, 5.0)) = 1.0
    }

    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        Pass {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv_MainTex : TEXCOORD0;
                float2 uv_NormalMap : TEXCOORD1;
            };

            struct v2f {
                float2 uv_MainTex : TEXCOORD0;
                float2 uv_NormalMap : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _NormalMap;
            sampler2D _AlgaeTex;
            fixed4 _AlgaeColor;
            float _AlgaeDensity;
            float _WaveSpeed;

            v2f vert(appdata v) {
                v2f o;
                o.uv_MainTex = v.uv_MainTex;
                o.uv_NormalMap = v.uv_NormalMap;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            // fixed3 perturb(fixed3 normal, fixed3x3 tangentToWorld, float2 ddx_uv, float2 ddy_uv) {
            //     fixed3x3 worldToTangent = transpose(tangentToWorld);
            //     fixed3 dx = ddx(normal);
            //     fixed3 dy = ddy(normal);
            //     fixed3 dxW = mul(worldToTangent, dx);
            //     fixed3 dyW = mul(worldToTangent, dy);
            //     fixed2 grad = float2(dot(dxW, dxW), dot(dyW, dyW));
            //     fixed3x3 TBN = fixed3x3(normal, dx, dy);
            //     fixed3x3 invTBN = transpose(TBN);
            //     return mul(invTBN, normalize(mul(TBN, normal) + (grad.x * ddx_uv + grad.y * ddy_uv)));
            // }

            fixed4 frag(v2f IN) : SV_Target {
                fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex);
                fixed4 algaeColor = tex2D(_AlgaeTex, IN.uv_MainTex);
                //fixed3 normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap)).rgb;
                //fixed4 finalNormal = fixed4(normal, 1.0);
                
                // Apply algae color and density
                texColor.rgb = lerp(texColor.rgb, algaeColor.rgb * _AlgaeColor.rgb, _AlgaeColor.a * _AlgaeDensity);

                // Apply normal mapping with perturb function
                IN.uv_NormalMap.xy = IN.uv_NormalMap.xy * 2.0 - 1.0;
                //o.Normal = perturb(finalNormal.rgb, UnityObjectToWorldDir(IN.uv_NormalMap), ddx(IN.uv_NormalMap), ddy(IN.uv_NormalMap));

                // Apply wave animation
                float waveFactor = sin(_Time.y * _WaveSpeed + IN.uv_MainTex.x * 10.0 + IN.uv_MainTex.y * 10.0);
                //o.Normal += normalize(float3(waveFactor, 0.0, waveFactor));

                fixed3 finalAlbedo = texColor.rgb;
                fixed4 finalColor = fixed4(finalAlbedo, texColor.a);
                return finalColor;
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
