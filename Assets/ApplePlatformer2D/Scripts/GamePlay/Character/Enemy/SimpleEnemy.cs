using UnityEngine;

public class SimpleEnemy : CharacterController
{
    public Trigger2D GroundCheck;
    public Trigger2D ForwardCheck;
    public Trigger2D FallCheck;
    public Trigger2D AttackCheck;

    private void Awake()
    {
        var characterMovement = GetComponent<CharacterMovement>();
        characterMovement.enabled = false;

        GroundCheck.OnTriggerEnter.AddListener(() => 
        {
            // 触发这个之后，再进行移动
            characterMovement.enabled = true;   
        });
        GroundCheck.OnTriggerExit.AddListener(() =>
        {
            characterMovement.enabled = false;
        });

        ForwardCheck.OnTriggerEnter.AddListener(() =>
        {
            var loacalScale = transform.localScale;
            loacalScale.x *= -1;
            transform.localScale = loacalScale; 
        });

        FallCheck.OnTriggerExit.AddListener(() =>
        {
            var loacalScale = transform.localScale;
            loacalScale.x *= -1;
            transform.localScale = loacalScale;
        });

        AttackCheck.OnTriggerEnterWithCollider.AddListener((collider) =>
        {
            collider.GetComponent<PlayerHit>().Hit();

            AttackPhysicsEffect(transform,collider.transform);
        });

    }


    public int HitterVelocityX = 5;
    public int HitterVelocityY = 5; 
    void AttackPhysicsEffect(Transform attacker,Transform hitter)
    {
        var attackPos = attacker.position.x;
        var hitPos = hitter.position.x;

        var direction = hitPos - attackPos; // 方向

        var directionNormal = Mathf.Sign(direction); // 大于0 Return 1，相等 Return 0,小于0 Return -1

        hitter.GetComponent<Rigidbody2D>().velocity = new Vector2(directionNormal * HitterVelocityX, HitterVelocityY); // 碰撞体附加到的刚体
    }
}
