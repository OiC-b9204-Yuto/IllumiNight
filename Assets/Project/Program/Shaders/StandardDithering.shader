Shader "Custom/Standard Dithering"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Smoothness("Roughness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _BayerTex ("BayerTex", 2D) = "black" {}
        _BlockSize ("BlockSize", int) = 4
        _Radius ("Radius", Range(0.001, 100)) = 10
    }

        SubShader
        {
            Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}

            CGPROGRAM

            #pragma surface surf Standard
            #pragma target 3.0

            sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
            float4 screenPos;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        sampler2D _BayerTex;
        float _BlockSize;
        float _Radius;

        void surf(Input i, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D(_MainTex, i.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;

            // カメラからの距離
            float dist = distance(i.worldPos, _WorldSpaceCameraPos);
            // 領域内で0~1の距離にClamp
            float clamp_distance = saturate(dist / _Radius);
            // BlockSizeピクセル分でBayerMatrixの割り当て
            float2 uv_BayerTex = (i.screenPos.xy / i.screenPos.w) * (_ScreenParams.xy / _BlockSize);
            // BayerMatrixから閾値をとってくる
            float threshold = tex2D(_BayerTex, uv_BayerTex).r;

            //discard pixels accordingly
            clip(clamp_distance - threshold);
        }
        ENDCG
    }
    FallBack "Standard"
}