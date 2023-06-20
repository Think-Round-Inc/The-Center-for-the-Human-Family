Shader "Custom/Hologram" {
    Properties{
        _MainTex("Main Texture", 2D) = "white" {}
        _OverlayTex("Overlay Texture", 2D) = "white" {}
        _ScrollSpeed("Scroll Speed", Range(-1.0, 1.0)) = 0.1
        _MainAlpha("Main Alpha", Range(0.0, 1.0)) = 1.0
        _OverlayAlpha("Overlay Alpha", Range(0.0, 1.0)) = 1.0
        _ObjectAlpha("Object Alpha", Range(0.0, 1.0)) = 1.0
    }

        SubShader{
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }

            Pass {
                Blend SrcAlpha OneMinusSrcAlpha
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                sampler2D _OverlayTex;
                float _ScrollSpeed;
                float _MainAlpha;
                float _OverlayAlpha;
                float _ObjectAlpha;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed4 mainColor = tex2D(_MainTex, i.uv);
                    mainColor.a *= _MainAlpha;

                    fixed4 overlayColor = tex2D(_OverlayTex, i.uv + float2(0, _Time.y * _ScrollSpeed));
                    overlayColor.a *= _OverlayAlpha;

                    fixed4 finalColor = lerp(mainColor, overlayColor, overlayColor.a);
                    finalColor.rgb *= finalColor.a; // Multiply RGB by alpha

                    finalColor.a *= _ObjectAlpha; // Apply object transparency

                    // If object alpha is 1, set final alpha to 1 to make the object fully opaque
                    finalColor.a = (_ObjectAlpha >= 1.0) ? 1.0 : finalColor.a;

                    return finalColor;
                }
                ENDCG
            }
        }
}
