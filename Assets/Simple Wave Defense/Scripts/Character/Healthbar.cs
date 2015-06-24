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

		gameObject.transform.position = Camera.main.WorldToScreenPoint(Target.transform.position+_Offset);
	}
}
