using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pickup : MonoBehaviour {

	public enum PickupType {Bullet, Bounce, Pierce, Health}
	public PickupType pickupType = PickupType.Bullet;
	public float rotateSpeed = 90f;

	void Update() {
		transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
	}

	void OnTriggerEnter (Collider other) {

		if (other.gameObject.tag!="Player") {
			return;
		}

		/*switch (pickupType) {
			case PickupType.Bullet:
				if (other.GetComponentInChildren<PlayerShooting>().numberOfBullets <= 36) {
					other.GetComponentInChildren<PlayerShooting>().numberOfBullets++;
				}
				break;
				
			case PickupType.Bounce:
				other.GetComponentInChildren<PlayerShooting>().BounceTimer = 0;
				break;
				
			case PickupType.Pierce:
				other.GetComponentInChildren<PlayerShooting>().PierceTimer = 0;
				break;
				
			case PickupType.Health:
				other.GetComponentInChildren<PlayerHealth>().AddHealth(25);
				break;
		}*/

		GetComponent<AudioSource>().Play();

		GetComponent<Collider>().enabled = false;
		for (int i=0; i<transform.childCount; i++) {
			Destroy(transform.GetChild(i).gameObject);
		}

		Destroy(gameObject, 1);
	}
}
