using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger2D : MonoBehaviour
{
    public LayerMask Layers;

    public bool Triggerd = false;

    HashSet<Collider2D> mCollider2Ds = new HashSet<Collider2D>(3);

    public void Reset()
    {
        Triggerd = false;
       mCollider2Ds.Clear();
    }

    public UnityEvent OnTriggerEnter = new UnityEvent();
    public UnityEvent OnTriggerExit = new UnityEvent();

    public UnityEvent<Collider2D> OnTriggerEnterWithCollider = new UnityEvent<Collider2D>();
    public UnityEvent<Collider2D> OnTriggerExitWithCollider = new UnityEvent<Collider2D>(); 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMaskUtility.Contains(Layers, collision.gameObject.layer))
        {
            OnTriggerEnterWithCollider?.Invoke(collision);

            mCollider2Ds.Add(collision);
            if (!Triggerd && mCollider2Ds.Count > 0)
            {
                Triggerd = true;
                OnTriggerEnter?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMaskUtility.Contains(Layers, collision.gameObject.layer))
        {
            OnTriggerExitWithCollider?.Invoke(collision);

            mCollider2Ds.Remove(collision);
            if (Triggerd && mCollider2Ds.Count == 0)
            {
                Triggerd = false;
                OnTriggerExit?.Invoke();
            }
        }
      
    }
}
