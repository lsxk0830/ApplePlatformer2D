using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D mRigidbody2D;
    public float HorizontalMovememntSpeed = 5;
    public float JumpSpeed = 5;

    public float GraivityMultiplier = 2;
    public float FallMultiplier = 1;

    public UnityEvent OnLand;
    public UnityEvent OnJump;

    public float MinJumpTime = 0.2f;
    public float MaxJumpTime = 0.5f;

    public Trigger2D GroundCheck;
    private void Start()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private float mHorizontalInput = 0;
    private bool mJumpPressed = false;
    private float mCurrentJumpTime = 0;
    void Update()
    {
        mHorizontalInput = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.K) && GroundCheck .Triggerd)
        {
            OnJump?.Invoke();
            mJumpPressed=true;
           
            if(JumpState == JumpStates.NotJump)
            {
                JumpState = JumpStates.MinJump;
                mCurrentJumpTime = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            mJumpPressed = false;
        }

        mCurrentJumpTime+=Time.deltaTime;
    }

    public enum JumpStates
    {
        NotJump,
        MinJump,
        ControlJump,
    }

    public JumpStates JumpState = JumpStates.NotJump; 
    private void FixedUpdate()
    {
        if (JumpState == JumpStates.MinJump)
        {
            mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, JumpSpeed);
            if (mCurrentJumpTime >= MinJumpTime)
            {
                JumpState = JumpStates.ControlJump;
            }        
        }
        else if(JumpState == JumpStates.ControlJump)
        {
            mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, JumpSpeed);
            if (mJumpPressed && mCurrentJumpTime>=MaxJumpTime || !mJumpPressed)
            {
                JumpState = JumpStates.NotJump;
            }
        }

        mRigidbody2D.velocity = new Vector2(mHorizontalInput * HorizontalMovememntSpeed, mRigidbody2D.velocity.y);

        if (mRigidbody2D.velocity.y > 0 && JumpState != JumpStates.NotJump)
        {
            var progress = mCurrentJumpTime / MaxJumpTime;
            float jumpGravityMultiplier = GraivityMultiplier;
            if (progress > 0.5f)
            {
                jumpGravityMultiplier = GraivityMultiplier * (1-progress);
            }
            mRigidbody2D.velocity += Physics2D.gravity * jumpGravityMultiplier * Time.deltaTime;
        }
        else if (mRigidbody2D.velocity.y < 0)
        {
            mRigidbody2D.velocity += Physics2D.gravity * FallMultiplier * Time.deltaTime;
        }
    }
}
