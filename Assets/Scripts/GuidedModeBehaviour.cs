using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedModeBehaviour : MonoBehaviour
{
	private MidiDrumScript midiCrontoller;
	//Score of the player whith the notes pressed
	private int missedNotes;
	private int onBeatNotes;
	private int offBeatNotes;

	//We create an instance of the class so the notes can acces the code
	public static GuidedModeBehaviour instance;
    // Start is called before the first frame update
    void Start()
    {
		this.midiCrontoller = new MidiDrumScript (this.gameObject);
		this.missedNotes = 0;
		this.onBeatNotes = 0;
		this.offBeatNotes = 0;

		//We set the value of the instance
		instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	/*
	 * We read the data from the arduino and we return to the note if the note has been pressed
	*/
	public bool readArduino(int sensor){
		try{
			var arduinoInput = this.midiCrontoller.readMultiplePorts(sensor);

			//If is not empty a note has been pressed
			if (arduinoInput != "") {
				var aux = arduinoInput.Split ('-');

				int note = int.Parse(aux [0]);
				int volume = int.Parse(aux [1]);

				//If the note pressed is the correct one, we play the note sound and we return that is has been pressed
				if (sensor == note) {
					this.midiCrontoller.playSound (sensor,volume);
					return true;
				}
			}
			return false;
		}
		catch(System.Exception ex){
			Debug.Log (ex);
			return false;
		}
	}

	/*
	 * We call this method when a note is been hit with perfect timing
	*/
	public void noteOnBeat(){
		this.onBeatNotes++;
		Debug.Log ("Note On Beat: " + this.onBeatNotes);
	}

	/*
	 * We call this method when a note is been hit with a slightly off timing
	*/
	public void noteOffBeat(){
		this.offBeatNotes++;
		Debug.Log ("Note Off Beat: " + this.offBeatNotes);
	}

	/*
	 * We call this method when a note is not played in the correct time
	*/
	public void noteMissed(){
		this.missedNotes++;
		Debug.Log ("Note Missed: " + this.missedNotes);
	}
}
