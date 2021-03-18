﻿using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace CustomRP.Runtime
{
    
    //未来支持不同相机 不同渲染方法
    public partial  class CameraRenderer
    {


#if UNITY_EDITOR
                private static ShaderTagId[] legacyShaderTagIds =
                {
                    new ShaderTagId("Always"),
                    new ShaderTagId("ForwardBase"),
                    new ShaderTagId("PrepassBase"),
                    new ShaderTagId("Vertex"),
                    new ShaderTagId("VertexLMRGBM"),
                    new ShaderTagId("VertexLM"),
                };
        
                private static Material errorMaterial;
        
                
                partial void DrawUnsupportedShaders()
                {
                    if (errorMaterial==null)
                    {
                        errorMaterial= new  Material(Shader.Find("Hidden/InternalErrorShader"));
                    }
                    
                    var  drawingSettings= new  DrawingSettings(legacyShaderTagIds[0],new  SortingSettings(_camera))
                    {
                        overrideMaterial = errorMaterial
                    };
                    
                    for (int i = 1; i < legacyShaderTagIds.Length; i++)
                    {
                        drawingSettings.SetShaderPassName(i,legacyShaderTagIds[i]);
                    }
                    
                    var filteringSettings = FilteringSettings.defaultValue;
                    _context.DrawRenderers(_cullingResults,ref drawingSettings,ref  filteringSettings);
                    
                }
                
                
                
                partial void DrawGizmos()
                {
                    if (Handles.ShouldRenderGizmos())
                    {
                        _context.DrawGizmos(_camera,GizmoSubset.PreImageEffects);
                        _context.DrawGizmos(_camera,GizmoSubset.PostImageEffects);
                    }
                }
                
#endif
        

        
     
        
    }
}