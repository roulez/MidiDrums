using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
	public Utilities.DrumParts drumNote;
	private bool inTime = false;
	private GameObject parentObject;
	private GeneralBeatsBehaviour parentScript;
	/*private MidiDrumScript midiCrontoller;
	private int volume;
	private int sensor;
	public AudioSource noteAudio;*/

    // Start is called before the first frame update
    void Start()
    {
		this.parentObject = transform.parent.gameObject;
		this.parentScript = this.parentObject.GetComponent<GeneralBeatsBehaviour>();
		/*this.midiCrontoller = new MidiDrumScript ();
		this.volume = 0;
		this.sensor = 0;*/
    }

    // Update is called once per frame
    void Update()
    {
		try{
			//If the note is on the collider to press it, we read the arduino to see if it is pressed
			if (this.inTime) {
				Debug.Log("Llamo al script pade: " + (int) this.drumNote);
				this.parentScript.mainScript.readArduino((int) this.drumNote, gameObject);
				/*var arduinoInput = this.midiCrontoller.readPort();

				//If is not empty a note has been pressed
				if (arduinoInput != "") {
					Debug.Log("Reading: " + arduinoInput);
					var aux = arduinoInput.Split ('-');

					this.sensor = int.Parse(aux [0]);
					this.volume = int.Parse(aux [1]);

					Debug.Log("Sensor: " + this.sensor + " || Note: " + (int)this.drumNote);
					//If the note pressed is the correct one, we play the note sound and we deactivate the note on screen
					if (this.sensor == (int)this.drumNote) {
						Debug.Log("Entra: " + this.noteAudio);
						this.playSound (this.volume);
						gameObject.SetActive (false);
					}
				}*/
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
