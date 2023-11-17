using QFramework;
using UnityEngine;

namespace Blue
{
    public interface IInputSystem : ISystem
    {
        float HorizontalInput { get; }
        float VerticalInput { get; }
        bool JumpDown { get; }
        bool Jump { get; }
        bool JumpUp { get; }
    }

    public class InputSystem : AbstractSystem, IInputSystem
    {
        public float HorizontalInput => Input.GetAxisRaw("Horizontal");
        public float VerticalInput => Input.GetAxisRaw("Vrtical");

        public bool JumpDown => Input.GetButtonDown("Jump");
        public bool Jump => Input.GetButton("Jump");
        public bool JumpUp => Input.GetButtonUp("Jump");

        protected override void OnInit()
        {

        }
    }
}