using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Scripts
{
    public class JoystickSync : EventTrigger
    {
        private Vector3 _zeroPosition;
        private string _horizontalAxisName;
        private string _verticalAxisName;

        private bool _drag;

        public void Start()
        {
            _zeroPosition = GetComponent<RectTransform>().localPosition;
            _horizontalAxisName = GetComponent<Joystick>().horizontalAxisName;
            _verticalAxisName = GetComponent<Joystick>().verticalAxisName;
        }

        public void Update()
        {
            if (_drag) return;

            var direction = new Vector3(CrossPlatformInputManager.GetAxis(_horizontalAxisName), CrossPlatformInputManager.GetAxis(_verticalAxisName)) +
                new Vector3(Input.GetAxis(_horizontalAxisName), Input.GetAxis(_verticalAxisName));

            GetComponent<RectTransform>().localPosition = _zeroPosition + 50 * direction;
        }

        public override void OnBeginDrag(PointerEventData data)
        {
            _drag = true;
        }

        public override void OnEndDrag(PointerEventData data)
        {
            _drag = false;
        }
    }
}
