using UnityEngine;
using UnityEngine.Events;

public class CharacterHit : MonoBehaviour
{
    public UnityEvent OnHit = new UnityEvent();
    public void Hit()
    {
        OnHit?.Invoke();    
    }
}
