using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Camera))]
public class CameraView : MonoBehaviour {
	
	// The position that that camera will be following.
	public Transform[] _targets; 
	// The speed with which the camera will be following.
	public float smoothing = 5f;                        
	Vector3 offset; 
	private Camera _camera;

	void Start() {
		_camera = GetComponent<Camera> ();
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		_targets = new Transform[players.Length];
		for (int i=0; i<players.Length; i++) {
			_targets[i]=players[i].transform;
		}
		// Calculate the initial offset.
		offset = transform.position - _targets[0].position;
		offset = new Vector3 (0, offset.y, offset.z);
	}
	
	void FixedUpdate () {
		Vector3 Center = DetermineCenter ();
		SetCenter (Center);
		SetZoom (Center);
		// Create a postion the camera is aiming for based on the offset from the target.
		//Vector3 targetCamPos = target.position;
		
		// Smoothly interpolate between the camera's current position and it's target position.
		//transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}

	void SetZoom (Vector3 center)
	{
		Vector3 Max = Vector3.Cross (new Vector3 (center.x, center.z, 0), Vector3.up);
		_camera.orthographicSize =6+ Mathf.Abs(Max.z) / 2;
	}

	void SetCenter (Vector3 center)
	{
		Vector3 targetPosition = center+offset;
		transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing * Time.deltaTime);
	}

	Vector3 DetermineCenter ()
	{

		//#####################################		CLEANUP
		float minX=0;
		float minY=0;
		float maxX=0;
		float maxY=0;
		float yLevel = 0;
		for (int i =0; i<_targets.Length; i++) {
			if (_targets [i] != null) {
				minX=_targets[i].position.x;
				minY=_targets[i].position.z; 
				maxX=_targets[i].position.x; 
				maxY=_targets[i].position.z;
				yLevel = _targets[i].position.y;
				break;
			}
		}
		float x, y;
		for (int i =1; i<_targets.Length; i++) {
			if(_targets[i]!=null){
				x = _targets[i].position.x;
				y = _targets[i].position.z;
				if(x<minX)
					minX=x;
				if(x>maxX)
					maxX=x;
				if(y<minY)
					minY=y;
				if(y>maxY)
					maxY=y;
			}
		}

		return new Vector3 ((minX + maxX) / 2, yLevel, (minY + maxY) / 2);
	}
}