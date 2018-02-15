using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDebug : MonoBehaviour {

	//Settingsd
    private bool cutscene_bool;
    private Vector3 cutscene_pos;
    private Vector3 position;
    private GameObject follow_obj;

    //Init
    void Start() {
        follow_obj = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(follow_obj.transform.position.x, follow_obj.transform.position.y , transform.position.z);
        position = transform.position;
    }

    //Update
    void FixedUpdate () {
        Vector2 target = new Vector2(transform.position.x, transform.position.y);
        if (cutscene_bool){
            target = new Vector2(cutscene_pos.x, cutscene_pos.y);
        }
        else{
            if (follow_obj != null){
		        target = new Vector2(follow_obj.transform.position.x, follow_obj.transform.position.y);
            }
        }

        float x_pos = Mathf.Lerp(position.x, target.x, 0.05f);
        float y_pos = Mathf.Lerp(position.y, target.y, 0.05f);
        position = new Vector3(x_pos, y_pos, transform.position.z);
        transform.position = new Vector3(position.x - (position.x % 0.03125f), position.y - (position.y % 0.03125f), transform.position.z);
    }

    //Getters and Setters
    public bool cutscene {
        get {
            return cutscene_bool;
        }
        set {
            cutscene_bool = value;
        }
    }

    public Vector3 cutscenePosition {
        set {
            cutscene_pos = value;
        }
    }

}
