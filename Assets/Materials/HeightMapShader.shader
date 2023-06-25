Shader "Custom/HeightMapShader"
 {
     Properties
     {
         _MainTex ("Albedo (RGB)", 2D) = "white" {}
         _MinHeight("Min Height", Float) = 0.4
         _MaxHeight("Max Height", Float) = 1
     }
     SubShader
     {
         Tags { "RenderType"="Opaque" }
         LOD 200
 
         CGPROGRAM
         // Physically based Standard lighting model, and enable shadows on all light types
         #pragma surface surf Standard fullforwardshadows
 
         // Use shader model 3.0 target, to get nicer looking lighting
         #pragma target 3.0
 
         struct Input
         {
             float2 uv_MainTex;
             float3 worldPos;
         };
         
         sampler2D _MainTex;
         half _MaxHeight;
         half _MinHeight;
 
         // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
         // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
         // #pragma instancing_options assumeuniformscaling
         UNITY_INSTANCING_BUFFER_START(Props)
             // put more per-instance properties here
         UNITY_INSTANCING_BUFFER_END(Props)
 
         void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Remap worldPos.y to go from [0,1] between _MinHeight and _MaxHeight
            float z = (IN.worldPos.z - _MinHeight) / (_MaxHeight - _MinHeight);
            float2 uv = float2 (z, 0);
            fixed4 c = tex2D (_MainTex, uv);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
         ENDCG
     }
     FallBack "Diffuse"
 }