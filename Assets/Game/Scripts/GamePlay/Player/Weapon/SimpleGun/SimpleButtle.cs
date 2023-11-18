using System.Collections;
using UnityEngine;

namespace Blue
{
    public class SimpleButtle : MonoBehaviour
    {
        private IEnumerator Start()
        {
            var rigidbody2D = GetComponent<Rigidbody2D>();
            var player = GameObject.FindWithTag("Player");
            rigidbody2D.velocity = Vector2.right * 10 * player.transform.localScale.x;

            yield return new WaitForSeconds(15);

            Destroy(gameObject); // 超过15秒销毁自己
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var enemyLayer = LayerMask.GetMask("Enemy");
            var groundLayer = LayerMask.GetMask("Ground");

            if (LayerMaskUtility.Contains(enemyLayer, other.collider.gameObject.layer))
            {
                //如果是敌人，则攻击敌人
                other.collider.GetComponent<CharacterHit>().Hit();
            }
            else if (LayerMaskUtility.Contains(groundLayer, other.collider.gameObject.layer))
            {
                //如果是墻毀自己
                Destroy(gameObject);
            }
        }
    }
}