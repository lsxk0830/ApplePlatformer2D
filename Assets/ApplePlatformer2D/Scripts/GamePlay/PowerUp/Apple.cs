using UnityEngine;
using UnityEngine.Events;

public class Apple : MonoBehaviour
{
    public UnityEvent OnGet;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnGet?.Invoke();
            Destroy(gameObject);
        }
    }
}
