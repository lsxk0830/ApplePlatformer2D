using UnityEngine;

public class SimpleShootEnemy : CharacterController
{
    public enum States
    {
        /// <summary>
        /// 落下
        /// </summary>
        Falling,
        /// <summary>
        /// 巡逻
        /// </summary>
        Patrol,
        /// <summary>
        /// 攻击
        /// </summary>
        Shoot
    }

    public States State = States.Falling;
    public Trigger2D GroundCheck;
    public Trigger2D ForwardCheck;
    public Trigger2D FallCheck;
    public Trigger2D ShootArea;

    public GameObject BulletPrefab;
    private void Awake()
    {
        var characterMovement = GetComponent<CharacterMovement>();
        characterMovement.enabled = false;

        GroundCheck.OnTriggerEnter.AddListener(() =>
        {
            State = States.Patrol;
            // 触发这个之后，再进行移动
            characterMovement.enabled = true;
        });
        GroundCheck.OnTriggerExit.AddListener(() =>
        {
            State = States.Falling;
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

        ShootArea.OnTriggerEnter.AddListener(() =>
        {
            State = States.Shoot;
            mPreviousShootTime = Time.time-0.8f;
            characterMovement.enabled = false;
        });
        ShootArea.OnTriggerExit.AddListener(() =>
        {
            State = States.Patrol;
            characterMovement.enabled = true;
        });

    }

    private float mPreviousShootTime; // 上一次射击时间
    private void Update()
    {
        if (State == States.Shoot)
        {
            if (Time.time - mPreviousShootTime > 1f)
            {
                // 射击操作
                var bulletObject = Instantiate(BulletPrefab);
                bulletObject.transform.position = BulletPrefab.transform.position;
                bulletObject.SetActive(true);
                bulletObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * transform.localScale.x * 6;
                mPreviousShootTime = Time.time;
            }
        }
    }
}
