using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	// The speed that the player will move at.
	public float speed = 6f;
	public string Player = "P1";
	private Color[] _playerColors = {Color.white,Color.green,Color.red,Color.blue,Color.yellow,Color.magenta};
	public SkinnedMeshRenderer renderer;
	// The vector to store the direction of the player's movement.
	Vector3 movement;
	// Reference to the animator component.
	Animator anim;    
	// Reference to the player's rigidbody.
	Rigidbody playerRigidbody;    
	int floorMask;    
	float camRayLength = 100f;

	void Awake() {
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator>();
		if (anim == null) {
			for(int i=0;i<transform.childCount;i++){
				anim=transform.GetChild(i).GetComponent<Animator>();
				if(anim!=null)
					break;
			}
		}
		if (renderer) {
			renderer.material.color=_playerColors[int.Parse(Player.Substring(1))];
		}
		playerRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		// Store the input axes.
		float h = Input.GetAxisRaw(Player+"_LS_X");
		float v = Input.GetAxisRaw(Player+"_LS_Y");
		Move (h, v);
		Turning ();
		Animating (h, v);
	}
	
	void Move(float h, float v) {
		// Set the movement vector based on the axis input.
		movement.Set (h, 0f, v);
		
		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;
		
		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition(transform.position + movement);
	}
	
	void Turning() {
		float h = Input.GetAxisRaw(Player+"_RS_X");
		float v = Input.GetAxisRaw(Player+"_RS_Y");
		if (Mathf.Abs(h) + Mathf.Abs(v) > 0.1f) {
			Vector3 playerToMouse = new Vector3 (h, 0, -v);

			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
		
			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation (newRotation);
		}
	}
	void Animating(float h, float v) {
		// Create a boolean that is true if either of the input axes is non-zero.
		bool walking = h != 0f || v != 0f;
		
		// Tell the animator whether or not the player is walking.
		anim.SetBool("IsWalking", walking);
	}
}
