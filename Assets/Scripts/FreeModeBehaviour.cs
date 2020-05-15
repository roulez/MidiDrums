using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FreeModeBehaviour : MonoBehaviour {
	private List<GameObject> drumParts;
	MidiDrumScript midiCrontoller;
	int volume;
	int sensor;
	string arduinoInput;
	private Button backButton;
	private float timeOnScreen = 0.5f;

	// Use this for initialization
	void Start () {
		this.drumParts = new List<GameObject> ();

		//Images that represent the differnt parts of the Drum
		this.drumParts.Add(GameObject.Find("RightCymbal"));
		this.drumParts.Add(GameObject.Find("RightDrum"));
		this.drumParts.Add(GameObject.Find("CentralDrum"));
		this.drumParts.Add(GameObject.Find("LeftCymbal"));
		this.drumParts.Add(GameObject.Find("LeftDrum"));

		foreach (GameObject go in this.drumParts) {
			go.gameObject.SetActive(false);
		}

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
				StartCoroutine(ManageImagery(this.sensor));
			}
		}
		catch(System.Exception ex){
			Debug.Log (ex);
		}
	}

	/*
	 * Shows the image of the drum part that has been hited
	*/
	IEnumerator ManageImagery(int sensor){
		this.drumParts[sensor].gameObject.SetActive(true);
		yield return new WaitForSeconds(this.timeOnScreen);
		this.drumParts[sensor].gameObject.SetActive(false);
	}
}
