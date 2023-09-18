using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blue
{
    public class SimpleEnemy : CharacterController
    {
        public Trigger2D GroundCheck;

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
        }
    }
}