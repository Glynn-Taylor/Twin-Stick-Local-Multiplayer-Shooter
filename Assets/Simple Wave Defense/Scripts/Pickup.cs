using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pickup : MonoBehaviour {

	public string Type;
	public float rotateSpeed = 90f;

	void Update() {
		transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
	}

	void OnTriggerEnter (Collider other) {

		if (other.gameObject.tag!="Player") {
			return;
		}
		for (int i=0; i<other.transform.childCount; i++) {
			if(other.transform.GetChild(i).GetComponent<PlayerWeapon>()){
				other.transform.GetChild(i).GetComponent<PlayerWeapon> ().AddWeapon (Type);
			}
		}

		GetComponent<AudioSource>().Play();

		GetComponent<Collider>().enabled = false;
		for (int i=0; i<transform.childCount; i++) {
			Destroy(transform.GetChild(i).gameObject);
		}

		Destroy(gameObject, 1);
	}
}
