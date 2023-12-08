using System.Collections;
using System.Collections.Generic;
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
    }
}