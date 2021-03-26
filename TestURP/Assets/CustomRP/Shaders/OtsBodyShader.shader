
Shader "CustomRP/OtsBodyShader"
{
    Properties
    {
        _BaseMap("Texture",2D)="white"{}
        _BaseColor("Color",Color)=(1.0,1.0,1.0,1.0)
        _Gloss("Gloss", float )=0.5
        _CellWidth("CellWidth",Range(-1,1))=0.5
        _CellColor("CellColor",Color) = (0,0,0,0)
        _HighLightWidth("HighLightWidth",Range(-1,1))=0.5
        _HighLightColor("HighLightColor",Color) = (1,1,1,1)
    }
    SubShader
    {
        pass{
            Tags{
                "LightMode"="CustomLit"
            }

            HLSLPROGRAM
            #pragma multi_compile_instanceing
            #pragma vertex OtsBodyVertex
            #pragma fragment OtsBodyFragment
            #include "OtsLitPass.hlsl"
            ENDHLSL
        }
    }
}
