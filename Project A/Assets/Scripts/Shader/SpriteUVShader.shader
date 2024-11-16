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

                    // UV ��ǥ�� _UVRect ������ �°� ����
                    o.uv = v.uv;

                    return o;
                }

                // UV ��ǥ�� �־��� ����(_UVRect)�� �߽����� �̵��Ͽ� �� ��ǥ ��ȯ
                float2 UVtoPolarInRect(float2 uv, float2 uvCenter)
                {
                    float2 centeredUV = uv - uvCenter; // UV �߽����κ����� ��� ��ǥ
                    float radius = length(centeredUV); // �߽����κ����� �Ÿ� ���
                    float angle = atan2(centeredUV.x, -centeredUV.y); // ���� ���
                    return float2(radius, angle);
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    
                    // _UVRect �߽� ���
                    float2 uvCenter = _UVRect.xy + _UVRect.zw * 0.5;

                    // UV �߽��� �������� �� ��ǥ ��ȯ ����
                    float2 polar = UVtoPolarInRect(i.uv, uvCenter);

                    // ������ [0, 1] ������ ����ȭ�Ͽ� �ð���� ȸ�� ����
                    float normalizedAngle = polar.y / (2 * UNITY_PI) + 0.5;

                    // ��ٿ� �Ѱ踦 �Ѵ� ������ ���� ó��
                    if (normalizedAngle > _Cooldown)
                    {
                        discard;
                    }

                    // �ؽ�ó ���� ���ø� �� ��ȯ
                    fixed4 col = tex2D(_MainTex, i.uv);
                    return col;
                }
                ENDCG
            }
        }
}
