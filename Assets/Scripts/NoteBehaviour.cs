using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
	public Utilities.DrumParts drumNote;
	private bool inTime = false;
	private bool offBeat = false;
	private GameObject parentObject;
	private GeneralBeatsBehaviour parentScript;
	private bool beenHitted;

    // Start is called before the first frame update
    void Start()
    {
		this.beenHitted = false;
    }

    // Update is called once per frame
    void Update()
    {
		try{
			//If the note is on the collider to press it, we read the arduino to see if it is pressed
			if ((this.offBeat || this.inTime) && !this.beenHitted) {
				this.beenHitted = GuidedModeBehaviour.instance.readArduino((int) this.drumNote);

				//If the note is hitted we add a note to the score and we deactivate the note
				if(this.beenHitted){
					//If its been hitted perfectly, we count as a hit on beat
					if(this.inTime){
						GuidedModeBehaviour.instance.noteOnBeat();
					}
					//If not, we count it as a of beat note
					else{
						GuidedModeBehaviour.instance.noteOffBeat();
					}
					gameObject.SetActive (false);
				}
			}
		}
		catch(System.Exception ex){
			Debug.Log (ex);
		}
    }

	/*
	 * Method called when a note enters the collider of the button to press
	*/
	private void OnTriggerEnter2D(Collider2D box){
		//Depending the flag of the collider, we activate a flag
		if (box.tag == "Input") {
			this.inTime = true;
		}
		else if (box.tag == "OffBeat") {
			this.offBeat = true;
		}
	}

	/*
	 * Method called when a note leaves the collider of the button to press
	*/
	private void OnTriggerExit2D(Collider2D box){
		//If the note leaves the collider that we consider the perfect beat, we deactivate the flag
		if (box.tag == "Input") {
			this.inTime = false;
		}
		//If the note leaves the collider completely, if the note hasn't been hitted we count that as a missed note
		else if (box.tag == "OffBeat") {
			this.offBeat = false;

			if (!this.beenHitted) {
				GuidedModeBehaviour.instance.noteMissed ();
			}
		}
	}

	public bool getInTime(){
		return this.inTime;
	}
}
