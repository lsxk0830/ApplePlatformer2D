using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace Blue
{
    public class LevelR1Controller : LevelController
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

            OnCurrentLevelPassed.Register(()=>
            {
                // 处理通关的逻辑
                var bonfireSystem = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>();
                var levelR1 = bonfireSystem.GetRuleByKey("LevelR1");
                var genericLevel = levelR1 as GenericlLevel;
                genericLevel.Passed = true; // 设置通关状态
                genericLevel.Unlocked = false; // 设置未解锁状态
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private RoomTemplate GetRoomTemplate(List<RoomTemplate> templates)
        {
            return templates[Random.Range(0, templates.Count)];
        }
    }
}