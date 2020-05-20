using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using UnityEngine;

public class MidiDrumScript {
	//Initializes variables needed to read the arduino Data
	const int serialPort = 9600;
	const string portName = "COM3";
	SerialPort arduinoPort;

	//Initializes the path to the sound files
	string[] soundFiles = {
					"Sounds/DrumSounds/0"
					,"Sounds/DrumSounds/1"
					,"Sounds/DrumSounds/2"
					,"Sounds/DrumSounds/3"
					,"Sounds/DrumSounds/4"
						};
	List<AudioSource> audioSources = new List<AudioSource>();
	public string midiData = "";
	private bool finished = false;
	private const int numberOfPiezos = 5;
	public List<string> midiArray = new List<string>();
	public List<string> midiAux = new List<string>();
	public List<bool> midiFinished = new List<bool>();

	public MidiDrumScript(GameObject gameObject){
		this.midiArray = new List<string>();
		this.midiFinished = new List<bool>();

		this.arduinoPort = new SerialPort (portName, serialPort);
		this.arduinoPort.Open ();
		this.arduinoPort.ReadTimeout = 1;

		for (int i = 0; i < soundFiles.Length; i++) {
			AudioSource audio = gameObject.AddComponent<AudioSource>();
			audio.clip = Resources.Load (soundFiles[i]) as AudioClip;
			audioSources.Add (audio);
		}

		for (int i = 0; i < numberOfPiezos; i++) {
			this.midiArray.Add("");
			this.midiAux.Add("");
			this.midiFinished.Add(false);
		}
	}

	public MidiDrumScript(){
		this.midiArray = new List<string>();
		this.midiFinished = new List<bool>();

		this.arduinoPort = new SerialPort (portName, serialPort);
		this.arduinoPort.Open ();
		this.arduinoPort.ReadTimeout = 1;

		for (int i = 0; i < numberOfPiezos; i++) {
			this.midiArray.Add("");
			this.midiAux.Add("");
			this.midiFinished.Add(false);
		}
	}
		
	/*
	 * Reads and returns the data read from the arduino in format 'Piezo-Volume'
	*/
	public string readPort(){
		try{
			string data = this.arduinoPort.ReadExisting();

			foreach(char c in data){
				if(c != '|'){
					this.midiData += c;
				}
				else{
					this.finished = true;
				}
			}

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
			Debug.Log (ex);
			return "";
		}
	}

	public string readMultiplePorts(int sensor){
		try{
			//If we have an input saved for thah sensor, we pick that input and return it
			if(this.midiArray[sensor] != "" && this.midiFinished[sensor]){
				//We empty the entry so next time we read it from the arduino
				var aux = this.midiArray[sensor];
				this.midiArray[sensor] = "";
				this.midiFinished[sensor] = false;
				return aux;
			}
			//If there is no entry, we read it from the arduino
			else{
				//We read the whole entry untill we find the '|' character
				string data = this.arduinoPort.ReadExisting();

				foreach(char c in data){
					if(c != '|'){
						this.midiAux[sensor] += c;
					}
					//If we reach the end of the reading we assign the correct value
					else{
						//We parse the note of the correct reading
						var aux = this.midiAux[sensor].Split ('-');
						int note = int.Parse(aux [0]);

						//We assign the value of the reading to the correct position of the array and we restart the aux array
						this.midiArray[note] = this.midiAux[sensor];
						this.midiAux[sensor] = "";
						this.midiFinished[note] = true;
					}

				}
				return "";
			}
		}
		catch(System.Exception ex){
			Debug.Log("ERROR: " + ex);
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

	/*
	 * Method to close the port so we can open it again later
	*/
	public void closePort(){
		this.arduinoPort.Close ();
	}
}
