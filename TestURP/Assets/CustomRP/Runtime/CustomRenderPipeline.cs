using UnityEngine;
using UnityEngine.Rendering;

namespace CustomRP.Runtime
{
    public class CustomRenderPipeline : RenderPipeline
    {
        
        CameraRenderer renderer= new CameraRenderer();
        
        //Unity每帧调用
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach (var VARIABLE in cameras)
            {
                renderer.Render(context,VARIABLE);
            }
        }
        
        
    }
    
    
}
