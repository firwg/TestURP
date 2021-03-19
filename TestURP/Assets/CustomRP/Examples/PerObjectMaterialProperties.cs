using System;
using UnityEngine;

namespace CustomRP.Examples
{
    public class PerObjectMaterialProperties : MonoBehaviour
    {
        private static int baseColorId = Shader.PropertyToID("_BaseColor");

        private static MaterialPropertyBlock _block;

        
        [SerializeField]
        Color bseColor=Color.white;


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
            GetComponent<Renderer>().SetPropertyBlock(_block);
            
        }
    }
}