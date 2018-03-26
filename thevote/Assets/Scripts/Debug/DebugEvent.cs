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

        /*
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
        */

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

        
        file.addSkip(0, 20);
        file.addKey(0);
        file.addText("Omg you weren't kidding", Vector2.zero, "player");
        file.addText("I told you so", Vector2.zero, "Will");
        file.addText("Your favorite diner opened up in Naperville", Vector2.zero, "Will");
        file.addText("I can't beleive it's so empty", Vector2.zero, "player");
        file.addChoice("Will", "What are you going to order?", "Waffle Fries", 1, "Cheesy Waffle Fries", 1);
        file.addText("I thought you said we were getting waffles", Vector2.zero, "Will");
        file.addText("Waffle fries are a type of waffle!", Vector2.zero, "player");
        file.addText("...", Vector2.zero, "Will");
        file.addText("What?", Vector2.zero, "player");
        file.addText("I missed you so much", Vector2.zero, "Will");
        file.addChoice("Will", "What made you come back from LA?", "Waffle Fries", 1, "Cheesy Waffle Fries", 1);
        file.addText("...", Vector2.zero, "Will");
        file.addText("You're such a dumpster fire", Vector2.zero, "Will");
        file.addText("Just grab us some food so we don't starve", Vector2.zero, "Will");
        file.addText("Anything you want in particular?", Vector2.zero, "player");
        file.addText("Some plastic spoons to gouge my eyes out", Vector2.zero, "Will");
        file.addText("Sure thing <3", Vector2.zero, "player");
        file.addSkip(0, 30);

        file.addSkip(1, 4);
        file.addText("I'm starvinggggggggg", Vector2.zero, "Will");
        file.addText("hurry up pleeeeaseeee", Vector2.zero, "Will");
        file.addSkip(0, 26);

        file.addText("So guess what I have for you", Vector2.zero, "Will");
        file.addText("Wait! Last time you gave me a surprise I had a panic attack", Vector2.zero, "player");
        file.addChoice("Will", "Since when did my gift giving hurt anyone?", "Last Febuary", 1, "May two years ago", 1, "The Christmas Apocalypse", 1);
        file.addText("Old people get heart attacks all the time", Vector2.zero, "Will");
        file.addText("He died of shock", Vector2.zero, "player");
        file.addText("WITH A SMILE ON HIS FACE", Vector2.zero, "Will");
        file.addText("You can't argue with results", Vector2.zero, "Will");

        file.addText("But no, this is something entirely different!", Vector2.zero, "Will");
        file.addText("It's a job!", Vector2.zero, "Will");
        file.addText("...", Vector2.zero, "player");
        file.addText("How come you're not that excited?", Vector2.zero, "Will");
        file.addText("In today's economy you just practically won the lottery!", Vector2.zero, "Will");
        file.addText("~yayyy~", Vector2.zero, "player");
        file.addChoice("Will", "You're so lame", "Do I at least get paid?", 1, "What do I have to do?", 1);
        file.addText("Glad you asked!", Vector2.zero, "Will");
        file.addText("We're having an upcoming election for mayor!", Vector2.zero, "Will");
        file.addText("Isn't the mayor alive?", Vector2.zero, "player");
        file.addText("Nope, not since yesterday at least...", Vector2.zero, "Will");
        file.addText("Omg!", Vector2.zero, "player");
        file.addText("He had one of those heart attacks", Vector2.zero, "Will");
        file.addText("really common among old people", Vector2.zero, "Will");
        file.addText("What about his advisors? Or his board?", Vector2.zero, "player");
        file.addText("None of them are running.", Vector2.zero, "Will");
        file.addText("In fact all of them declined to enter this year's campaign", Vector2.zero, "Will");
        file.addText("Which brings us to our main ", Vector2.zero, "Will");

        file.addEnd();

        /*
        file.addText("Waffles?", Vector2.zero, "Ellie");
        file.addText("Yes", Vector2.zero, "player");
        file.addKey(1);
        file.addEnd();
        */

        em.saveFile("WillDinerA", file);
    }

}
