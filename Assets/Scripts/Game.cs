using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        public Slider Slider;
        public float CloneInterval = 5;
        public int MaxBunnies = 32;

        public static Game Instance;
        private float _cloneTime;

        public void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            StartCoroutine(Clone(CloneInterval));
        }

        public void Update()
        {
            Slider.value = (Time.time - _cloneTime) / CloneInterval;
        }

        public IEnumerator Clone(float interval)
        {
            yield return new WaitForSeconds(interval);

            StartCoroutine(Clone(interval));

            var groups = FindObjectsOfType<Bunny>().GroupBy(i => i.ControllerId).ToDictionary(i => i.Key, i => i.ToList());

            foreach (var key in groups.Keys)
            {
                groups[key][Random.Range(0, groups[key].Count)].Clone();
            }

            _cloneTime = Time.time;
        }
    }
}