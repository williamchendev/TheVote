using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubChoiceScript : MonoBehaviour {

	//Text
    private int choice;
    [SerializeField] private string text;

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

    private float alpha;

    private bool destroy;
    private ChoiceScript boss;
    private EventManager em;

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

        spd = 0.36f;
        
        //Settings
        offset = 6f;
        sin_offset = 4f;
        sin_val = Random.Range(0f, 1f);
        texttimer = 0;

        width = 0f;
        height = 0f;

        destroy = false;
	}

	public void Start () {
        //Set Text Box Settings
        alpha = 0;
		text_obj.color = Color.white;
        text_obj.color = new Color(text_obj.color.r, text_obj.color.g, text_obj.color.b, alpha);
        for (int i = 0; i < 4; i++){
            corners[i].GetComponent<SpriteRenderer>().enabled = true;
        }

        //Camera
        transform.parent = GameManager.instance.canvas.gameObject.transform;
        cam = GameManager.instance.canvas.worldCamera;
        position = new Vector3(transform.position.x, transform.position.y, -1f * GameManager.instance.canvas.transform.childCount);
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

        //Check Destroy
        if (!destroy){
            if (boss.destroyself){
                destroy = true;
                if (text_obj != null){
                    Destroy(text_obj.gameObject);
                }
            }
        }

        //Text Calculations
        if (!destroy){
            alpha += 0.02f;
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
        Vector2 vA = new Vector2(corners[0].transform.position.x, corners[0].transform.position.y);
        Vector2 vB = new Vector2(corners[2].transform.position.x, corners[2].transform.position.y);
        Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool inside = false;
        if (v3.x > vA.x && v3.x < vB.x) {
            if (v3.y < vA.y && v3.y > vB.y) {
                inside = true;
            }
        }

        if (inside){
            if (Input.GetMouseButtonDown(0)){
                if (!destroy){
                    destroy = true;
                    if (text_obj != null){
                        Destroy(text_obj.gameObject);
                    }
                    em.setChoice(choice);
                    boss.destroyself = true;
                }
            }
        }
        else {
            alpha = Mathf.Clamp(alpha, 0, 0.6f);
        }

        //Box Dimensions
        if (!destroy){
            //Alpha
            text_obj.color = new Color(text_obj.color.r, text_obj.color.g, text_obj.color.b, alpha);

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

    public ChoiceScript choicescript {
        set {
            boss = value;
        }
    }

    public EventManager setEvent {
        set {
            em = value;
        }
    }

    public int choiceContent {
        set {
            choice = value;
        }
    }

}
