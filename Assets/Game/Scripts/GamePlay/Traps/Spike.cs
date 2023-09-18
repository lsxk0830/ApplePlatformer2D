using UnityEngine;

namespace Blue
{
    public class Spike : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                var hit = other.GetComponent<PlayerHit>();
                hit.Hit();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            
        }
    }
}