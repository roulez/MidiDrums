using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class MidiDrumScript {
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
	public string midiData = "";
	private bool finished = false;
	private const int numberOfPiezos = 5;
	public List<string> midiArray = new List<string>(numberOfPiezos);
	public List<bool> midiFinished = new List<bool>(numberOfPiezos);

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

	public MidiDrumScript(){
		this.arduinoPort = new SerialPort (portName, serialPort);
		this.arduinoPort.Open ();
		this.arduinoPort.ReadTimeout = 1;

		for (int i = 0; i < numberOfPiezos; i++) {
			this.midiArray[i] = "";
			this.midiFinished[i] = false;
		}
	}
		
	/*
	 * Reads and returns the data read from the arduino in format 'Piezo-Volume'
	*/
	public string readPort(){
		try{
			string data = this.arduinoPort.ReadExisting();
			string test = "";
			//Debug.Log("Empieza");
			foreach(char c in data){
				if(c != '|'){
					this.midiData += c;
				}
				else{
					this.finished = true;
				}
			}
			//Debug.Log("Termina");

			if(this.finished){
				this.finished = false;
				string aux = this.midiData;
				this.midiData = "";

				return aux;
			}
			else {
				return "";
			}
		}
		catch(System.Exception ex){
			return "";
		}
	}

	public string readMultiplePorts(int sensor){
		try{
			if(this.midiArray[sensor] != "" && this.midiFinished[sensor]){
				var aux = this.midiArray[sensor];
				this.midiArray[sensor] = "";
				this.midiFinished[sensor] = false;
				return aux;
			}
			else{
				string data = this.arduinoPort.ReadExisting();
				string test = "";

				foreach(char c in data){
					if(c != '|'){
						this.midiArray[sensor] += c;
					}
					else{
						this.midiFinished[sensor] = true;
					}
				}
				return "";
			}
		}
		catch(System.Exception ex){
			return "";
		}
	}

	/*
	 * Plays the sound of a sensor with an specified volume
	*/
	public void playSound(int sensor, int volume){
		Debug.Log("Debug: " + audioSources [sensor]);
		if (sensor >= 0 && sensor <= this.audioSources.Count) {
			audioSources [sensor].Play ();
		}
	}
}
