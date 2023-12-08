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
        public Transform Objects;

        public Vector3Int RightConnectionCellPosOffset => Grid.WorldToCell(RightConnectionPoint.position) - Grid.WorldToCell(LB.position);
        public Vector3Int LeftConnectionCellPosOffset => Grid.WorldToCell(LeftConnectionPoint.position) - Grid.WorldToCell(LB.position);

        /// <summary>
        /// 除了 Tilemap,Objects 里的东西也要复制
        /// </summary>
        /// <param name="currentRightRoomConnectionCellPos"></param>
        /// <param name="toGround"></param>
        /// <returns></returns>
        public void CopyRoomTemplateToBase(GeneratedRoom generatedRoom)
        {
            if (generatedRoom.CurrentRoomRighConnectionCellPos == Vector3Int.zero)
                generatedRoom.CurrentRoomRighConnectionCellPos = this.LeftConnectionCellPosOffset; // 第一个先是自己左边
            var cellOffsetToNextRoom = generatedRoom.CurrentRoomRighConnectionCellPos - this.LeftConnectionCellPosOffset;

            CopyTilemapToRoom(generatedRoom,cellOffsetToNextRoom);
            CopyObjectsToRoom(generatedRoom,cellOffsetToNextRoom);

            generatedRoom.CurrentRoomRighConnectionCellPos = cellOffsetToNextRoom + this.RightConnectionCellPosOffset;

        }

        private void CopyTilemapToRoom(GeneratedRoom generatedRoom,Vector3Int cellOffsetToNextRoom)
        {
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
                        generatedRoom.ToGround.SetTile(new Vector3Int(x - startPosLB.x + cellOffsetToNextRoom.x,
                                                        y - startPosLB.y + cellOffsetToNextRoom.y,
                                                        startPosLB.z),
                                                        tile);
                    }
                }
            }
        }

        private void CopyObjectsToRoom(GeneratedRoom generatedRoom,Vector3Int cellOffsetToNextRoom)
        {
            foreach(Transform objectTransform in Objects)
            {
                var positionOffset = objectTransform.position -Grid.CellToWorld(Grid.WorldToCell(LB.position));
                var realWorldPosition = generatedRoom.ToGrid.CellToWorld(cellOffsetToNextRoom)+positionOffset;
                var newObjectTransform = Instantiate(objectTransform,generatedRoom.ToObjects);
                newObjectTransform.position = realWorldPosition;
            }
        }
    }
}