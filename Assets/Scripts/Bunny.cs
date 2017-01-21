using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class Bunny : MonoBehaviour
    {
        public float Speed;
        public int ControllerId;
        public Animator AnimatorController;
        public string AnimatorRunFlag = "Run";

        private Vector3 _direction = Vector3.forward;

        public bool catched = false;
        
        public void Update()
        {
            if (catched) {
                AnimatorController.SetBool (AnimatorRunFlag, false);
                return;
            }                

            var x = CrossPlatformInputManager.GetAxis("Horizontal" + ControllerId);
            var y = CrossPlatformInputManager.GetAxis("Vertical" + ControllerId);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(this).transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            }

            if (x == 0)
            {
                x = Input.GetAxis("Horizontal" + ControllerId);
            }

            if (y == 0)
            {
                y = Input.GetAxis("Vertical" + ControllerId);
            }

            AnimatorController.SetBool (AnimatorRunFlag, x != 0 || y != 0);

            _direction = new Vector3(x, 0, y);
            _direction.y -= 320 * Time.deltaTime;
        }

        public void FixedUpdate()
        {
            if (_direction.magnitude > 0)
            {
                transform.LookAt(transform.position + _direction);
            }

            GetComponent<CharacterController>().Move(_direction * Speed * Time.fixedDeltaTime);
        }
    }
}