using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : MonoBehaviour {

    public GameObject[] SuccessParticles;

    public void DestroyBunny (GameObject bunnyGO) {
        Destroy (bunnyGO);

        foreach(var particle in SuccessParticles) {
            particle.SetActive (false);
            particle.SetActive (true);
        }
    }

}
