Shader "CustomRP/Lit"
{
    Properties
    {
        _BaseMap("Texture",2D)="white"{}
        _BaseColor("Color",Color)=(1.0,1.0,1.0,1.0)
    }
    SubShader
    {
        pass{
            Tags{
                "LightMode"="CustomLit"
            }


            HLSLPROGRAM
            #pragma multi_compile_instanceing
            #pragma vertex LitPassVertex
            #pragma fragment LitPassFragment
            #include "LitPass.hlsl"

            ENDHLSL
        }
    }
}
