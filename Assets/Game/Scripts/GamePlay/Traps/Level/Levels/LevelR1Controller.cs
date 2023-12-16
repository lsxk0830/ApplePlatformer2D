using System.Collections.Generic;
using System.Linq;
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
            var initRoomTemplates = InitRoomTemplates.ToList();
            var shootRoomTemplates = ShootRoomTemplates.ToList();
            var emptyRoomTemplates = EmptyRoomTemplates.ToList();
            var finalRoomTemplates = FinalRoomTemplates.ToList();

            var generator = GetComponent<GeneratedRoom>();
            // 先生成初始房间
            generator.GenerateRoomWithTemplate(GetRoomTemplate(initRoomTemplates));

            // 然后生成 2 个战斗房间
            generator.GenerateRoomWithTemplate(GetRoomTemplate(shootRoomTemplates));
            generator.GenerateRoomWithTemplate(GetRoomTemplate(shootRoomTemplates));

            // 然后生成 1 个空白房间
            generator.GenerateRoomWithTemplate(GetRoomTemplate(emptyRoomTemplates));

            // 再生成 2 个战斗房间
            generator.GenerateRoomWithTemplate(GetRoomTemplate(shootRoomTemplates));
            generator.GenerateRoomWithTemplate(GetRoomTemplate(shootRoomTemplates));

            // 生成 1 个空白房间
            generator.GenerateRoomWithTemplate(GetRoomTemplate(emptyRoomTemplates));

            // 生成最终房间
            generator.GenerateRoomWithTemplate(GetRoomTemplate(finalRoomTemplates));

            OnCurrentLevelPassed.Register(() =>
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
            var randomIndex = Random.Range(0, templates.Count);
            var returnRoomTemplate = templates[randomIndex];
            templates.RemoveAt(randomIndex);
            return returnRoomTemplate;
        }
    }
}