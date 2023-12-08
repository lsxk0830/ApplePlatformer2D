using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blue
{
    public class LevelGenTest : MonoBehaviour
    {
        public List<RoomTemplate> InitRoomTemplates;
        public List<RoomTemplate> SomeRoomTemplates;
        public List<RoomTemplate> FinalRoomTemplates;

        void Start()
        {
            var generator = GetComponent<GeneratedRoom>();

            generator.GenerateRoomWithTemplate(GetRoomTemplate(InitRoomTemplates));

            var generateCount = Random.Range(3, 5);
            for (int i = 0; i < generateCount;i++)
            {
                generator.GenerateRoomWithTemplate(GetRoomTemplate(SomeRoomTemplates));
            }

            generator.GenerateRoomWithTemplate(GetRoomTemplate(FinalRoomTemplates));
        }

        private RoomTemplate GetRoomTemplate(List<RoomTemplate> templates)
        {
            return templates[Random.Range(0, templates.Count)];
        }
    }
}