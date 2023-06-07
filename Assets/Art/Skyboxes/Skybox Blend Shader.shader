Shader "Custom/SkyboxBlend" {
    Properties {
        _SkyboxTexA ("Skybox Texture A", 2D) = "white" {}
        _SkyboxTexB ("Skybox Texture B", 2D) = "white" {}
        _BlendAmount ("Blend Amount", Range(0, 1)) = 0.5
        _HueShift ("Hue Shift", Range(0, 360)) = 0
        _Rotation ("Rotation", Range(0, 360)) = 0
        _Exposure ("Exposure", Range(0, 10)) = 1.0
    }
    
    SubShader {
        Tags { "RenderType"="Background" }
        Lighting Off
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            
            struct appdata {
                float4 vertex : POSITION;
            };
            
            struct v2f {
                float4 vertex : SV_POSITION;
                float3 viewDir : TEXCOORD0;
                float3 rotatedDir : TEXCOORD1;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            sampler2D _SkyboxTexA;
            sampler2D _SkyboxTexB;
            float _BlendAmount;
            float _HueShift;
            float _Rotation;
            float _Exposure;
            
            v2f vert(appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.viewDir = normalize(mul(unity_ObjectToWorld, v.vertex).xyz - _WorldSpaceCameraPos.xyz);
                
                float rot = _Rotation * 3.14159 / 180;
                float3x3 rotationMatrix = float3x3(
                    cos(rot), 0, sin(rot),
                    0, 1, 0,
                    -sin(rot), 0, cos(rot)
                );
                o.rotatedDir = mul(rotationMatrix, o.viewDir);
                
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target {
                float3 dir = normalize(i.rotatedDir);
                float3 sphereCoords = float3(
                    atan2(dir.x, dir.z) / (2 * 3.14159) + 0.5,
                    acos(dir.y) / 3.14159,
                    0
                );
                sphereCoords.y = 1 - sphereCoords.y;
                
                UNITY_SETUP_INSTANCE_ID(i);
                fixed4 skyboxColorA = tex2D(_SkyboxTexA, sphereCoords.xy);
                fixed4 skyboxColorB = tex2D(_SkyboxTexB, sphereCoords.xy);
                fixed4 finalColor = lerp(skyboxColorA, skyboxColorB, _BlendAmount);

                float hueRot = _HueShift * 3.14159 / 180;
                float3x3 hueRotationMatrix = float3x3(
                    cos(hueRot), 0, sin(hueRot),
                    0, 1, 0,
                    -sin(hueRot), 0, cos(hueRot)
                );
                finalColor.rgb = mul(hueRotationMatrix, finalColor.rgb);

                finalColor.rgb *= _Exposure;

                return finalColor;
            }
            ENDCG
        }
    }
}
