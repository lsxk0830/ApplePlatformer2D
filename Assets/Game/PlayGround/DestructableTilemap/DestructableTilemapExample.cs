using UnityEngine;
using UnityEngine.Tilemaps;

namespace Blue
{
    public class DestructableTilemapExample : MonoBehaviour
    {
        public OnCollisionEnter2DEvent OnEnter;

        public Grid Grid;

        public Tilemap Tilemap;

        private void Start()
        {
            OnEnter.OnEnterWithCollision.AddListener(col =>
            {
                // 获取碰撞位置
                var contact = col.GetContact(0);

                var contactPoint = contact.point;

                var cellPosition = Grid.WorldToCell(contactPoint + contact.normal * 0.01f);

                // 设置为 null
                Tilemap.SetTile(cellPosition, null);
            });
        }
    }
}