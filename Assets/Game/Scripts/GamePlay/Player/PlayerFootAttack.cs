using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blue
{
    public class PlayerFootAttack : MonoBehaviour
    {
        public Trigger2D FootAttackCheck;

        void Awake()
        {
            FootAttackCheck.OnTriggerEnterWithCollider.AddListener(collider=>
            {
                collider.GetComponent<CharacterHit>().Hit();
            });
        }
    }
}