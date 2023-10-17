using UnityEngine;

namespace Blue
{
    public class SetSpriteColor : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;
        public Color Color = Color.white;

        public void Execute()
        {
            SpriteRenderer.color = Color;
        }
    }
}