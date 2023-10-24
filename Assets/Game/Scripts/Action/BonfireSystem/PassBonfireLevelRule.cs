using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 
    /// </summary>
    public class PassBonfireLevelRule : MonoBehaviour
    {
        public string LevelName;
        public void Execute()
        {
            var levelRule = ApplePlatformer2D.Interface
                            .GetSystem<IBonfireSystem>()
                            .GetRuleByKey(LevelName) as AbstractBonfireLevelRule;

            levelRule.Passed = true;
        }
    }
}