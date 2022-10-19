using UnityEngine;

namespace UTGM
{
    public class AttackPlayerEffect : MonoBehaviour
    {
        public void Execute()
        {
            var playerObj = GameObject.FindWithTag("Player");
            playerObj.GetComponent<PlayerHit>().Hit();
            AttackPhysicsEffect(this.transform, playerObj.transform);
        }

        public int HitterVelocityX = 5;
        public int HitterVelocityY = 5;
        void AttackPhysicsEffect(Transform attacker, Transform hitter)
        {
            var attackPos = attacker.position.x;
            var hitPos = hitter.position.x;

            var direction = hitPos - attackPos; // 方向

            var directionNormal = Mathf.Sign(direction); // 大于0 Return 1，相等 Return 0,小于0 Return -1

            hitter.GetComponent<Rigidbody2D>().velocity = new Vector2(directionNormal * HitterVelocityX, HitterVelocityY); // 碰撞体附加到的刚体
        }
    }

}
