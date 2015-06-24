using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {
	private static bool Instantiated; //Quick fix for audio
	// Use this for initialization
	void Start () {
		if (Instantiated) {
			Destroy(gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
			Instantiated=true;
			Destroy (this);

		}
	}
}
