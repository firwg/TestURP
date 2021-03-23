using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.CustomRP.Runtime
{
    class Lighting
    {
        const string bufferName = "Lighting";

        CullingResults cullingResults;


        const int maxDirLightCount = 4;

        static int dirLightColorId = Shader.PropertyToID("_DirectionalLightColor"),
            dirLightDirectionId = Shader.PropertyToID("_DirectionalLightDirection"),
            dirLightCountId = Shader.PropertyToID("_DirectionalLightCount");

        static Vector4[] dirLightColors = new Vector4[maxDirLightCount],
            dirLightDirections = new Vector4[maxDirLightCount];


        CommandBuffer buffer = new CommandBuffer
        {
            name = bufferName
        };

        public void Setup(ScriptableRenderContext context,CullingResults cullingResults)
        {
            buffer.BeginSample(bufferName);
            this.cullingResults = cullingResults;
            SetupLights();
            buffer.EndSample(bufferName);
            context.ExecuteCommandBuffer(buffer);
            buffer.Clear();
        }



        void SetupLights()
        {

            NativeArray<VisibleLight> visibleLights=cullingResults.visibleLights;

            int dirLightCount = 0;

            for(int i=0;i< visibleLights.Length; i++)
            {
                VisibleLight light = visibleLights[i];
                SetupDirectionalLight(i, light);


            }

            buffer.SetGlobalInt(dirLightCountId, visibleLights.Length);
            buffer.SetGlobalVectorArray(dirLightColorId, dirLightColors);
            buffer.SetGlobalVectorArray(dirLightDirectionId, dirLightDirections);

            //Light light = RenderSettings.sun;
            //buffer.SetGlobalVector(dirLightColorId, light.color.linear*light.intensity);
            //buffer.SetGlobalVector(dirLightDirectionId, -light.transform.forward);



        }

        void SetupDirectionalLight(int index,VisibleLight visibleLight)
        {
            dirLightColors[index] = visibleLight.finalColor;
            dirLightDirections[index] = -visibleLight.localToWorldMatrix.GetColumn(2);
        }


    }
}
