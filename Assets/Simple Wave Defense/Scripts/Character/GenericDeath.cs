using UnityEngine;
using System.Collections;

public class GenericDeath : CharacterDeath {
	#region implemented abstract members of CharacterDeath

	public override void OnDeath (CharacterHealth health)
	{
		Destroy (gameObject);
	}

	#endregion



}
