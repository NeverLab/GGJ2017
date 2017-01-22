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
            if (target.transform == transform.parent || target.GetComponent<Bunny>() == null) return;

            var direction = (transform.position - target.transform.position).normalized;

            //transform.parent.position += transform.lossyScale.x * new Vector3(direction.x, 0, direction.z) * Time.fixedDeltaTime;
        }
    }
}