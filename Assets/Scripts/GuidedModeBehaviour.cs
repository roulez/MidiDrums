using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedModeBehaviour : MonoBehaviour
{
	private MidiDrumScript midiCrontoller;
    // Start is called before the first frame update
    void Start()
    {
		this.midiCrontoller = new MidiDrumScript (this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void readArduino(int sensor, GameObject obj){
		try{
			var arduinoInput = this.midiCrontoller.readMultiplePorts(sensor);

			//If is not empty a note has been pressed
			if (arduinoInput != "") {
				var aux = arduinoInput.Split ('-');

				int note = int.Parse(aux [0]);
				int volume = int.Parse(aux [1]);

				//If the note pressed is the correct one, we play the note sound and we deactivate the note on screen
				if (sensor == note) {
					this.midiCrontoller.playSound (sensor,volume);
					obj.SetActive (false);
				}
			}
		}
		catch(System.Exception ex){

		}
	}
}
