using UnityEngine;

public class GameOverManager : MonoBehaviour {


	public float RestartDelay = 5f;			
	public GameObject Fader;
	[SerializeField]
	private int _remainingPlayers=0;

	void Awake() {
		_remainingPlayers = GameObject.FindGameObjectsWithTag ("Player").Length;
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		for (int i=0; i<players.Length; i++) {
			if(!PlayerData.PlayerExists(players[i].GetComponent<PlayerController>().Player)){
				Destroy(players[i]);
				_remainingPlayers--;
			}
		}


	}

	public void PlayerDeath(){
		_remainingPlayers--;
		if (_remainingPlayers <= 0) {
			Fader.SetActive(true);
			Invoke("LoadLevel",RestartDelay);
		}
	}

	void LoadLevel(){
		Application.LoadLevel (0);
	}
}