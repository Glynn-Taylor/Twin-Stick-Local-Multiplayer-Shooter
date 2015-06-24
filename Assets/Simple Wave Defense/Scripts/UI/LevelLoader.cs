using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	public void LoadLevel(int i){
		Application.LoadLevel (i);
	}
	public void LoadLevel(string i){
		Application.LoadLevel (i);
	}
}
