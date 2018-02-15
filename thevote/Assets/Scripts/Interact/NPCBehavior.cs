using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : InteractableBehavior {

    //NPC
    [SerializeField] private string hash = "new_npc_hash";
    [SerializeField] private Color color = Color.red;
    [SerializeField] private string event_name = "";

    //Components
    private EventManager em;
    private PathScript path;

    //Settings
    private float spd;
    private bool canmove;
    private bool moving;
    private bool cutscene;
    private int path_num;
    private Vector2[] path_array;

	// Use this for initialization
	private new void Awake () {
        //Interactable
        base.Awake();

        //Settings
        em = GetComponent<EventManager>();
		gameObject.tag = "NPC";

        //Grid
        path = gameObject.AddComponent(typeof(PathScript)) as PathScript;
        path.Grid = (Grid) GameObject.FindObjectOfType(typeof(Grid));

        //Movement Settings
        canmove = false;
        cutscene = false;
        moving = false;
		spd = 1.8f;
        path_num = 0;
	}
	
	//Update Event
	private new void Update () {
        //Interactable
		base.Update();

        //NPC Behavior Event
        if (action){
            if (!em.isActive){
                em.playEvent(event_name);
                action = false;
            }
        }

        if (!canmove){
            moving = false;
            if (cutscene){
                moving = true;
            }
        }

        //NPC Movement
        if (moving){
            Vector2 current_pos = new Vector2(transform.position.x, transform.position.y);
            if (current_pos != path_array[path_array.Length - 1]){
                if (current_pos != path_array[path_num]){
                    current_pos = Vector2.MoveTowards(current_pos, path_array[path_num], spd * Time.deltaTime);
                    transform.position = new Vector3(current_pos.x, current_pos.y, transform.position.z);
                }
                else {
                    path_num++;
                }
            }
            else {
                moving = false;
                cutscene = false;
            }
        }
	}

    //NPC Movement Methods
    public void moveTo(Vector2 v2){
        //Set move path to point
        path_num = 0;
        path_array = path.getPath(new Vector2(transform.position.x, transform.position.y), v2);
        if (path_array == null){
            path_array = new Vector2[1];
            path_array[0] = new Vector2(transform.position.x, transform.position.y);
        }
        else {
            path_array[0] = new Vector2(transform.position.x, transform.position.y);
        }
        cutscene = true;
        moving = true;
    }

    public bool can_move {
        get {
            return canmove;
        }
        set {
            canmove = value;
        }
    }

    public bool cut_scene {
        get {
            return cutscene;
        }
    }

    //Get NPC data
    public string hashid {
        get {
            return hash;
        }
    }

    public Color textcolor {
        get {
            return color;
        }
    }

    //Debug Editor
    void OnDrawGizmosSelected() {
        if (Application.isEditor){
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(position.x - 0.1f, position.y, 0), new Vector3(position.x + 0.1f, position.y, 0));
            Gizmos.DrawLine(new Vector3(position.x, position.y - 0.1f, 0), new Vector3(position.x, position.y + 0.1f, 0));
            Gizmos.DrawCube(new Vector3(position.x, position.y, 0), new Vector3(0.05f, 0.05f, 0.05f));
        }
    }

}
