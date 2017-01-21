﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    //public string PikableTagName = "Pikable";
    public string BunnyTagName = "Bunny";
    public SteamVR_TrackedController TrackController;
    public Vector3 Offset = new Vector3 (0.2f, 0.0f, -0.06f);
    private Transform _target;
    private bool _catched = false;
    private Transform _catchedTarget;
    //private bool _lastTriggerclickState = false;

    public void OnTriggerStay (Collider target) {

        //Debug.LogWarning ("Stay(" + name + "): " + target.name);

        if (_catched)
            return;

        //if (target.transform.tag != PikableTagName)
        //    return;

        _target = target.transform;
    }

    public void OnTriggerExit (Collider target) {

        //Debug.LogWarning ("Exit("+name+"): " + target.name);

        if (target.transform == _target)
            _target = null;
    }

    void Update() {
        //Debug.Log ("(" + (_target != null).ToString () + ")(" + (_catched).ToString () + ")(" + (TrackController.triggerPressed).ToString () + ")");
        if(!TrackController.triggerPressed) {
            if (_catchedTarget != null) {
                //Debug.LogWarning ("Not Catched: " + _catchedTarget.name);
                var bunny = _catchedTarget/*.parent*/.GetComponent<Bunny> ();
                bunny.catched = false;
                _catchedTarget = null;
            }
            _target = null;
            _catched = false;
        }
        if (_target != null) {
            if (!_catched && TrackController.triggerPressed) {
                _catched = true;
                _catchedTarget = _target;
                //Debug.LogWarning ("Catched: " + _target.name);

            }
        }
        if(_catchedTarget != null) {
            if (_catchedTarget/*.parent*/.tag == BunnyTagName) {
                var bunny = _catchedTarget/*.parent*/.GetComponent<Bunny> ();
                bunny.catched = true;
            }

            _catchedTarget.position = transform.position + Offset;
        }
            
    }
}
