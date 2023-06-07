Shader "Custom/SkyboxBlend" {
    Properties {
        _SkyboxTexA ("Skybox Texture A", 2D) = "white" {}
        _SkyboxTexB ("Skybox Texture B", 2D) = "white" {}
        _BlendAmount ("Blend Amount", Range(0, 1)) = 0.5
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
            #include "UnityCG.cginc"
            
            struct appdata {
                float4 vertex : POSITION;
            };
            
            struct v2f {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };
            
            float _BlendAmount;
            sampler2D _SkyboxTexA;
            sampler2D _SkyboxTexB;
            
            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = normalize(mul(unity_ObjectToWorld, v.vertex).xyz);
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target {
                fixed4 skyboxColorA = tex2D(_SkyboxTexA, i.worldPos.xy);
                fixed4 skyboxColorB = tex2D(_SkyboxTexB, i.worldPos.xy);
                fixed4 finalColor = lerp(skyboxColorA, skyboxColorB, _BlendAmount);
                return finalColor;
            }
            ENDCG
        }
    }
}
