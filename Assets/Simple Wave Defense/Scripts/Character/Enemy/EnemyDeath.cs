using UnityEngine;
using System.Collections;

public class EnemyDeath : CharacterDeath {

	public override void OnDeath(CharacterHealth health){
		GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager> ().EnemyDeath ();
		Destroy (gameObject);
	}
}
