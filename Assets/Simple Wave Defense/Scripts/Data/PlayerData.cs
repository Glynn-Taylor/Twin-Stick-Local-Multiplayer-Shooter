using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {

	public static bool[] Players = {true,false,false,false};

	public static bool PlayerExists(string name){
		return Players [int.Parse (name.Substring (1))-1];
	}

	public void AddPlayer(int i){
		Players [i-1] = true;
	}
	public void AddPlayer(string i){
		Players [int.Parse(i.Substring(1))-1] = true;
	}
	public void RemovePlayer(int i){
		Players [i-1] = true;
	}
	public void RemovePlayer(string i){
		Players [int.Parse(i.Substring(1))-1] = true;
	}

}
