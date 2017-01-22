using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Scripts
{
    public class JoystickSync : EventTrigger
    {
        private Vector3 _originalPosition;
        private string _horizontalAxisName;
        private string _verticalAxisName;

        private bool _drag;

        public void Start()
        {
            _originalPosition = GetComponent<RectTransform>().localPosition;
            _horizontalAxisName = GetComponent<Joystick>().horizontalAxisName;
            _verticalAxisName = GetComponent<Joystick>().verticalAxisName;
        }

        public void Update()
        {
            if (_drag) return;

            GetComponent<RectTransform>().localPosition = _originalPosition
                + 50 * new Vector3(CrossPlatformInputManager.GetAxis(_horizontalAxisName), CrossPlatformInputManager.GetAxis(_verticalAxisName));
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
