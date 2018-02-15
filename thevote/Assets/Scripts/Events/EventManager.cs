using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EventManager : MonoBehaviour {

    [SerializeField] private GameObject text_obj;

    //Settings
    private List<ArrayList> event_data;
    private bool active;
    private int event_type;
    private int event_num;
    private string event_hash;

    //Init
    private void Awake() {
        //Settings
        active = false;
        event_type = -1;
        event_num = 0;

        //Debug
        /*
        EventFile event_file = new EventFile();
        event_file.addText("I don't have any more space for that", Vector2.zero, "playerfixed");
        event_file.addText("I wish women's clothing had more pockets...", Vector2.zero, "playerfixed");
        event_file.addEnd();
        saveFile("noinventoryspace", event_file);
        */

        /*
        List<ArrayList> text = loadFile("test_file").getEvent();
        for (int i = 0; i < text.Count; i++){
            ArrayList temp_text = text[i];
            for (int k = 0; k < temp_text.Count; k++){
                Debug.Log(temp_text[k]);
            }
        }
        */

        //playEvent("test_file");
    }

    //Event Methods
    public void playEvent(string event_name){
        event_data = loadFile(event_name).getEvent();
        startEvent();
        eventHandler(event_data[0]);
    }

    private void startEvent() {
        //Player Settings
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerBehavior>().can_move = false;

        //NPC Settings
        GameObject[] npc = GameObject.FindGameObjectsWithTag("NPC");
        for (int i = 0; i < npc.Length; i++){
            npc[i].GetComponent<NPCBehavior>().can_move = false;
        }

        //Event Reader Settings
        active = true;
        event_type = -1;
        event_num = 0;
        event_hash = null;
    }

    private void endEvent() {
        //Player Settings
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerBehavior>().can_move = true;

        //NPC Settings
        GameObject[] npc = GameObject.FindGameObjectsWithTag("NPC");
        for (int i = 0; i < npc.Length; i++){
            npc[i].GetComponent<NPCBehavior>().can_move = true;
        }

        //Event Reader Settings
        active = false;
    }

    private void eventHandler(ArrayList event_array) {
        event_type = (int) event_array[0];
        if (event_type == 0){
            //Text Event
            string text = (string) event_array[1];
            Vector2 v2 = (Vector2) event_array[2];
            string npc_hash = (string) event_array[3];
            Color text_color = Color.white;

            if (npc_hash == "playerfixed"){
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                v2 = new Vector2(player.transform.position.x, player.transform.position.y +  3.2f);
            }
            else {
                GameObject[] check = GameObject.FindGameObjectsWithTag("NPC");
                for (int i = 0; i < check.Length; i++){
                    if (check[i].GetComponent<NPCBehavior>().hashid == npc_hash){
                        text_color = check[i].GetComponent<NPCBehavior>().textcolor;
                        break;
                    }
                }
            }
            TextScript textbox = Instantiate(text_obj, new Vector3(v2.x, v2.y, 0f), transform.rotation).GetComponent<TextScript>();

            textbox.colorContent = text_color;
            textbox.textContent = text;
            textbox.Start();
        }
        else if (event_type == 1){
            //Movement
            string npc_hash = (string) event_array[1];
            Vector2 v2 = (Vector2) event_array[2];
            event_hash = npc_hash;

            if (npc_hash == "player"){
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<PlayerBehavior>().moveTo(v2);
            }
            else {
                GameObject[] check = GameObject.FindGameObjectsWithTag("NPC");
                for (int i = 0; i < check.Length; i++){
                    if (check[i].GetComponent<NPCBehavior>().hashid == npc_hash){
                        check[i].GetComponent<NPCBehavior>().moveTo(v2);
                        break;
                    }
                }
            }
        }
        else if (event_type == 2){
            //Animation
        }
        else if (event_type == 3){
            //Camera
        }
        else if (event_type == 4){
            //Transition
        }
        else if (event_type == 5){
            //Music
        }
        else if (event_type == 6){
            //Keys
            GameManager.instance.save.setKey((int) event_array[1], true);
            event_num++;
            eventHandler(event_data[event_num]);
        }
        else if (event_type == 7){
            //End
            endEvent();
        }
    }

    //Update Event
    private void Update() {
        if (active){
            if (event_type != -1){
                if (event_type == 0){
                    int canvas_child_count = GameManager.instance.canvas.gameObject.transform.childCount;
                    if (canvas_child_count <= 0){
                        event_type = -1;
                        event_num++;
                        eventHandler(event_data[event_num]);
                    }
                }
                else if (event_type == 1){
                    if (event_hash == "player"){
                        GameObject player = GameObject.FindGameObjectWithTag("Player");
                        if (player.GetComponent<PlayerBehavior>().cut_scene == false){
                            event_hash = null;
                            event_type = -1;
                            event_num++;
                            eventHandler(event_data[event_num]);
                        }
                    }
                    else {
                        GameObject[] check = GameObject.FindGameObjectsWithTag("NPC");
                        for (int i = 0; i < check.Length; i++){
                            if (check[i].GetComponent<NPCBehavior>().hashid == event_hash){
                                if (check[i].GetComponent<NPCBehavior>().cut_scene == false){
                                    event_hash = null;
                                    event_type = -1;
                                    event_num++;
                                    eventHandler(event_data[event_num]);
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (event_type == 3){

                }
            }
        }
    }

    //Save & Load File Functionality
    public void saveFile(string file_name, EventFile file) {
        string json_save = JsonUtility.ToJson(file, true);
        string path = Application.streamingAssetsPath + "/Events/" + file_name + ".json";
        if (!File.Exists(path)){
            Directory.CreateDirectory(Application.dataPath + "/StreamingAssets/Events/");
        }
        File.WriteAllText (path, json_save);
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    public EventFile loadFile(string file_name) {
        string path = Application.streamingAssetsPath + "/Events/" + file_name + ".json";
        if (File.Exists(path)){
            string json_save = File.ReadAllText(path);
            return JsonUtility.FromJson<EventFile>(json_save);
        }
        else {
            Debug.LogError("File not found exception");
            return new EventFile();
        }
    }

    //Check Active
    public bool isActive {
        get {
            return active;
        }
    }

}
