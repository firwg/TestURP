using UnityEngine;
using UnityEngine.Rendering;

namespace CustomRP.Runtime
{
    public class CustomRenderPipeline : RenderPipeline
    {
        private bool useDynamicBatching, useGPUInstancing;

        public CustomRenderPipeline(bool useDynamicBatching,bool useGPUInstancing,bool useSRPBatcher)
        {
            this.useDynamicBatching = useDynamicBatching;
            this.useGPUInstancing = useGPUInstancing;
            GraphicsSettings.useScriptableRenderPipelineBatching = useSRPBatcher;
        }
        
        
        CameraRenderer renderer= new CameraRenderer();
        
        //Unity每帧调用
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach (var VARIABLE in cameras)
            {
                renderer.Render(context,VARIABLE,useDynamicBatching,useGPUInstancing);
            }
        }
        
        
    }
    
    
}
