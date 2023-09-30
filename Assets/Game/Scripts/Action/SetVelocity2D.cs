using UnityEngine;

namespace Blue
{
    public class SetVelocity2D : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D;
        public Vector2 Vector2;

        public void Execute()
        {
            Rigidbody2D.velocity = Vector2;
        }
    }
}