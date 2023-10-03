using System;
using UnityEngine;

namespace Blue
{
    public class AttackPlayerEffect : MonoBehaviour
    {
        public void Execute()
        {
            var playerObj = GameObject.FindWithTag("Player");
            playerObj.GetComponent<PlayerHit>().Hit();
            AttackPhysicsEffect(this.transform, playerObj.transform);
        }

        public int HitterVelocityX = 5; // 攻击时水平方向速度
        public int HitterVelocityY = 5; // 攻击时垂直方向速度
        private void AttackPhysicsEffect(Transform attack, Transform hitter)
        {
            var attackPos = attack.position.x;
            var hitPos = hitter.position.x;
            var direction = hitPos - attackPos;
            var directionNormal = MathF.Sign(direction);
            hitter.GetComponent<Rigidbody2D>().velocity = new Vector2(directionNormal * HitterVelocityX, HitterVelocityY);
        }
    }
}