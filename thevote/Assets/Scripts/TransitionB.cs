﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionB : Transition {

	//Settings
    [SerializeField] private bool direction;

    //Variables
    private float spd;
    private GameObject tilt;

    //Init
	void Awake () {
        base.finished = true;
        base.position = transform.position;
        spd = 0.05f;

        transform.position = new Vector3(position.x, position.y, position.z);
        transform.localScale = new Vector3(15f, 8.5f, 1f);
		if (direction){
            //Comes from the Right Side
            tilt = new GameObject("TransitionB_tilt", typeof(SpriteRenderer), typeof(PixelSnap));
            tilt.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            tilt.transform.position = new Vector3(position.x + 7f, position.y, position.z);
            tilt.transform.localScale = new Vector3(4f, 11f, 1f);
            tilt.transform.eulerAngles = new Vector3(0f, 0f, 15);
        }
        else {
            //Comes from the Left Side
            tilt = new GameObject("TransitionB_tilt", typeof(SpriteRenderer), typeof(PixelSnap));
            tilt.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            tilt.transform.position = new Vector3(position.x - 7f, position.y, position.z);
            tilt.transform.localScale = new Vector3(4f, 11f, 1f);
            tilt.transform.eulerAngles = new Vector3(0f, 0f, -15);
        }
	}
	
	//Update
	void Update () {
        spd *= 1.06f;
        if (Mathf.Abs(transform.position.x - position.x) < 18){
            if (direction){
                transform.position = new Vector3(transform.position.x - spd, transform.position.y, transform.position.z);
                tilt.transform.position = new Vector3(tilt.transform.position.x - spd, tilt.transform.position.y, tilt.transform.position.z);
            }
            else {
                transform.position = new Vector3(transform.position.x + spd, transform.position.y, transform.position.z);
                tilt.transform.position = new Vector3(tilt.transform.position.x + spd, tilt.transform.position.y, tilt.transform.position.z);
            }
        }
        else {
            Destroy(tilt.gameObject);
            Destroy(gameObject);
        }
	}

}