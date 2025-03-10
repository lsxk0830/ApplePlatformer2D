using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    public class Trigger2D : MonoBehaviour
    {
        public bool Triggered = false;
        private HashSet<Collider2D> mCollider2Ds = new HashSet<Collider2D>(3);

        public void Reset()
        {
            Triggered = false;
            mCollider2Ds.Clear();
        }

        public LayerMask Layers;

        public UnityEvent OnTriggerEnter = new UnityEvent();
        public UnityEvent OnTriggerExit = new UnityEvent();

        public UnityEvent<Collider2D> OnTriggerEnterWithCollider = new UnityEvent<Collider2D>();
        public UnityEvent<Collider2D> OnTriggerExitWithCollider = new UnityEvent<Collider2D>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerMaskUtility.Contains(Layers,other.gameObject.layer))
            {
                OnTriggerEnterWithCollider?.Invoke(other);

                mCollider2Ds.Add(other);

                if (!Triggered && mCollider2Ds.Count > 0)
                {
                    Triggered = true;

                    OnTriggerEnter?.Invoke();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (LayerMaskUtility.Contains(Layers,other.gameObject.layer))
            {
                OnTriggerExitWithCollider?.Invoke(other);

                mCollider2Ds.Remove(other);

                if (Triggered && mCollider2Ds.Count == 0)
                {
                    Triggered = false;

                    OnTriggerExit?.Invoke();
                }
            }
        }
    }
}