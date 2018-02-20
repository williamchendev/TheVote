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
        file.addSkip(3, 4);
        file.addText("1", Vector2.zero, "playerfixed");
        file.addText("2", Vector2.zero, "playerfixed");
        file.addKey(3);
        file.addText("3", Vector2.zero, "playerfixed");
        file.addText("4", Vector2.zero, "playerfixed");
        file.addText("5", Vector2.zero, "playerfixed");
        file.addText("6", Vector2.zero, "playerfixed");
        file.addText("7", Vector2.zero, "playerfixed");
        file.addEnd();
        em.saveFile("debug_skip", file);
    }

}
