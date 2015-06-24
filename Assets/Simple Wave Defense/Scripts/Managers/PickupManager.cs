using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour {

	public float SpawnEvery = 5f;
	public GameObject[] Pickups;
	public float DistanceFromGround=0.75f;

	float _spawnTimer=0;
	private Transform[] _SpawnPoints;	

	void Start() {
		_SpawnPoints= new Transform[transform.childCount];
		for (int i=0; i<transform.childCount; i++) {
			_SpawnPoints[i]=transform.GetChild(i);
		}
		if(_SpawnPoints.Length==0)
			Debug.LogError("<PickupManager[Start]>: No spawn points (children)");
		
	}

	void Update() {
		
		// Add the time since Update was last called to the timer.
		_spawnTimer += Time.deltaTime;
		
		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive attack.
		if (_spawnTimer >= SpawnEvery) {
			Vector3 randomPosition = _SpawnPoints [Random.Range (0, _SpawnPoints.Length)].position;
			randomPosition.y = DistanceFromGround;
			GameObject pickup = Instantiate (Pickups[Random.Range(0,Pickups.Length)], randomPosition, Quaternion.identity) as GameObject;

			_spawnTimer=0;
		}
	}
}
