using UnityEngine;

namespace Blue
{
    public class BGScroll : MonoBehaviour
    {
        public float Ratio; // 比例
        public Camera Camera;
        private Material mMaterial;

        void Start()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            mMaterial = spriteRenderer.material;
        }

        void Update()
        {
            var position = Camera.transform.position;
            mMaterial.SetVector("OffsetXY", new Vector4(position.x * Ratio, position.y * Ratio, 0, 0));
        }
    }
}