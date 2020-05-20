using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {
	public enum DrumParts {
		RighCymbal = 0
		,RightDrum = 1
		,CentralDrum = 2
		,LeftCymbal = 3
		,LeftDrum = 4
	}

	//Track information
	private static string currentTrack = "";
	private static string currentTrackName = "";
	private static string currentTrackDificulty = "";

	//Score earned after finishing a track
	private static int totalNotes = 0;
	private static int perfectNotes = 0;
	private static int goodNotes = 0;
	private static int missedNotes = 0;

	//Get and set of the track file
	public static string getCurrentTrack(){
		return currentTrack;
	}

	public static void setCurrentTrack(string newTrack){
		currentTrack = newTrack;
	}

	//Get and set of the track name
	public static string getCurrentTrackName(){
		return currentTrackName;
	}

	public static void setCurrentTrackName(string newTrack){
		currentTrackName = newTrack;
	}
		
	//Get and set of the track dificulty
	public static string getCurrentTrackDificulty(){
		return currentTrackDificulty;
	}

	public static void setCurrentTrackDificulty(string newDificulty){
		currentTrackDificulty = newDificulty;
	}

	//Get and set of the number of notes of the track
	public static int getTotalNotes(){
		return totalNotes;
	}

	public static void setTotalNotes(int notes){
		totalNotes = notes;
	}

	//Get and set of the number of perfect notes hitted on the track
	public static int getPerfectNotes(){
		return perfectNotes;
	}

	public static void setPerfectNotes(int notes){
		perfectNotes = notes;
	}

	//Get and set of the number of good notes hitted on the track
	public static int getGoodNotes(){
		return goodNotes;
	}

	public static void setGoodNotes(int notes){
		goodNotes = notes;
	}

	//Get and set of the number of notes missed on the track
	public static int getMissedNotes(){
		return missedNotes;
	}

	public static void setMissedNotes(int notes){
		missedNotes = notes;
	}
}
