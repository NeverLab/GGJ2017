using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    public string PikableTagName = "Pikable";
    public string BunnyTagName = "Bunny";
    public SteamVR_TrackedController TrackController;
    private Transform _target;
    private bool _catched = false;
    private bool _lastTriggerclickState = false;

    public void OnTriggerEnter (Collider target) {
        if (_catched)
            return;

        if (target.transform.tag != PikableTagName)
            return;

        _target = target.transform;
    }

    public void OnTriggerExit (Collider target) {
        if(target.transform == _target)
            _target = null;
    }

    void Update() {        
        if(!TrackController.triggerPressed) {
            _catched = false;
        }
        if (_target != null) {
            if (!_catched && TrackController.triggerPressed) {
                _catched = true;
            }
            if (_target.parent.tag == BunnyTagName) {
                var bunny = _target.parent.GetComponent<Bunny> ();
                bunny.catched = _catched;
                bunny.transform.position = transform.position;
            }
        }
    }
}
