using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CustomRP.Examples
{
    public class MeshBall : MonoBehaviour
    {
        private static int baseColorID = Shader.PropertyToID("_BaseColor");

        [SerializeField]
        private Mesh mesh = default;

        [SerializeField]
        private Material material = default;

        
        Matrix4x4[] matrices= new Matrix4x4[1023];
        
        Vector4[] baseColors= new Vector4[1023];
        private MaterialPropertyBlock block;


        private void Awake()
        {
            for (int i = 0; i < matrices.Length; i++)
            {
                Debug.Log("i="+i);
                matrices[i] = Matrix4x4.TRS(Random.insideUnitSphere * 10f, Quaternion.identity, Vector3.one);
                baseColors[i]=new Vector4(Random.value,Random.value,Random.value,1f);
            }
        }

        private void Update()
        {
            if (block == null)
            {
                Debug.Log("Update");
                block= new  MaterialPropertyBlock();
                block.SetVectorArray(baseColorID,baseColors);
            }
            Graphics.DrawMeshInstanced(mesh,0,material,matrices,1023,block);
        }
    }
}