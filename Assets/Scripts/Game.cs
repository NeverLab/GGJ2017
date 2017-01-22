using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        public Slider Slider;
        public Text Timer;
        public Text VRScore;
        public Text WinMessage;
        public float CloneInterval = 5;
        public int MaxBunnies = 32;
        public int RoundTime = 60;
        public Microwave microwave;

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
            WinMessage.transform.parent.gameObject.SetActive(false);
            StartCoroutine(Clone(2));
        }

        public void Update()
        {
            Timer.text = string.Format("Time left {0}", Mathf.Max(0, (int) TimeLeft));
            VRScore.text = string.Format("VR score {0}", microwave.Counter);
            Slider.value = TimeLeft > 0 ? (Time.time - CloneTime) / CloneInterval : 0;

            if (TimeLeft <= 0)
            {
                Time.timeScale = 0.0f;
                enabled = false;
                WinMessage.transform.parent.gameObject.SetActive(true);
                WinMessage.text = string.Format("VR score: {0}\nBest bunny: {1}", microwave.Counter, BunnyCount.GetBest());
                StopAllCoroutines();
            }
        }

        public IEnumerator Clone(float interval)
        {
            yield return new WaitForSeconds(interval);

            StartCoroutine(Clone(CloneInterval));

            var groups = FindObjectsOfType<Bunny>().GroupBy(i => i.ControllerId).ToDictionary(i => i.Key, i => i.ToList());

            foreach (var key in groups.Keys)
            {
                groups[key][Random.Range(0, groups[key].Count)].Clone();
            }

            CloneTime = Time.time;
        }

        public void Reload()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}