using UnityEngine;
using UnityEngine.Events;

public class PlayerFootAttack : MonoBehaviour
{
    public Trigger2D FootAttackCheck;

    public float JumpSpeed = 15;
    private Rigidbody2D mRigidbody2D;

    public UnityEvent OnFootAttack = new UnityEvent();  
    private void Awake()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();

        FootAttackCheck.OnTriggerEnterWithCollider.AddListener((collider) => 
        {
            collider.GetComponent<CharacterHit>().Hit();

            mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x,JumpSpeed);

            OnFootAttack?.Invoke();
        });
    }
}
