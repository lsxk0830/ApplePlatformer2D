using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D mRigidbody2D;

    public float HorizontalMovementSpeed = 3;
    private void Awake()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        mRigidbody2D.velocity = new Vector2(transform.localScale.x * HorizontalMovementSpeed, mRigidbody2D.velocity.y);
    }
}
