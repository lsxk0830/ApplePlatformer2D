using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.SceneManagement;

namespace Blue
{
    public class LoadSceneTask : Action
    {
        public SharedString SceneName;

        /// <summary>
        /// 执行逻辑，返回TaskStatus.Success则为完成
        /// </summary>
        /// <returns></returns>
        public override TaskStatus OnUpdate()
        {
            SceneManager.LoadScene(SceneName.Value);
            return TaskStatus.Success;
        }
    }
}