using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    //Movement Settings
    private PathScript path;
    private float spd;

    private bool canmove;
    private bool cutscene;
    private bool moving;
    private int path_num;
    private Vector2[] path_array;

    //Select Settings
    private InteractableBehavior interact;

    //Inventory
    private bool inventory_active;
    private GameObject[] inventory;

    //Init Player
	void Awake () {
        //Grid
        path = gameObject.AddComponent(typeof(PathScript)) as PathScript;
        path.Grid = (Grid) GameObject.FindObjectOfType(typeof(Grid));

        //Movement Settings
        canmove = true;
        cutscene = false;
        moving = false;
		spd = 1.8f;
        path_num = 0;

        //Inventory
        inventory_active = false;
        inventory = new GameObject[6];
        GameObject inventory_obj = (Resources.Load("Prefabs/pInvBlip")) as GameObject;
        for (int i = 0; i < inventory.Length; i++){
            GameObject temp_inv = Instantiate(inventory_obj, transform.position, transform.rotation);
            inventory[i] = temp_inv;
            inventory[i].transform.parent = transform;
            inventory[i].GetComponent<InventoryScript>().setItem(-1);
        }
	}
	
	//Update Event
	void Update () {
        //Movement
        if (canmove){
            if (Input.GetMouseButtonDown(0)){
                //Get Mouse Point
                Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                bool position_found = false;
                Vector2 move_v2 = new Vector2(v3.x, v3.y);

                //Reset last Interact
                if (interact != null){
                    interact.selected = false;
                    interact = null;
                }

                if (Vector2.Distance(move_v2, new Vector2(transform.position.x, transform.position.y + 1.7f)) < 0.5f){
                    canmove = false;
                    inventory_active = true;
                    for (int q = 0; q < inventory.Length; q++){
                        inventory[q].GetComponent<InventoryScript>().setActive = true;
                        inventory[q].GetComponent<InventoryScript>().setAngle((Mathf.PI + (1.0471975512f * q)) + Random.Range(-0.4f, 0.4f) + 0.52359877559f);
                    }
                }

                //Check Point
                RaycastHit2D[] click_ray = Physics2D.RaycastAll(new Vector2(v3.x, v3.y), Vector2.zero, Mathf.Infinity);
                Vector2 move_click_v2 = new Vector2(move_v2.x, move_v2.y);
                int importance = 10;
                for (int i = 0; i < click_ray.Length; i++){
                    if (click_ray[i].collider != null){
                        bool higher_importance = false;
                        if (click_ray[i].collider.gameObject.tag == "Item"){
                            if (importance > 0){
                                importance = 0;
                                higher_importance = true;
                            }
                        }
                        else if (click_ray[i].collider.gameObject.tag == "NPC"){
                            if (importance > 1){
                                importance = 1;
                                higher_importance = true;
                            }
                        }
                        else if (click_ray[i].collider.gameObject.tag == "Banter"){
                            if (importance > 2){
                                importance = 2;
                                higher_importance = true;
                            }
                        }
                        else if (click_ray[i].collider.gameObject.tag == "Move"){
                            if (importance > 3){
                                importance = 3;
                                higher_importance = true;
                            }
                        }

                        if (higher_importance){
                            interact = click_ray[i].collider.gameObject.GetComponent<InteractableBehavior>();
                            interact.selected = true;
                            move_click_v2 = click_ray[i].collider.gameObject.GetComponent<InteractableBehavior>().getPosition();
                            position_found = true;
                        }
                    }
                }

                //Find point to move
                if (!position_found){
                    RaycastHit2D space_ray = Physics2D.CircleCast(new Vector2(v3.x, v3.y), (0.5f / 32f), Vector2.zero, Mathf.Infinity, path.Grid.spaceLayer, -Mathf.Infinity, Mathf.Infinity);
                    RaycastHit2D solid_ray = Physics2D.CircleCast(new Vector2(v3.x, v3.y), (0.5f / 32f), Vector2.zero, Mathf.Infinity, path.Grid.solidLayer, -Mathf.Infinity, Mathf.Infinity);
                    if ((space_ray.collider == null) || (solid_ray.collider != null)){
                        move_v2 = path.closestPoint(move_v2);
                    }
                }
                else {
                    move_v2 = move_click_v2;
                }

                //Set move path to point
                path_num = 0;
                path_array = path.getPath(new Vector2(transform.position.x, transform.position.y), move_v2);
                if (path_array == null){
                    path_array = new Vector2[1];
                    path_array[0] = new Vector2(transform.position.x, transform.position.y);
                }
                else {
                    path_array[0] = new Vector2(transform.position.x, transform.position.y);
                }
                moving = true;
            }
        }
        else {
            moving = false;
            if (cutscene){
                moving = true;
            }

            if (inventory_active){
                if (Input.GetMouseButtonDown(0)){
                    Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    canmove = true;
                    inventory_active = false;
                    for (int q = 0; q < inventory.Length; q++){
                        inventory[q].GetComponent<InventoryScript>().setActive = false;
                    }
                    if (Vector2.Distance(new Vector2(v3.x, v3.y), new Vector2(transform.position.x, transform.position.y + 1.7f)) >= 0.5f){
                        Update();
                        return;
                    }
                }
            }
        }

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
                if (interact != null){
                    if (interact.selected){
                        interact.action = true;
                        interact.selected = false;
                        interact = null;
                    }
                }
            }
        }
	}

    //Inventory Methods
    public bool addItem(int item) {
        bool found_space = false;
        for (int i = 0; i < inventory.Length; i++){
            if (inventory[i].GetComponent<InventoryScript>().itemnum == -1){
                inventory[i].GetComponent<InventoryScript>().setItem(item);
                found_space = true;
                break;
            }
        }
        if (found_space){
            inventory_active = true;
            for (int q = 0; q < inventory.Length; q++){
                inventory[q].GetComponent<InventoryScript>().setActive = true;
                inventory[q].GetComponent<InventoryScript>().setAngle((Mathf.PI + (1.0471975512f * q)) + Random.Range(-0.4f, 0.4f) + 0.52359877559f);
            }
            canmove = false;
        }
        return found_space;
    }

    public bool removeItem(int item) {
        bool found_space = false;
        for (int i = 0; i < inventory.Length; i++){
            if (inventory[i].GetComponent<InventoryScript>().itemnum == item){
                inventory[i].GetComponent<InventoryScript>().setItem(-1);
                found_space = true;
                break;
            }
        }
        return found_space;
    }


    //Player Settings
    public bool can_move {
        get {
            return canmove;
        }
        set {
            canmove = value;
        }
    }

    public bool inventory_act {
        get {
            return inventory_active;
        }
    }

    public bool cut_scene {
        get {
            return cutscene;
        }
    }

    public void moveTo(Vector2 v2) {
        path_num = 0;
        path_array = path.getPath(new Vector2(transform.position.x, transform.position.y), v2);
        if (path_array == null){
            path_array = new Vector2[1];
            path_array[0] = new Vector2(transform.position.x, transform.position.y);
        }
        else {
            path_array[0] = new Vector2(transform.position.x, transform.position.y);
            moving = true;
            cutscene = true;
        }
    }

    //Debug
    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        if (path_array != null){
            for (int i = 0; i < path_array.Length; i++){
                Gizmos.DrawSphere(new Vector3(path_array[i].x, path_array[i].y, transform.position.z), 0.05f);
            }
        }
    }
}
