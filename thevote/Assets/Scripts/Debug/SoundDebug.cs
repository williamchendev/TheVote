
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameManager.instance.playSoundLoop("noise");
	}

}
