using UnityEngine;

namespace UTGM
{
    public class SetVelocity2D : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D;
        public Vector2 velocity;

        public void Execute()
        {
            Rigidbody2D.velocity = velocity;
        }
    }

}
