using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D mRigidbody;
    private PlayerMovement mPlayerMovement;
    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
        mPlayerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        var velocity = mRigidbody.velocity;
        var scaleXOffset = 0.3f * Mathf.Abs(velocity.x / mPlayerMovement.HorizontalMovememntSpeed);
        var scale = new Vector3(1+ scaleXOffset, 0.7f+0.3f,1);
        transform.localScale = scale;
    }
}
