Shader "Custom/LitShader_URP_HDRP"
{
    Properties
    {
        _ColorMap ("Color Map", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _NormalStrength ("Normal Strength", Range(0,2)) = 1.0
        _Metallic ("Metallic", Range(0,1)) = 0.5
        _MetallicStrength ("Metallic Strength", Range(0,2)) = 1.0
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _SmoothnessStrength ("Smoothness Strength", Range(0,2)) = 1.0
        [Slider] _SmoothnessSlider ("Smoothness Slider", Range(0,1)) = 0.5
    }
    
    SubShader
    {
        Tags { "RenderPipeline"="UniversalRenderPipeline,HDRenderPipeline" "RenderType"="Opaque" }
        Pass
        {
            Name "LitPass"
            Tags { "LightMode"="UniversalForward" }
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #ifdef UNITY_SHADER_VARIABLES_INCLUDED
                #define USING_HDRP
            #else
                #define USING_URP
            #endif
            
            #ifdef USING_URP
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #endif
            
            #ifdef USING_HDRP
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/Lighting.hlsl"
            #endif
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
            };
            
            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float4 tangentWS : TEXCOORD2;
            };
            
            sampler2D _ColorMap;
            sampler2D _NormalMap;
            half _NormalStrength;
            half _Metallic;
            half _MetallicStrength;
            half _Smoothness;
            half _SmoothnessStrength;
            half _SmoothnessSlider;
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.tangentWS = TransformObjectToWorldDir(IN.tangentOS);
                return OUT;
            }
            
            half4 frag(Varyings IN) : SV_Target
            {
                half3 albedo = tex2D(_ColorMap, IN.uv).rgb;
                half3 normal = UnpackNormal(tex2D(_NormalMap, IN.uv));
                normal = normalize(lerp(float3(0,0,1), normal, _NormalStrength));
                
                half metallic = _Metallic * _MetallicStrength;
                half smoothness = _SmoothnessSlider * _SmoothnessStrength;
                
                #ifdef USING_URP
                    SurfaceData surfaceData;
                    surfaceData.albedo = albedo;
                    surfaceData.metallic = metallic;
                    surfaceData.smoothness = smoothness;
                    surfaceData.normalTS = normal;
                    InputData inputData = (InputData)0;
                    inputData.positionWS = IN.positionHCS;
                    inputData.normalWS = normalize(IN.normalWS);
                    inputData.viewDirectionWS = GetWorldSpaceViewDir(IN.positionHCS);
                    half4 color = UniversalFragmentPBR(inputData, surfaceData);
                #endif
                
                #ifdef USING_HDRP
                    BuiltinData builtinData;
                    builtinData.albedo = albedo;
                    builtinData.metallic = metallic;
                    builtinData.smoothness = smoothness;
                    builtinData.normalTS = normal;
                    half4 color = EvaluateBSDF_StandardLit(builtinData);
                #endif
                
                return color;
            }
            
            ENDHLSL
        }
    }
}