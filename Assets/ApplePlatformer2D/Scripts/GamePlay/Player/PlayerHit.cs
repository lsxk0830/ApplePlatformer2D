using UnityEngine;
using UnityEngine.Events;
public class PlayerHit : MonoBehaviour
{
    public UnityEvent OnHit = new UnityEvent();

    public void Hit()
    {
        OnHit?.Invoke();
    }
}
