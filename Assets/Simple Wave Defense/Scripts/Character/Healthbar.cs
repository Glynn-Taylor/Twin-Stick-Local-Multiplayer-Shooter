using UnityEngine;

public class Healthbar : MonoBehaviour {

	public GameObject Target;
	private Vector3 _Offset;

	public float YOffset{
		set{
			_Offset = new Vector3(0,value,0);
		}
	}

	void Update() {
		if (Target == null) {
			return;
		}

		Vector3 screenPos = Camera.main.WorldToScreenPoint(Target.transform.position+_Offset);
		gameObject.transform.position = screenPos;
	}
}
