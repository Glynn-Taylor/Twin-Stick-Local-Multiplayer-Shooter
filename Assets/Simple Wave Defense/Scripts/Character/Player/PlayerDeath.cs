using UnityEngine;
using System.Collections;

public class PlayerDeath : CharacterDeath {

	public override void OnDeath(CharacterHealth health){
		GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameOverManager> ().PlayerDeath ();
		Destroy (gameObject);
	}
}
