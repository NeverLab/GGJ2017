using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : MonoBehaviour {

    public GameObject[] SuccessParticles;
    public int Counter = 0;

    public void DestroyBunny (GameObject bunnyGO) {
        Destroy (bunnyGO);
        Counter++;

        foreach (var particle in SuccessParticles) {
            particle.SetActive (false);
            particle.SetActive (true);
        }
    }

}
