using UnityEngine;
using UnityEngine.UI;

namespace Blue
{
    public class KeyboardAutoSelect : MonoBehaviour
    {
        public Selectable AutoSelectTarget;

        public enum InputStates
        {
            Keyboard,
            Mouse,
        }

        public InputStates InputState = InputStates.Keyboard;

        void Update()
        {
            if (InputState == InputStates.Keyboard)
            {
                if (Input.GetMouseButtonDown(0) || // 鼠标左键
                    Input.GetMouseButtonDown(1) || // 鼠标右键
                    Input.GetMouseButtonDown(2) || // 鼠标中键
                    Input.mouseScrollDelta != Vector2.zero) // 鼠标滚动
                {
                    InputState = InputStates.Mouse;
                }
            }
            else if (InputState == InputStates.Mouse)
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetMouseButtonDown(0) || // 鼠标左键
                        Input.GetMouseButtonDown(1) || // 鼠标右键
                        Input.GetMouseButtonDown(2)) // 鼠标中键
                    {

                    }
                    else // 不是鼠标按下的情况下
                    {
                        InputState = InputStates.Keyboard;
                        AutoSelectTarget.Select();
                    }
                }
            }
        }
    }
}