using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class MidiDrumScript : MonoBehaviour {
	//Initializes variables needed to read the arduino Data
	const int serialPort = 9600;
	const string portName = "COM3";
	SerialPort arduinoPort = new SerialPort(portName,serialPort);

	//Initializes the path to the sound files
	string[] soundFiles = {
					"Sounds/0"
					,"Sounds/1"
					,"Sounds/2"
					,"Sounds/3"
					,"Sounds/4"
						};
	List<AudioSource> test = new List<AudioSource>();

	// Use this for initialization
	void Start () {
		arduinoPort.Open ();
		arduinoPort.ReadTimeout = 1;

		for (int i = 0; i < soundFiles.Length; i++) {
			AudioSource audio = gameObject.AddComponent<AudioSource>();
			audio.clip = Resources.Load (soundFiles[i]) as AudioClip;
			test.Add (audio);
		}
	}

	/*
	 * Reads and returns the data read from the arduino in format 'Piezo-Volume'
	*/
	string readPort(){
		string data = arduinoPort.ReadLine();

		if (data != null && data != "" && data.Contains ("-")) {
			var arduinoData = data.Split ('-');
			if (arduinoData [0] != "" && arduinoData [1] != "") {
				return data;
			} else {
				return "ERROR";
			}
		} else {
			return "ERROR";
		}
	}

	/*
	 * Plays the sound of a sensor with an specified volume
	*/
	void playSound(int sensor, int volumen){
		Debug.Log ("Playing: "+ test[sensor]);
		test [sensor].Play ();
	}
	
	// Update is called once per frame
	/*void Update () {
		try{
			string data = arduinoPort.ReadLine();

			if(data != null && data != "" && data.Contains("-")){
				var arduinoData = data.Split('-');
				if(arduinoData[0] != "" && arduinoData[1] != ""){
					Debug.Log("Piezo: " + arduinoData[0] + " || Volumen: " + arduinoData[1]);
					this.playSound(int.Parse(arduinoData[0]),15);
				}
			}
		}
		catch(System.Exception ex){
			Debug.Log (ex);
		}

	}*/
}
