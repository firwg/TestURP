Shader "CustomRP/Unlit"
{
    Properties
    {
        _BaseMap("Texture",2D)="white"{}
        _BaseColor("Color",Color)=(1.0,1.0,1.0,1.0)
        _Cutoff("Alpha Cutoff",Range(0.0,1.0))=0.5
        [Toggle(_CLIPPING)] _Clipping("Alpha Clipping",float)=0
        [Enum(UnityEngine.Rendering.BlendMode)]_SrcBlend("Src Blend",Float)=1
        [Enum(UnityEngine.Rendering.BlendMode)]_DstBlend("Dst Blend",Float)=0
        [Enum(off,0,On,1)] _ZWrite("Z Writw",Float)=1
    }
    SubShader
    {
        pass{
            Blend [_SrcBlend][_DstBlend]
            ZWrite [_ZWrite]
            HLSLPROGRAM
            #pragma multi_compile_instanceing
            #pragma vertex UnlitPassVertex
            #pragma fragment UnlitPassFragment
            #include "UnlitPass.hlsl"
            ENDHLSL
        }
    }
}
