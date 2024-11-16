Shader "Custom/SpriteCooldownShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _UVRect("UV Rect", Vector) = (0, 0, 1, 1) // x, y, width, height
        _Cooldown("Cooldown", Range(0,1)) = 1.0
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" }
            LOD 100
            Blend SrcAlpha OneMinusSrcAlpha

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
                    float4 pos : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _UVRect;
                float _Cooldown;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);

                    // UV 좌표를 _UVRect 영역에 맞게 조정
                    o.uv = v.uv;

                    return o;
                }

                // UV 좌표를 주어진 영역(_UVRect)의 중심으로 이동하여 극 좌표 변환
                float2 UVtoPolarInRect(float2 uv, float2 uvCenter)
                {
                    float2 centeredUV = uv - uvCenter; // UV 중심으로부터의 상대 좌표
                    float radius = length(centeredUV); // 중심으로부터의 거리 계산
                    float angle = atan2(centeredUV.x, -centeredUV.y); // 각도 계산
                    return float2(radius, angle);
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    
                    // _UVRect 중심 계산
                    float2 uvCenter = _UVRect.xy + _UVRect.zw * 0.5;

                    // UV 중심을 기준으로 극 좌표 변환 수행
                    float2 polar = UVtoPolarInRect(i.uv, uvCenter);

                    // 각도를 [0, 1] 범위로 정규화하여 시계방향 회전 적용
                    float normalizedAngle = polar.y / (2 * UNITY_PI) + 0.5;

                    // 쿨다운 한계를 넘는 각도는 숨김 처리
                    if (normalizedAngle > _Cooldown)
                    {
                        discard;
                    }

                    // 텍스처 색상 샘플링 후 반환
                    fixed4 col = tex2D(_MainTex, i.uv);
                    return col;
                }
                ENDCG
            }
        }
}
