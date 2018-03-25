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
        /*
        file.addText("*whimpers*", Vector2.zero, "will");
        file.addText("I can't believe this is happening", Vector2.zero, "player");
        file.addEnd();
        */
        file.addSkip(1, 22);
        file.addKey(0);
        file.addText("..auuuuggggggghhh...", Vector2.zero, "will");
        file.addText("I found him like this when I came in", Vector2.zero, "ann");
        file.addText("Are you serious, come on", Vector2.zero, "player");
        file.addText("You barely canvassed", Vector2.zero, "player");
        file.addText("That last house was so mean to me", Vector2.zero, "will");
        file.addChoice("will", "ughhh..", "You only did 5 houses", 1, "You argued for an hour with a 12 year old", 1);
        file.addText("We need you out there knocking doors", Vector2.zero, "ann");
        file.addText("We're down 40 points in the polls", Vector2.zero, "ann");
        file.addChoice("will", "Now's not the time for a break", "Kick him", 1, "Drag him outside", 1, "Verbal Absue", 1);
        file.addText("Hannah...", Vector2.zero, "ann");
        file.addText("What?..", Vector2.zero, "player");
        file.addText("Okay what should we do now?", Vector2.zero, "player");
        file.addText("Well since we're stuck here because of someone...", Vector2.zero, "ann");
        file.addText("*whimpers*", Vector2.zero, "will");
        file.addText("We'll have to find a way out of the house...", Vector2.zero, "ann");
        file.addChoice("will", "*crying*", "ditch will", 1, "throw a brick at the press", 1, "More Verbal Absue", 1);
        file.addText("I know! Grab me a banana", Vector2.zero, "ann");
        file.addText("*inaudiable moans*", Vector2.zero, "will");
        file.addKey(1);
        file.addSkip(0, 4);
        file.addSkip(2, 4);
        file.addText("There should be a banana somewhere around here...", Vector2.zero, "ann");
        file.addText("Please Hannah, you're my only hope <3", Vector2.zero, "ann");
        file.addSkip(0, 11);
        file.addText("We're kinda trapped", Vector2.zero, "ann");
        file.addText("this is a nightmaree...", Vector2.zero, "will");
        file.addText("Alright, time to actually do something", Vector2.zero, "ann");
        file.addChoice("will", "whyyy me...", "what's the plan?", 1, "what's the plan?", 1, "what's the plan?", 1);
        file.addText("Help me toss Will into the van", Vector2.zero, "ann");
        file.addText("You'll get me to canvas over my dead body", Vector2.zero, "will");
        file.addText("good", Vector2.zero, "player");
        file.addText("good", Vector2.zero, "ann");
        file.addText("We have about 3 hours to get 500 signatures", Vector2.zero, "ann");
        file.addText("I think we can do it <3", Vector2.zero, "player");
        file.addEnd();

        /*
        file.addKey(4);
        file.addText("Thanks so much for the Banana Hannah <3", Vector2.zero, "ann");
        file.addText("So how is that going to get us out of here?", Vector2.zero, "player");
        file.addText("Oh it won't, I'm just hungry", Vector2.zero, "ann");
        file.addText("Ann", Vector2.zero, "player");
        file.addText("I can't think of a way out of this if I'm hungry!", Vector2.zero, "ann");
        file.addText("..this is a really good banana...", Vector2.zero, "ann");
        file.addText("Ann!", Vector2.zero, "player");
        file.addText("okay Okay I Got it", Vector2.zero, "ann");
        file.addEnd();
        */
        em.saveFile("TestText", file);
    }

}
