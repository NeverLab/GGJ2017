using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

    public Transform LinkedObject;
    public bool IsRotatable = true;

	void Update () {
		if(LinkedObject != null) {
            transform.position = new Vector3 (LinkedObject.position.x, transform.position.y, LinkedObject.position.z);
            if(IsRotatable) transform.rotation = LinkedObject.rotation;
        }
	}
}
