using System;
using UnityEngine;

namespace CustomRP.Examples
{
    public class PerObjectMaterialProperties : MonoBehaviour
    {
        private static int baseColorId = Shader.PropertyToID("_BaseColor");
        private static int cutoffId = Shader.PropertyToID("_Cutoff");
        private static int metallicId = Shader.PropertyToID("_Metallic");
        private static int smoothnessId = Shader.PropertyToID("_Smoothness");
        
        private static MaterialPropertyBlock _block;

        
        [SerializeField]
        Color bseColor=Color.white;
        [SerializeField,Range(0f,1f)]
        float cutoff = 0.5f;
        
        [SerializeField,Range(0f,1f)]
        float  metallic= 0f;
        [SerializeField,Range(0f,1f)]
        float  smoothness= 0.5f;


        private void Awake()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            if (_block==null)
            {
                _block= new  MaterialPropertyBlock();
            }
            _block.SetColor(baseColorId,bseColor);
            _block.SetFloat(cutoffId, cutoff);
            GetComponent<Renderer>().SetPropertyBlock(_block);
            
        }
    }
}