namespace Blue
{
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
            GroundCheck.OnTriggerEnter.AddListener(()=>
            {
                // 触发这个之后，在进行移动
                characterMovement.enabled = true;
            });

            GroundCheck.OnTriggerExit.AddListener(()=>
            {
                characterMovement.enabled = false;
            });

            ForwardCheck.OnTriggerEnter.AddListener(()=>
            {
                var localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            });

            FallCheck.OnTriggerExit.AddListener(()=>
            {
                var localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            });

            AttackCheck.OnTriggerEnterWithCollider.AddListener((collider)=>
            {
                collider.GetComponent<PlayerHit>().Hit();
            });
        }
    }
}