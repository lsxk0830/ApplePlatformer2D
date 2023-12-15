using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace Blue
{
    public class LevelR1Controller : LevelController
    {
        public List<RoomTemplate> InitRoomTemplates;
        public List<RoomTemplate> ShootRoomTemplates;
        public List<RoomTemplate> EmptyRoomTemplates;
        public List<RoomTemplate> FinalRoomTemplates;

        void Start()
        {
            var generator = GetComponent<GeneratedRoom>();
            // 先生成初始房间
            generator.GenerateRoomWithTemplate(GetRoomTemplate(InitRoomTemplates));

            // 然后生成 1 个战斗房间
            generator.GenerateRoomWithTemplate(GetRoomTemplate(ShootRoomTemplates));

            // 然后生成一个空白房间
            generator.GenerateRoomWithTemplate(GetRoomTemplate(EmptyRoomTemplates));

            // 再生成 2 个战斗房间
            generator.GenerateRoomWithTemplate(GetRoomTemplate(ShootRoomTemplates));
            generator.GenerateRoomWithTemplate(GetRoomTemplate(ShootRoomTemplates));

            // 生成 1 个空白房间
            generator.GenerateRoomWithTemplate(GetRoomTemplate(EmptyRoomTemplates));

            // 生成最终房间
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