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
        public Collider[] Colliders;
        public bool StopByMusic = false;
        public bool Catched;

        private Vector3 _direction = Vector3.forward;
        private static int _count;

        public void Start()
        {
            Trail.GetComponent<Renderer>().material = TrailMaterials[ControllerId - 1];
            Renderer.material = RabbitMaterials[ControllerId - 1];
            _count++;
        }

        public void OnDestroy()
        {
            BunnyCount.Refresh (ControllerId);
            _count--;
        }
        
        public void Update()
        {
            if (Catched)
            {
                _direction = Vector3.zero;
                foreach (var collider in Colliders)
                    collider.enabled = false;
                return;
            }
            foreach (var collider in Colliders)
                collider.enabled = true;

            if (StopByMusic)
                return;

            var x = CrossPlatformInputManager.GetAxis("Horizontal" + ControllerId) + Input.GetAxis("Horizontal" + ControllerId);
            var y = CrossPlatformInputManager.GetAxis("Vertical" + ControllerId) + Input.GetAxis("Vertical" + ControllerId);

            _direction = new Vector3(x, 0, y).normalized;

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
            if (Catched) {
                return;
            }
            foreach (var collider in Colliders)
                collider.enabled = true;

            GetComponent<CharacterController>().Move(_direction * Speed * Time.fixedDeltaTime);
        }

        public void Clone()
        {
            if (Catched || _count >= Game.Instance.MaxBunnies) return;

            var angle = Random.Range(0, Mathf.PI);
            var offset = new Vector3(Mathf.Sign(angle), 0, Mathf.Cos(angle)) * transform.lossyScale.x;

            Instantiate(this, transform.position + offset, transform.rotation, transform.parent).transform.localScale = transform.localScale;
            BunnyCount.Refresh(ControllerId);
        }
    }
}