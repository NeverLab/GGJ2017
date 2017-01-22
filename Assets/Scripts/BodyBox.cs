using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Prevents creatures overlap and creates opposite "force"
    /// </summary>
    public class BodyBox : MonoBehaviour
    {
        public void OnTriggerStay(Collider target)
        {
            var bunny = target.GetComponent<Bunny>();

            if (target.transform == transform.parent || bunny == null) return;

            var direction = (transform.position - target.transform.position).normalized;
            var motion = transform.lossyScale.x * new Vector3(direction.x, 0, direction.z) * Time.fixedDeltaTime;

            bunny.GetComponent<CharacterController>().Move(motion);
            //transform.parent.position += transform.lossyScale.x * new Vector3(direction.x, 0, direction.z) * Time.fixedDeltaTime;
        }
    }
}