using UnityEngine;
using UnityEngine.Rendering;
using  UnityEditor;

namespace CustomRP.Runtime
{
    //未来支持不同相机 不同渲染方法
    public partial class CameraRenderer
    {
        private ScriptableRenderContext _context;

        private Camera _camera;

        private const string bufferName = "Render Camera";
        
        private CommandBuffer buffer = new CommandBuffer() {name = bufferName};

        private CullingResults _cullingResults;
        
        static  ShaderTagId unlitShaderTagId= new  ShaderTagId("SRPDefaultUnlit");
        
        
        public void Render(ScriptableRenderContext context, Camera camera,bool useDynamicBatching, bool useGPUInstancing)
        {
            _context = context;
            _camera = camera;
            
            if (!Cull())
            {
                return;
            }
            
            //设置
            SetUp();

            //绘制天空
            DrawVisibleGeometry(useDynamicBatching,useGPUInstancing);
            //其他得绘制  必须单独通过commandbuffer 间接执行

#if UNITY_EDITOR
            DrawUnsupportedShaders();
            DrawGizmos();
#endif

            
            //提交
            Submit();
        }


        void SetUp()
        {            
            _context.SetupCameraProperties(_camera);
            buffer.BeginSample(bufferName);
            //是否清除 深度  颜色
            buffer.ClearRenderTarget(true,true,Color.clear);
            ExecuteBuffer();
        }
        
        
        void DrawVisibleGeometry(bool useDynamicBatching,bool useGPUInstancing)
        {
            var sortingSettings= new  SortingSettings(_camera)
            {
                criteria = SortingCriteria.CommonOpaque
            };
            
            
            var  drawingSettings= new  DrawingSettings(unlitShaderTagId,sortingSettings)
            {
                enableDynamicBatching = useDynamicBatching,
                enableInstancing = useGPUInstancing
            };
            var filteringSettings= new  FilteringSettings(RenderQueueRange.opaque);
            _context.DrawRenderers(_cullingResults,ref drawingSettings,ref filteringSettings);

            _context.DrawSkybox(_camera);


            sortingSettings.criteria = SortingCriteria.CommonTransparent;
            drawingSettings.sortingSettings = sortingSettings;
            filteringSettings.renderQueueRange=RenderQueueRange.transparent;
            
            _context.DrawRenderers(_cullingResults,ref drawingSettings,ref filteringSettings);
            
            
           // _context.DrawGizmos(_camera,)
            
        }



        partial void DrawUnsupportedShaders();

        //partial void DrawGizmos();
        

        void Submit()
        {
            buffer.EndSample(bufferName);
            ExecuteBuffer();
            _context.Submit();
        }

        void ExecuteBuffer()
        {
            _context.ExecuteCommandBuffer(buffer);
            buffer.Clear();
        }


        bool Cull()
        {
            if (_camera.TryGetCullingParameters(out ScriptableCullingParameters p))
            {
                _cullingResults = _context.Cull(ref p);
                return true;
            }
            return false;
        }
        
    }
}