#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED



float3 GetLighting(Surface surface){
  Light light=GetDirectionalLight();

return saturate(dot(surface.normal,light.direction))*light.color*surface.color;
    //return GetLightingn(surface,GetDirectionalLight());
}

// float3 GetLightingn(Surface surface,Light light){
//     return IncomingLight(surface,light)*surface.color;
// }

// float3 IncomingLight(Surface surface,Light light){
//     return saturate(dot(surface.normal,light.direction))*light.color;
// }





#endif