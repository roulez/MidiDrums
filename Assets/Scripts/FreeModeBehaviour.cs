using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FreeModeBehaviour : MonoBehaviour {
	private InputBehaviour[] drumParts;
	MidiDrumScript midiCrontoller;
	int volume;
	int sensor;
	string arduinoInput;
	private Button backButton;
	private float timeOnScreen = 0.5f;
	public

	// Use this for initialization
	void Start () {
		//Images that represent the differnt parts of the Drum
		this.drumParts = FindObjectsOfType<InputBehaviour> ();

		//Button to exit the free mode
		backButton = GameObject.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener (delegate {
			SceneManager.LoadScene ("mainMenu");
		});

		this.midiCrontoller = new MidiDrumScript (this.gameObject);
		this.volume = 0;
		this.sensor = 0;
		this.arduinoInput = "";
	}
	
	// Update is called once per frame
	void Update () {
		try{
			this.arduinoInput = this.midiCrontoller.readPort();

			if (this.arduinoInput != "") {
				var aux = this.arduinoInput.Split ('-');

				this.sensor = int.Parse(aux [0]);
				this.volume = int.Parse(aux [1]);
				this.midiCrontoller.playSound (this.sensor,this.volume);

				for(int i = 0; i < this.drumParts.Length; i++){
					if((int)this.drumParts[i].drumNote == this.sensor){
						this.drumParts[i].changeImage();
					}
				}
			}
		}
		catch(System.Exception ex){
			Debug.Log (ex);
		}
	}
}
