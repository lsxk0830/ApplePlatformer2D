using System.Diagnostics.Contracts;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Blue
{
    public class DestructableTilemap : MonoBehaviour
    {
        public UnityEvent OnTileDestroy = new UnityEvent();

        private Tilemap mTilemap;

        private void Awake()
        {
            mTilemap = GetComponent<Tilemap>();
        }

        /// <summary>
        /// 外部调用，也可以自己实现功能
        /// </summary>
        public void DestoryTile(Collision2D other, Vector2 offset)
        {
            var contact = other.GetContact(0);
            var contactPoint = contact.point;
            var cellPosition = mTilemap.WorldToCell(contactPoint + offset);
            mTilemap.SetTile(cellPosition, null);

            // 屏幕震动
            GameObject.FindWithTag("CameraController").GetComponent<BehaviorTree>()
                .SendEvent("CAMERA_SHAKE");
            // 播放地块销毁音效
            AudioSystem.PlayDestrutcTile(Camera.main.transform.position +
                                        Vector3.right * Mathf.Sign(contact.point.x - Camera.main.transform.position.x) + Vector3.forward * 2);
            // TODO:破坏动画
            OnTileDestroy?.Invoke();
        }
    }
}