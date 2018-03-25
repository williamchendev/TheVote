
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //Settings
    private static GameManager manager;
    private Canvas uicanvas;

    //Save System
    private SaveFile save_file;
    
    //Player Scene Management
    private bool init_scene;
    private int[] player_inventory;
    private bool player_facing;
    private int player_door;

	//Init Manager
	void Awake () {
        //Set Resolution
        //Screen.SetResolution(1920, 1080, true, 60);

		//Create Save
        save_file = new SaveFile();

        //Create Manager
        bool exists = (GameObject.Find("GameManager") != true);
        if (exists){
            gameObject.name = "GameManager";
            DontDestroyOnLoad(this.gameObject);
            manager = gameObject.GetComponent<GameManager>();
        }
        else {
            Destroy(this.gameObject);
            return;
        }

        //Settings
        uicanvas = GetComponentInChildren<Canvas>();
        init_scene = false;
	}

    //Scene Load Event
    void OnEnable () {
      SceneManager.sceneLoaded += OnSceneLoaded;
    }
 
    void OnDisable () {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
 
    private void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        if (init_scene){
            Vector2 player_position = Vector2.zero;
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
            if (doors.Length > 0){
                for (int d = 0; d < doors.Length; d++){
                    if (doors[d].GetComponent<DoorScript>().door == player_door){
                        player_position = doors[d].GetComponent<InteractableBehavior>().getPosition();
                        break;
                    }
                }
            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null){
                player = Instantiate((Resources.Load("pPlayer")) as GameObject, new Vector3(player_position.x, player_position.y, 0f), (((Resources.Load("pPlayer")) as GameObject).transform.rotation));
            }
            for (int i = 0; i < player_inventory.Length; i++){
                player.GetComponent<PlayerBehavior>().addItem(player_inventory[i]);
            }
            player.transform.position = new Vector3(player_position.x, player_position.y, 0f);
            player.GetComponent<SpriteRenderer>().flipX = player_facing;
            player.GetComponent<PlayerBehavior>().hideItem();
            init_scene = false;
        }

        uicanvas.worldCamera = Camera.main;
    }

    //Update Event
    void Update () {
        if (Input.GetKeyDown(KeyCode.R)){

        }
	}

    //Scene Management
    public void changeScene (string name, int door_num){
        init_scene = true;
        PlayerBehavior player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        player.cleanItem();
        player_inventory = player.allItem();
        player_facing = player.gameObject.GetComponent<SpriteRenderer>().flipX;
        player_door = door_num;
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    //Save Management
    public SaveFile save {
        get {
            return save_file;
        }
    }

    //Instance Methods
    public static GameManager instance {
        get {
            return manager;
        }
    }

    public Canvas canvas {
        get {
            return uicanvas;
        }
    }
}

[Serializable]
public class SaveFile {

    //Settings
    [SerializeField] private string save_place;
    [SerializeField] private bool[] save_keys;

    public SaveFile() {
        save_place = null;
        save_keys = new bool[5];
    }

    //Key functions
    public bool getKey (int key) {
        return save_keys[key];
    }

    public void setKey (int key, bool val) {
        save_keys[key] = val;
    }

    //File functions
    public void saveFile() {
        string json_save = JsonUtility.ToJson(this, true);
        string path = Application.streamingAssetsPath + "/Saves/Save.json";
        if (!File.Exists(path)){
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Saves");
        }
        File.WriteAllText (path, json_save);
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    public void loadFile() {
        string path = Application.streamingAssetsPath + "/Saves/Save.json";
        if (File.Exists(path)){
            string json_save = File.ReadAllText(path);
            SaveFile temp_save = JsonUtility.FromJson<SaveFile>(json_save);

            //Transfer over data
            save_place = temp_save.place;
            save_keys = temp_save.keys;
        }
        else {
            Debug.LogError("File not found exception");
        }
    }

    //Get & Set functions
    public string place {
        get {
            return save_place;
        }
        set {
            save_place = value;
        }
    }

    public bool[] keys {
        get {
            return save_keys;
        }
        set {
            save_keys = value;
        }
    }

}