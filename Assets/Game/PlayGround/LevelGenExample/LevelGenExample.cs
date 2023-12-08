using UnityEngine;

namespace Blue
{
    public class LevelGenExample : GeneratedRoom
    {
        private void Start()
        {
            GenerateRoomWithTemplate(RoomTemplates[Random.Range(0, RoomTemplates.Count)]);
            GenerateRoomWithTemplate(RoomTemplates[Random.Range(0, RoomTemplates.Count)]);
            GenerateRoomWithTemplate(RoomTemplates[Random.Range(0, RoomTemplates.Count)]);
            GenerateRoomWithTemplate(RoomTemplates[Random.Range(0, RoomTemplates.Count)]);
            GenerateRoomWithTemplate(RoomTemplates[Random.Range(0, RoomTemplates.Count)]);
        }
    }
}