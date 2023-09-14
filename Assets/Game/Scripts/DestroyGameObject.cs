using UnityEngine;

namespace Blue
{
    public class DestroyGameObject : MonoBehaviour
    {
        public void Execute()
        {
            Destroy(gameObject);
        }
    }
}