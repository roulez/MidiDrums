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
					"Sounds/DrumSounds/Crash"
					,"Sounds/DrumSounds/Tom-2"
					,"Sounds/DrumSounds/Tom-1"
					,"Sounds/DrumSounds/Hi-Hat"
					,"Sounds/DrumSounds/Snare"
						};
	//Array of the different audio sources of the instrument					
	List<AudioSource> audioSources = new List<AudioSource>();
	//Number of piezos of the instrument
	private const int numberOfPiezos = 5;
	//Variables used to read the data from the arduino
	public string midiData = "";
	private bool finished = false;
	public List<string> midiArray = new List<string>();
	public List<string> midiAux = new List<string>();
	public List<bool> midiFinished = new List<bool>();

	//Volume control for the notes
	public int minVolume = 0;
	public int maxVolume = 127;
	//Minimun volumen of the sounds we play
	public float thressholdVolume = 0.15f;

	/*
	 * Constructor which adds the audio sources of the instruments to the game object passed
	*/
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

	/*
	 * Constructor of the arduino reader
	*/
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
	 * Reads and returns the data read from the arduino in format 'Piezo-Volume'. Used in the free mode.
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

	/*
	 * We read the data from the arduino but chceck the data corresponding to an expecified sensor.
	 * With this we make sure that if a sensor reads the data corresponding to another sensor the data is not lost. Used in the guided mode.
	*/
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
	 * Plays the sound of a sensor, independient of the volume. Used in the guided mode.
	*/
	public void playSound(int sensor){
		//We check if the sensor is correct
		if (sensor >= 0 && sensor <= this.audioSources.Count) {
			//We play the corresponding sound effect
			audioSources [sensor].Play ();
		}
	}

	/*
	 * Plays the sound of a sensor with an specified volume. Used in the free mode.
	*/
	public void playSound(int sensor, int volume){
		//We get the normalized value of the sound effect
		float newVolume = this.normalizeVolume(volume);

		//We check if the sensor is correct
		if (sensor >= 0 && sensor <= this.audioSources.Count) {
			//We play the corresponding sound effect with the corresponding volume
			audioSources [sensor].volume = newVolume;
			audioSources [sensor].Play ();
		}
	}

	/*
	 * Method to close the port so we can open it again later
	*/
	public void closePort(){
		this.arduinoPort.Close ();
	}

	/*
	 * We transform the volume value in a value between 0 and 1 so we can change the audio source volume
	*/
	public float normalizeVolume(int volume){
		float newVolume = 0.0f;

		newVolume = ((float)(volume - this.minVolume) / (float)(this.maxVolume - this.minVolume));

		//We consider the values below the thresshold as the thresshold so some sound is played since the arduino registered the hit
		if(newVolume < this.thressholdVolume){
			newVolume = this.thressholdVolume;
		}

		return newVolume;
	}
}
