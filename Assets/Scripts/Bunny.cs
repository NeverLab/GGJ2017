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
        public ParticleSystem Trail;
        public Material[] TrailMaterials;
        public Renderer Renderer;
        public Material[] RabbitMaterials;

        private Vector3 _direction = Vector3.forward;

        public void Start()
        {
            Trail.GetComponent<Renderer>().material = TrailMaterials[ControllerId - 1];
            Renderer.material = RabbitMaterials[ControllerId - 1];
        }
        
        public void Update()
        {
            var x = CrossPlatformInputManager.GetAxis("Horizontal" + ControllerId) + Input.GetAxis("Horizontal" + ControllerId);
            var y = CrossPlatformInputManager.GetAxis("Vertical" + ControllerId) + Input.GetAxis("Vertical" + ControllerId);

            _direction = new Vector3(x, 0, y);

            if (_direction.magnitude > 0)
            {
                transform.LookAt(transform.position + _direction);
            }

            AnimatorController.SetBool(AnimatorRunFlag, _direction.magnitude > 0);
            Trail.enableEmission = _direction.magnitude > 0;

            _direction.y -= 320 * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Clone();
            }
        }

        public void FixedUpdate()
        {
            GetComponent<CharacterController>().Move(_direction * Speed * Time.fixedDeltaTime);
        }

        private void Clone()
        {
            var offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

            Instantiate(this, transform.position + offset, transform.rotation, transform.parent).transform.localScale = transform.localScale;
        }
    }
}