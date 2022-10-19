using UnityEngine;

public class SetAsRootGameObject : MonoBehaviour
{
    public void Execute()
    {
        transform.SetParent(null);
    }
}
