using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {
	
	void OnGUI() {
		if (GUI.Button (new Rect(50,50,300,50), "Load dancing Priya")) {    #name of the tab   
			Application.LoadLevel("dancingPriya");                            #name of the scene
		}
		
		if (GUI.Button (new Rect(50,100,300,50), "Load Guitar Guy")) {
			Application.LoadLevel("guitarGuy");
		}


		
		GUI.Box (new Rect(50,200,300,30), "Current scene: " + Application.loadedLevelName);
	}
}
