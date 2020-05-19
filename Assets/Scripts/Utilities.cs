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

	public static string currentTrack = "";

	public static string getCurrentTrack(){
		return currentTrack;
	}

	public static void setCurrentTrack(string newTrack){
		currentTrack = newTrack;
	}
}
