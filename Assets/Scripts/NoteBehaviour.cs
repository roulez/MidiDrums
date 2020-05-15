using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
	public Utilities.DrumParts drumNote;
	private bool inTime = false;
	private GameObject parentObject;
	private GeneralBeatsBehaviour parentScript;

    // Start is called before the first frame update
    void Start()
    {
		this.parentObject = transform.parent.gameObject;
		this.parentScript = this.parentObject.GetComponent<GeneralBeatsBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
		try{
			//If the note is on the collider to press it, we read the arduino to see if it is pressed
			if (this.inTime) {
				this.parentScript.mainScript.readArduino((int) this.drumNote, gameObject);
			}
		}
		catch(System.Exception ex){
			
		}
    }

	private void OnTriggerEnter2D(Collider2D box){
		if (box.tag == "Input") {
			this.inTime = true;
		}
	}

	private void OnTriggerExit2D(Collider2D box){
		if (box.tag == "Input") {
			this.inTime = false;
		}
	}

	public bool getInTime(){
		return this.inTime;
	}
}
