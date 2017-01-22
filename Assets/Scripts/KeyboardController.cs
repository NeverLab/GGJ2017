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

        public void Update()
        {
            var controllerId = GetComponent<Bunny>().ControllerId;

            CrossPlatformInputManager.SetAxis("Horizontal" + controllerId, Input.GetKey(Left) ? -1 : Input.GetKey(Right) ? 1 : 0);
            CrossPlatformInputManager.SetAxis("Vertical" + controllerId, Input.GetKey(Down) ? -1 : Input.GetKey(Up) ? 1 : 0);
        }
    }
}
