using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class MidiDrumScript : MonoBehaviour {
	//Initializes variables needed to read the arduino Data
	const int serialPort = 9600;
	const string portName = "COM3";
	SerialPort arduinoPort;

	//Initializes the path to the sound files
	string[] soundFiles = {
					"Sounds/0"
					,"Sounds/1"
					,"Sounds/2"
					,"Sounds/3"
					,"Sounds/4"
						};
	List<AudioSource> audioSources = new List<AudioSource>();

	public MidiDrumScript(GameObject gameObject){
		this.arduinoPort = new SerialPort (portName, serialPort);
		this.arduinoPort.Open ();
		this.arduinoPort.ReadTimeout = 1;

		for (int i = 0; i < soundFiles.Length; i++) {
			AudioSource audio = gameObject.AddComponent<AudioSource>();
			audio.clip = Resources.Load (soundFiles[i]) as AudioClip;
			audioSources.Add (audio);
		}
	}

	// Use this for initialization
	void Start () {
		this.arduinoPort = new SerialPort (portName, serialPort);
		this.arduinoPort.Open ();
		this.arduinoPort.ReadTimeout = 1;

		for (int i = 0; i < soundFiles.Length; i++) {
			AudioSource audio = gameObject.AddComponent<AudioSource>();
			audio.clip = Resources.Load (soundFiles[i]) as AudioClip;
			audioSources.Add (audio);
		}
	}

	/*
	 * Reads and returns the data read from the arduino in format 'Piezo-Volume'
	*/
	public string readPort(){
		try{
			string data = this.arduinoPort.ReadLine();

			if (data != null && data != "" && data.Contains ("-")) {
				var arduinoData = data.Split ('-');
				if (arduinoData [0] != "" && arduinoData [1] != "") {
					return data;
				} else {
					return "";
				}
			} else {
				return "";
			}
		}
		catch(System.Exception ex){
			Debug.Log (ex);
			return "";
		}
	}

	/*
	 * Plays the sound of a sensor with an specified volume
	*/
	public void playSound(int sensor, int volume){
		if (sensor >= 0 && sensor <= this.audioSources.Count) {
			audioSources [sensor].Play ();
		}
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
