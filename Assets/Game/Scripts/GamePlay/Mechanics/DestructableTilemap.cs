using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Blue
{
    public class DestructableTilemap : MonoBehaviour
    {
        public UnityEvent<Collision2D, Tilemap> OnCollisionEvent = new UnityEvent<Collision2D, Tilemap>();

        private Tilemap mTilemap;

        private void Awake()
        {
            mTilemap = GetComponent<Tilemap>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollisionEvent?.Invoke(other, mTilemap);
        }

        /// <summary>
        /// 外部调用，也可以自己实现功能
        /// </summary>
        public void DestoryTile(Collision2D other,Vector2 offset)
        {
            var contact = other.GetContact(0);
            var contactPoint = contact.point;
            var cellPosition = mTilemap.WorldToCell(contactPoint + offset);
            mTilemap.SetTile(cellPosition, null);
        }
    }
}