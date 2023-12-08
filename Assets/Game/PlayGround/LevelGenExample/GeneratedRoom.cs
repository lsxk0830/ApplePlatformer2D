using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Blue
{
    public class GeneratedRoom : MonoBehaviour
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
        public Vector3Int CurrentRoomRighConnectionCellPos = Vector3Int.zero;

        public void GenerateRoomWithTemplate(RoomTemplate roomTemplate)
        {
            roomTemplate.CopyRoomTemplateToBase(this);
        }
    }
}