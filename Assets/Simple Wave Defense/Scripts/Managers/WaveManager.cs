using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {
	private const float MIN_ENEMIES_INCREMENT = 0.4f;
	[Range(1,3)]
	public float Difficulty=1;
	public float TimeBetweenWaves=20;
	//public GameObject[] Enemies;      
	public Wave[] Waves;

	private int _enemiesAlive = 0;
	private float _waveTimer=0;
	private int _WaveNumber=0;
	private Transform[] _SpawnPoints;	
	private float _minEnemies=0;

	[System.Serializable]
	public class Wave {
		public Entry[] Entries;
        [System.Serializable]
        public class Entry {
			public GameObject Enemy;
			public int Count;
        }    
    }


	void Start() {
		_SpawnPoints= new Transform[transform.childCount];
		for (int i=0; i<transform.childCount; i++) {
			_SpawnPoints[i]=transform.GetChild(i);
		}
		if(_SpawnPoints.Length==0)
			Debug.LogError("<WaveManager[Start]>: No spawn points (children)");

	}
	
	void Update() {

        // Add the time since Update was last called to the timer.
		_waveTimer += Time.deltaTime;
        
        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive attack.
		if (_waveTimer >= TimeBetweenWaves || _enemiesAlive<=(int)_minEnemies) {
			Wave currentWave = Waves[_WaveNumber>=Waves.Length?Waves.Length-1:_WaveNumber];
			// Spawn one enemy from each of the entries in this wave.
			foreach (Wave.Entry entry in currentWave.Entries) {
				Spawn(entry);
			}
			_waveTimer=0;
			_WaveNumber++;
			_minEnemies+=MIN_ENEMIES_INCREMENT;
		}
	}


	void Spawn(Wave.Entry entry) {
		
		for (int i=0; i<entry.Count; i++) {
			Vector3 randomPosition = _SpawnPoints [Random.Range (0, _SpawnPoints.Length)].position;
			randomPosition.y = 0;
			Vector3 screenPos = Camera.main.WorldToScreenPoint (randomPosition);

			GameObject enemy = Instantiate (entry.Enemy, randomPosition, Quaternion.identity) as GameObject;
			enemy.GetComponent<CharacterHealth> ().MaxHealth = (int)(enemy.GetComponent<CharacterHealth> ().MaxHealth * Difficulty);
			_enemiesAlive++;
		}
	}
	public void EnemyDeath(){
		_enemiesAlive--;
	}
}