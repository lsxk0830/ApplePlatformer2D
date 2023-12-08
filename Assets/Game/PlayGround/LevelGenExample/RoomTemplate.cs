using UnityEngine;
using UnityEngine.Tilemaps;

namespace Blue
{
    /// <summary>
    /// 房间模板类
    /// </summary>
    public class RoomTemplate : MonoBehaviour
    {
        public Transform LB;
        public Transform RT;
        public Grid Grid;
        public Tilemap Ground;
        public Transform LeftConnectionPoint;
        public Transform RightConnectionPoint;

        public Vector3Int RightConnectionCellPosOffset => Grid.WorldToCell(RightConnectionPoint.position) - Grid.WorldToCell(LB.position);
        public Vector3Int LeftConnectionCellPosOffset => Grid.WorldToCell(LeftConnectionPoint.position) - Grid.WorldToCell(LB.position);

        public Vector3Int CopyRoomTemplateToBase(Vector3Int currentRightRoomConnectionCellPos, Tilemap toGround)
        {
            if (currentRightRoomConnectionCellPos == Vector3Int.zero)
                currentRightRoomConnectionCellPos = this.LeftConnectionCellPosOffset; // 第一个先是自己左边

            var cellOffsetToNextRoom = currentRightRoomConnectionCellPos - this.LeftConnectionCellPosOffset;
            currentRightRoomConnectionCellPos = cellOffsetToNextRoom + this.RightConnectionCellPosOffset;
            var startPosLB = this.Grid.WorldToCell(this.LB.position); // 将世界位置转换为 Tilemap 坐标
            var endPosRT = this.Grid.WorldToCell(this.RT.position);

            // 左下到右上复制一整片区域
            for (var x = startPosLB.x; x < endPosRT.x + 1; x++)
            {
                for (var y = startPosLB.y; y < endPosRT.y + 1; y++)
                {
                    var tile = this.Ground.GetTile(new Vector3Int(x, y, startPosLB.z)); //根据给定的瓦片地图中某个单元格的 XYZ 坐标，获取瓦片
                    if (tile)
                    {
                        toGround.SetTile(new Vector3Int(x - startPosLB.x + cellOffsetToNextRoom.x,
                                                        y - startPosLB.y + cellOffsetToNextRoom.y,
                                                        startPosLB.z),
                                        tile);
                    }
                }
            }

            return currentRightRoomConnectionCellPos;
        }
    }
}