
Shader "Custom/RotateUVs" {
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _RotationSpeed("Rotation Speed", Float) = 2.0
    }

    SubShader{
        Blend SrcAlpha One
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        float4x4 _Matrix;
        void vert(inout appdata_full v) {
            v.texcoord.xy = mul(v.texcoord.xy, _Matrix);
            return v.texcoord.xy;
        }

        fixed4 frag(float2 IN) : SV_Target{
            return tex2D(_MainTex, IN.uv_MainTex);
        }


        ENDCG
    }
        FallBack "Diffuse"
}
