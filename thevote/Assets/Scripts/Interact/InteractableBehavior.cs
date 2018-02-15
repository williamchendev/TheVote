using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehavior : InteractableInterface {

    //Components
    protected SpriteOutline outline;
    protected SpriteRenderer sr;
    protected Collider2D collider2d;

    //Settings
    [HideInInspector] public bool selected;
    [HideInInspector] public bool action;

	//Init
	protected void Awake () {
        //Components
        sr = GetComponent<SpriteRenderer>();
        outline = gameObject.AddComponent<SpriteOutline>();
        if (GetComponent<Collider2D>() != null){
            collider2d = GetComponent<Collider2D>();
        }
        else {
            collider2d = (Collider2D) gameObject.AddComponent<PolygonCollider2D>();
        }

        //Outline
        sr.material = (Material) Resources.Load ("Materials/PixelPerfectOutline", typeof(Material));
        outline.OutlineColor = new Color(1, 1, 1, 0);

        //Settings
        selected = false;
        action = false;
	}
	
	//Update Event
	protected void Update () {
		Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PlayerBehavior player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();

        outline.OutlineColor = new Color(1, 1, 1, 0);
        if (player.can_move){
            if (selected){
                outline.OutlineColor = new Color(1, 1, 1, 0.8f);
            }
            else {
                if (collider2d.OverlapPoint(new Vector2(v3.x, v3.y))) {
                    outline.OutlineColor = new Color(1, 1, 1, 0.8f);
                }
            }
        }
        else {
            if (player.inventory_act){
                if (collider2d.OverlapPoint(new Vector2(v3.x, v3.y))) {
                    outline.OutlineColor = new Color(1, 1, 1, 0.8f);
                }
            }
        }
	}

    //Get Position
    public override Vector2 getPosition() {
        return position;
    }

}
