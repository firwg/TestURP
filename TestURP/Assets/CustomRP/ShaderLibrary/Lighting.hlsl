#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED




float3 GetLighting(Surface surface,BRDF brdf){
  
  Light light=GetDirectionalLight();

  return saturate(dot(surface.normal,light.direction))*light.color*brdf.diffuse;

  //  float3 f3=GetLighting2(surface,brdf,light);
  //  return f3;
}



// float3 GetLighting2(Surface surface,BRDF brdf,Light light){
//    float3 f3=IncomingLight(surface,brdf,light)*surface.color;
//     return f3;
// }


// float3 IncomingLight(Surface surface,BRDF brdf, Light light){
//    float3 f3=saturate(dot(surface.normal,light.direction))*light.color;
//     return f3;
// }





#endif