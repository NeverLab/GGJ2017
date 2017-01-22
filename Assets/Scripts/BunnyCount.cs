using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class BunnyCount : MonoBehaviour
    {
        public int ControllerId;
        public Text Text;

        public static Dictionary<int, BunnyCount> _dict = new Dictionary<int, BunnyCount>();

        public void Awake()
        {
            _dict.Add(ControllerId, this);
        }

        public static void Refresh(int controllerId)
        {
            _dict[controllerId].GetComponent<Text>().text = FindObjectsOfType<Bunny>().Count(i => i.ControllerId == controllerId).ToString();
        }
    }
}