using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        public Slider Slider;
        public Text Timer;
        public float CloneInterval = 5;
        public int MaxBunnies = 32;
        public int RoundTime = 60;

        public static Game Instance;

        public float StartTime { get; private set; }
        public float CloneTime { get; private set; }

        public float TimeLeft { get { return RoundTime - (Time.time - StartTime); } }

        public void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            StartTime = Time.time;
            StartCoroutine(Clone(CloneInterval));
        }

        public void Update()
        {
            Timer.text = Mathf.Max(0, (int)TimeLeft).ToString();
            Slider.value = TimeLeft > 0 ? (Time.time - CloneTime) / CloneInterval : 0;
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

            CloneTime = Time.time;
        }
    }
}