using UnityEngine;
using UnityEngine.Tilemaps;

namespace Blue
{
    public class LevelGenExample : MonoBehaviour
    {
        /// <summary>
        /// 所有在 Grid 子节点得到 TileMap 共享同一套 Tilemap 坐标（网格坐标）
        /// </summary>
        public Grid FromGild;

        public Tilemap From;
        /// <summary>
        /// 开始位置（左下）
        /// </summary>
        public Transform FromStartPosLB;
        /// <summary>
        /// 结束位置（右上）
        /// </summary>
        public Transform FromEndPosRT;

        public Tilemap To;
        public Vector2Int CopyOffset = Vector2Int.right * 5; // 偏移
        private void Start()
        {
            var startPosLB = FromGild.WorldToCell(FromStartPosLB.position); // 将世界位置转换为单元格位置
            var endPosRT = FromGild.WorldToCell(FromEndPosRT.position);

            // 左下到右上复制一整片区域
            for (var x = startPosLB.x; x < endPosRT.x + 1; x++)
            {
                for (var y = startPosLB.y; y < endPosRT.y + 1; y++)
                {
                    var tile = From.GetTile(new Vector3Int(x, y, startPosLB.z)); //根据给定的瓦片地图中某个单元格的 XYZ 坐标，获取瓦片
                    if (tile)
                    {
                        To.SetTile(new Vector3Int(x + CopyOffset.x, y + CopyOffset.y, startPosLB.z), tile);
                    }
                }
            }
        }
    }
}