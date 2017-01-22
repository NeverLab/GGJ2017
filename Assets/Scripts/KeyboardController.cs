using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Scripts
{
    public class KeyboardController: MonoBehaviour
    {
        public KeyCode Left = KeyCode.A;
        public KeyCode Right = KeyCode.D;
        public KeyCode Up = KeyCode.W;
        public KeyCode Down = KeyCode.S;
        public RectTransform Joystick;

        private int _controllerId;
        private string _horizontalAxisName;
        private string _verticalAxisName;

        public void Start()
        {
            var controllerId = GetComponent<Bunny>().ControllerId;

            _horizontalAxisName = "Horizontal" + controllerId;
            _verticalAxisName = "Vertical" + controllerId;
        }

        public void Update()
        {
            CrossPlatformInputManager.SetAxis(_horizontalAxisName, Input.GetKey(Left) ? -1 : Input.GetKey(Right) ? 1 : 0);
            CrossPlatformInputManager.SetAxis(_verticalAxisName, Input.GetKey(Down) ? -1 : Input.GetKey(Up) ? 1 : 0);
        }
    }
}
