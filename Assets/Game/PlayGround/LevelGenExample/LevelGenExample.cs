using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Blue
{
    public class LevelGenExample : MonoBehaviour
    {
        // 从哪里复制
        public List<RoomTemplate> RoomTemplates;

        // 要复制到的地方
        public Grid ToGrid;
        public Tilemap ToGround;
        public Transform ToObjects;

        /// <summary>
        /// 当前房间右节点的CellPos(相对已生成地图的原点)
        /// </summary>
        private Vector3Int mCurrentRoomRighConnectionCellPos = Vector3Int.zero;

        private void Start()
        {
            var roomTemplate = RoomTemplates[Random.Range(0, RoomTemplates.Count)];
            CopyRoomTemplateToBase(roomTemplate);
            roomTemplate = RoomTemplates[Random.Range(0, RoomTemplates.Count)];
            CopyRoomTemplateToBase(roomTemplate);
            roomTemplate = RoomTemplates[Random.Range(0, RoomTemplates.Count)];
            CopyRoomTemplateToBase(roomTemplate);
            roomTemplate = RoomTemplates[Random.Range(0, RoomTemplates.Count)];
            CopyRoomTemplateToBase(roomTemplate);
            roomTemplate = RoomTemplates[Random.Range(0, RoomTemplates.Count)];
            CopyRoomTemplateToBase(roomTemplate);
        }

        private void CopyRoomTemplateToBase(RoomTemplate roomTemplate)
        {
            if (mCurrentRoomRighConnectionCellPos == Vector3Int.zero)
                mCurrentRoomRighConnectionCellPos = roomTemplate.LeftConnectionCellPosOffset; // 第一个先是自己左边

            var cellOffsetToNextRoom = mCurrentRoomRighConnectionCellPos - roomTemplate.LeftConnectionCellPosOffset;
            mCurrentRoomRighConnectionCellPos = cellOffsetToNextRoom + roomTemplate.RightConnectionCellPosOffset;
            var startPosLB = roomTemplate.Grid.WorldToCell(roomTemplate.LB.position); // 将世界位置转换为 Tilemap 坐标
            var endPosRT = roomTemplate.Grid.WorldToCell(roomTemplate.RT.position);

            // 左下到右上复制一整片区域
            for (var x = startPosLB.x; x < endPosRT.x + 1; x++)
            {
                for (var y = startPosLB.y; y < endPosRT.y + 1; y++)
                {
                    var tile = roomTemplate.Ground.GetTile(new Vector3Int(x, y, startPosLB.z)); //根据给定的瓦片地图中某个单元格的 XYZ 坐标，获取瓦片
                    if (tile)
                    {
                        ToGround.SetTile(new Vector3Int(x - startPosLB.x + cellOffsetToNextRoom.x,
                                                        y - startPosLB.y + cellOffsetToNextRoom.y,
                                                        startPosLB.z),
                                        tile);
                    }
                }
            }
        }
    }
}