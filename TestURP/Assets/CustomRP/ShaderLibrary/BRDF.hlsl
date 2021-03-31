#ifndef CUSTOM_BRDF_INCLUDED
#define CUSTOM_BRDF_INCLUDED



struct BRDF{
    float3 diffuse;//漫反射颜色
    float3 specular;//镜面颜色
    float roughness;//粗糙度
};



#define MIN_REFLECTIVITY 0.04

//根据材质金属属性值  得到 1倍反射率
float OneMinusReflectivity(float metallic){
    float  range=1.0-MIN_REFLECTIVITY;
    range=range*metallic;
    return range;
}



BRDF GetBRDF(Surface surface){
    BRDF brdf;
    //反射率
    float oneMinusReflectivity=OneMinusReflectivity(surface.metallic);
    //漫反射光
    brdf.diffuse=surface.color*oneMinusReflectivity;
    //镜面颜色
    brdf.specular=lerp(MIN_REFLECTIVITY,surface.color,surface.metallic);//平稳过渡
    //粗糙度
    float perceptualRoughness=PerceptualRoughnessToPerceptualSmoothness(surface.smoothness);
    brdf.roughness=PerceptualRoughnessToRoughness(perceptualRoughness);
    return brdf;
}



//高光强度
float SpecularStrength(Surface surface,BRDF brdf,Light light){
    float3 h=SafeNormalize(light.direction+surface.viewDirection);
    float nh2=Square(saturate(dot(surface.normal,h)));
    float lh2=Square(saturate(dot(light.direction,h)));
    float r2=Square(brdf.roughness);
    float d2=Square(nh2*(r2-1.0)+1.00001);
    float normalization=brdf.roughness*4.0+2.0;
    return r2/(d2*max(0.1,lh2)*normalization);
}


//直接光照
float3 DirectBRDF(Surface surface,BRDF brdf,Light light){
    return SpecularStrength(surface,brdf,light)*brdf.specular+brdf.diffuse;
}



#endif