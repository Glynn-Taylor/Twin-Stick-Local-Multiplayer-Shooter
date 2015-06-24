using UnityEngine;
using System.Collections;

public abstract class CharacterDeath : MonoBehaviour {

	public abstract void OnDeath(CharacterHealth health);
}
