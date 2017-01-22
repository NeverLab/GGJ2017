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
        
        public static Dictionary<int, BunnyCount> Dict = new Dictionary<int, BunnyCount>();

        public void Awake()
        {
            Dict.Add(ControllerId, this);
        }

        public void OnDestroy()
        {
            Dict.Remove(ControllerId);
        }

        public static void Refresh(int controllerId)
        {
            Dict[controllerId].GetComponent<Text>().text = FindObjectsOfType<Bunny>().Count(i => i.ControllerId == controllerId).ToString();
        }

        public static string GetBest()
        {
            var index = Dict.OrderByDescending(i => FindObjectsOfType<Bunny>().Count(j => j.ControllerId == i.Key)).First().Key;

            return new[] { "red", "blue", "green", "pink", "orange", "violet", "cyan", "gray" }[index];
        }
    }
}