using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugIntroTransition : MonoBehaviour {

    private bool play;
    private bool active;
    [SerializeField] private GameObject[] objects;

	// Use this for initialization
	void Awake () {
		play = false;
        active = false;
        for (int i = 0; i < objects.Length; i++){
            objects[i].SetActive(false);
        }
	}

    void Update() {
        if (play){
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(GetComponent<SpriteRenderer>().color.a, 1, Time.deltaTime * 2f));
            if (GetComponent<SpriteRenderer>().color.a >= 0.99f){
                SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
            }
        }
        else {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(GetComponent<SpriteRenderer>().color.a, 0, Time.deltaTime * 3f));
            if (GetComponent<SpriteRenderer>().color.a < 0.2f){
                if (!active){
                    active = true;
                    for (int i = 0; i < objects.Length; i++){
                        objects[i].SetActive(true);
                    }
                }
            }

            Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0)){
                if (Vector2.Distance(mouse_position, new Vector2(0, -0.8f)) < 1f && mouse_position.y > -1.5f){
					AudioSource new_aus = gameObject.AddComponent<AudioSource>();
					new_aus.clip = Resources.Load("AudioClip/ButtonClickSFX") as AudioClip;
					new_aus.Play();
                    play = true;
                }
                else if (Vector2.Distance(mouse_position, new Vector2(0, -2.36f)) < 0.8f){
                    Application.Quit();
                }
            }
        }
    }
}
