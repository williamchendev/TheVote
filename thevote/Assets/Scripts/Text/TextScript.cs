using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

    //Text
    [SerializeField] private string text;
    [SerializeField] private Color color;

    //Components
    private Camera cam;

    private Text text_obj;
    private GameObject hBox;
    private GameObject vBox;
    private GameObject[] corners;

    //Settings
    private Vector3 position;

    private float spd;
    private float offset;
    private float sin_offset;
    private float sin_val;
    private float texttimer;

    private float width;
    private float height;

    private bool destroy;

	//Init Variables
    void Awake () {
        //Get Text Object
        text_obj = transform.GetChild(0).GetComponent<Text>();
        hBox = transform.GetChild(1).gameObject;
        vBox = transform.GetChild(2).gameObject;
        corners = new GameObject[4];
        for (int i = 0; i < 4; i++){
            corners[i] = transform.GetChild(i + 3).gameObject;
        }

        spd = 0.42f;
		color = Color.white;
        
        //Settings
        offset = 9f;
        sin_offset = 6f;
        sin_val = 0;
        texttimer = 0;

        width = 0f;
        height = 0f;

        destroy = false;
	}

	public void Start () {
        //Set Text Box Settings
		text_obj.color = color;
        for (int i = 0; i < 4; i++){
            corners[i].GetComponent<SpriteRenderer>().enabled = true;
        }

        //Camera
        transform.parent = GameManager.instance.canvas.gameObject.transform;
        cam = GameManager.instance.canvas.worldCamera;
        position = new Vector3(transform.position.x, transform.position.y, -1f * GameManager.instance.canvas.transform.childCount);

        //Set Text Details
        Vector3 real_position = new Vector3(((cam.transform.position.x / 32) + position.x), ((cam.transform.position.y / 32) + position.y), position.z);
        real_position = new Vector3(real_position.x - (real_position.x % 0.03125f), real_position.y - (real_position.y % 0.03125f), real_position.z);
        transform.position = real_position;

		//Draw Sin
        sin_val += 0.009f;
		if (sin_val > 1f){
            sin_val = 0f;
        }
        float draw_sin = (Mathf.Sin(sin_val * 2 * Mathf.PI) + 1) / 2f;

        //Text Calculations
        if (!destroy){
            if (text_obj.text.Length < text.Length){
                if (texttimer < 1){
                    texttimer += spd;
                }
                else {
                    texttimer = 0;
                    text_obj.text = text.Substring(0, text_obj.text.Length + 1);
                }
            }
        }

        //Box Dimensions
        width = Mathf.Clamp(text_obj.preferredWidth, 0, text_obj.gameObject.GetComponent<RectTransform>().sizeDelta.x) + offset;
        height = text_obj.preferredHeight + offset;
        width = Mathf.Round(width) + Mathf.Round(draw_sin * sin_offset);
        height = Mathf.Round(height) + Mathf.Round(draw_sin * sin_offset);  

        hBox.transform.localScale = new Vector3(width, height - 10, 1);
        vBox.transform.localScale = new Vector3(width - 10, height, 1);

        //Corner placement
        corners[0].transform.position = new Vector3(real_position.x - ((width / 32) / 2), real_position.y + ((height / 32) / 2), real_position.z);
        corners[1].transform.position = new Vector3(real_position.x + ((width / 32) / 2), real_position.y + ((height / 32) / 2), real_position.z);
        corners[2].transform.position = new Vector3(real_position.x + ((width / 32) / 2), real_position.y - ((height / 32) / 2), real_position.z);
        corners[3].transform.position = new Vector3(real_position.x - ((width / 32) / 2), real_position.y - ((height / 32) / 2), real_position.z);
	}
	
	//Update Event
	void Update () {
        //Set Positions
        Vector3 real_position = new Vector3(((cam.transform.position.x / 32) + position.x), ((cam.transform.position.y / 32) + position.y), position.z);
        real_position = new Vector3(real_position.x - (real_position.x % 0.03125f), real_position.y - (real_position.y % 0.03125f), real_position.z);
        transform.position = real_position;

		//Draw Sin
        sin_val += 0.009f;
		if (sin_val > 1f){
            sin_val = 0f;
        }
        float draw_sin = (Mathf.Sin(sin_val * 2 * Mathf.PI) + 1) / 2f;

        //Text Calculations
        if (!destroy){
            if (text_obj.text.Length < text.Length){
                if (texttimer < 1){
                    texttimer += spd;
                }
                else {
                    texttimer = 0;
                    text_obj.text = text.Substring(0, text_obj.text.Length + 1);
                }
            }
        }

        //Check Click
        if (Input.GetMouseButtonDown(0)){
            if (text_obj.text.Length < text.Length){
                text_obj.text = text;
            }
            else {
                destroy = true;
                if (text_obj != null){
                    Destroy(text_obj.gameObject);
                }
            }
        }

        //Box Dimensions
        if (!destroy){
            width = Mathf.Clamp(text_obj.preferredWidth, 0, text_obj.gameObject.GetComponent<RectTransform>().sizeDelta.x) + offset;
            height = text_obj.preferredHeight + offset;
            width = Mathf.Round(width) + Mathf.Round(draw_sin * sin_offset);
            height = Mathf.Round(height) + Mathf.Round(draw_sin * sin_offset);
        }
        else {
            float shrink_spd = 8f;
            width = Mathf.Clamp(Mathf.Round(width - shrink_spd), 8, width);
            height = Mathf.Clamp(Mathf.Round(height - shrink_spd), 20, height);
        }       

        hBox.transform.localScale = new Vector3(width, height - 10, 1);
        vBox.transform.localScale = new Vector3(width - 10, height, 1);

        //Corner placement
        corners[0].transform.position = new Vector3(real_position.x - ((width / 32) / 2), real_position.y + ((height / 32) / 2), real_position.z);
        corners[1].transform.position = new Vector3(real_position.x + ((width / 32) / 2), real_position.y + ((height / 32) / 2), real_position.z);
        corners[2].transform.position = new Vector3(real_position.x + ((width / 32) / 2), real_position.y - ((height / 32) / 2), real_position.z);
        corners[3].transform.position = new Vector3(real_position.x - ((width / 32) / 2), real_position.y - ((height / 32) / 2), real_position.z);

        //Destroy
        if (destroy){
            if (width < 10){
                Destroy(gameObject);
            }
        }
	}

    //Get and Set Methods
    public string textContent {
        set {
            text = value;
        }
    }

    public Color colorContent {
        set {
            color = value;
        }
    }

}
