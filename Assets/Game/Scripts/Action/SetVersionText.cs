using UnityEngine;
using UnityEngine.UI;

namespace Blue
{
    [RequireComponent(typeof(Text))] // RequireComponent 属性自动将所需的组件添加为依赖项
    public class SetVersionText : MonoBehaviour
    {
        public void Execute()
        {
            GetComponent<Text>().text = "v" + Application.version;
        }
    }
}