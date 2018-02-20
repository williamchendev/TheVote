using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEvent : MonoBehaviour {

    [SerializeField] private bool save;
    private EventManager em;

	void Awake () {
		em = GetComponent<EventManager>();

        if (save) {
            debug();
            save = false;
        }
	}

    private void debug() {
        EventFile file = new EventFile();
        file.addText("I can't use that, that's dumb!", Vector2.zero, "playerfixed");
        file.addEnd();
        em.saveFile("cantuseitem", file);
    }

}
