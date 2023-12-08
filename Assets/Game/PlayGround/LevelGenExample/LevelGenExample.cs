using UnityEngine;
using UnityEngine.Tilemaps;

namespace Blue
{
    public class LevelGenExample : MonoBehaviour
    {
        // 从哪里复制
        public RoomTemplate RoomTemplate;

        // 要复制到的地方
        public Grid ToGrid;
        public Tilemap ToGround;
        public Transform ToObjects;

        private void Start()
        {
            CopyRoomTemplateToBase(RoomTemplate, Vector3Int.zero);

            // 第二个得到生成需要在第一个RoomTemplate的RightConnection右边
            var firstRightConnection = RoomTemplate.RightConnectionPoint; // 上一个房间的RightConnection
            // 将世界位置转换为 Tilemap 坐标
            var firstRightCellPos = RoomTemplate.Grid.WorldToCell(firstRightConnection.position)-
                                    RoomTemplate.Grid.WorldToCell(RoomTemplate.LB.position);

            // 下一个 RoomTemplate 的起始需要将下一个RoomTemplate的LeftConnection和当前的Room的Right重合
            //第二个Room的left在自己的Tilemap的坐标位置是多少
            var secondLeftConnection = RoomTemplate.LeftConnectionPoint;
            var secondLeftCellPos = RoomTemplate.Grid.WorldToCell(secondLeftConnection.position);
            // 获取第二个Room的LB的CellPos
            var secondLBCellPos = RoomTemplate.Grid.WorldToCell(RoomTemplate.LB.position);
            // 计算连接点和初始位置的偏移
            var cellOffsetConnectionToLB = secondLeftCellPos - secondLBCellPos;
            // 计算第二个Room的LB应该从哪里开始
            var secondRoomCellPos = firstRightCellPos - cellOffsetConnectionToLB;
            CopyRoomTemplateToBase(RoomTemplate, secondRoomCellPos);
        }

        private void CopyRoomTemplateToBase(RoomTemplate roomTemplate, Vector3Int cellOffset)
        {
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
                        ToGround.SetTile(new Vector3Int(x - startPosLB.x + cellOffset.x,
                                                        y - startPosLB.y + cellOffset.y,
                                                        startPosLB.z),
                                        tile);
                    }
                }
            }
        }
    }
}