#ifndef CUSTOM_OTS_LIT_PASS_INCLUDED
#define CUSTOM_OTS_LIT_PASS_INCLUDED
#include "../ShaderLibrary/Common.hlsl"
#include "../ShaderLibrary/Surface.hlsl"
#include "../ShaderLibrary/Light.hlsl"
#include "../ShaderLibrary/Lighting.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"

//纹理  采样
TEXTURE2D(_BaseMap);
SAMPLER(sampler_BaseMap);


//GPU INSTANCING 材质实例
CBUFFER_START(UnityPerMaterial)
    float4 _BaseMap_ST;
    float4 _BaseColor;
    float _Cutoff;
    float _Gloss;
    float _HighLightWidth;
    float4 _HighLightColor;
    float _CellWidth;
    float4 _CellColor;
CBUFFER_END



struct Attributes{
    float3 positionOS:POSITION;
    float3 normalOS:NORMAL;
    float2 baseUV:TEXCOORD0;
    float4 color : Color;
};


struct FragInput{
    float4 positionCS:SV_POSITION;//裁剪空间坐标
    float3 positionWS:VAR_POSITIONWS;// 位置 世界坐标
    float3 normalWS:VAR_NORMAL;//法线世界坐标
    float2 baseUV:VAR_BASE_UV;
    float4 vColor : VAR_VCOLOR;
};


FragInput OtsBodyVertex(Attributes input){

    FragInput output;
    //UNITY_SETUP_INSTANCE_ID(input);
    //UNITY_TRANSFER_INSTANCE_ID(input,output);//转换 实例ID

    output.positionWS = TransformObjectToWorld(input.positionOS);
    output.positionCS=TransformWorldToHClip(output.positionWS);
    output.normalWS=TransformObjectToWorldNormal(input.normalOS);
    float4 baseST=_BaseMap_ST;
    output.baseUV=input.baseUV*baseST.xy+baseST.zw;
    output.vColor=input.color;
    return output;
}


float4 OtsBodyFragment(FragInput input):SV_TARGET{

    float3 NoramlWS = normalize(input.normalWS);
    Light light=GetDirectionalLight();

    float3 ViewDirection=normalize(_WorldSpaceCameraPos-input.positionWS);

    float3 halfDir=normalize((light.direction+ViewDirection)*0.5);

    float LdotN=dot(light.direction,NoramlWS);
    LdotN=saturate(step(0,LdotN-_CellWidth));
    
    float3 halfAgnle= normalize(normalize(light.direction)+ViewDirection);
    float HdotN=saturate(dot(halfAgnle,NoramlWS));
    HdotN = saturate(ceil(HdotN-_HighLightWidth));

    //基础着色
    //UNITY_SETUP_INSTANCE_ID(input);
    float4 baseMap=SAMPLE_TEXTURE2D(_BaseMap,sampler_BaseMap,input.baseUV);
    float4 baseColor=_BaseColor;//访问材质属性
    float4 base=baseMap*baseColor;

    float4 finalColor=lerp(base,_HighLightColor+base,HdotN);


    //float spec=pow(saturate(dot(NoramlWS,halfDir)),_Gloss);

    //clip(base.a-UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial,_Cutoff));
    //base.rgb=abs(length(input.normalWS)-1.0)*20.0;
    //base.rgb=normalize(input.normalWS);
    Surface surface;
    surface.normal=NoramlWS;
    surface.color=base.rgb;
    surface.alpha=base.a;
    surface.viewDirection=ViewDirection;


    //GetLighting(surface);
    float3 color=base;

    return finalColor;
}

#endif

