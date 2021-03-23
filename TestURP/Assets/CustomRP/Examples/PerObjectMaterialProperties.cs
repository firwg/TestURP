using System;
using UnityEngine;

namespace CustomRP.Examples
{
    public class PerObjectMaterialProperties : MonoBehaviour
    {
        private static int baseColorId = Shader.PropertyToID("_BaseColor");
        private static int cutoffId = Shader.PropertyToID("_Cutoff");

        private static MaterialPropertyBlock _block;

        
        [SerializeField]
        Color bseColor=Color.white;
        [SerializeField]
        float cutoff = 0.5f;

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