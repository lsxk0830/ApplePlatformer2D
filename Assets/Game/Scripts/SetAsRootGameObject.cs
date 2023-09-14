using UnityEngine;

namespace Blue
{
    public class SetAsRootGameObject : MonoBehaviour
    {
        public void Execute()
        {
            transform.SetParent(null);
        }
    }
}