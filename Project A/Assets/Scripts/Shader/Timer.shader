Shader "Custom/Timer"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Cooldown ("Cooldown", Range(0,1)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
            float _Cooldown;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // This function converts UV coordinates to polar coordinates
            float2 UVtoPolar(float2 uv)
            {
                float2 centeredUV = uv * 2.0 - 1.0; // center UV to (-1, 1)
                float radius = length(centeredUV);
                float angle = atan2( centeredUV.x,centeredUV.y); // returns radians between (-PI, PI)
                return float2(radius, angle);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Get polar coordinates from UV
                float2 polar = UVtoPolar(i.uv);

                // Normalize the angle to [0, 1] (clockwise)
                float angle = polar.y / (2 * UNITY_PI) + 0.5;

                // Check if the angle is within the cooldown threshold
                if (angle > _Cooldown)
                {
                    // Discard the pixel (or you can return a transparent value)
                    discard;
                }

                // Sample the texture and return it
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}