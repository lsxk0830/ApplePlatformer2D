using UnityEngine;

namespace Blue
{
    public class Spike : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var hit = other.GetComponent<PlayerHit>();
                if (hit.CanHit)
                {
                    hit.Hit();
                }
            }
        }
    }
}