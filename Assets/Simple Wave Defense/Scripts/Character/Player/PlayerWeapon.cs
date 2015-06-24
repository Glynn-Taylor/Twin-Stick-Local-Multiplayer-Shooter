using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {
	
	public Color[] bulletColors;

	public int DamagePerShot = 20;  
	public int NumberOfBullets = 1;
	public float TimeBetweenBullets = 0.15f;
	public float AngleBetweenBullets = 10f;
	public float Range = 100f;
	public LayerMask shootableMask;
	public GameObject Bullet;
	public Transform Firepoint;
	public string player;


	float _firingTimer;         
	RaycastHit shootHit;     
	ParticleSystem _gunParticles;     
	LineRenderer _gunLine;         
	AudioSource _gunAudio;
	Light _gunLight;         
	// The proportion of the timeBetweenBullets that the effects will display for.
	float _effectsDisplayTime = 0.2f;
	Color _bulletColor=Color.yellow;

	
	void Awake() {
		// Set up the references.
		_gunParticles = GetComponent<ParticleSystem> ();
		_gunAudio = GetComponent<AudioSource> ();
		_gunLight = GetComponent<Light> ();
		if (bulletColors.Length > 0)
			_bulletColor = bulletColors [0];
	}
	
	void Update() {

		_firingTimer += Time.deltaTime;
		
		// If the Fire1 button is being press and it's time to fire...
		if ((Input.GetButton (player+"_Shoot") || Input.GetAxisRaw(player+"_Shoot")>0.5f) && _firingTimer >= TimeBetweenBullets) {
			_gunParticles.startColor = _bulletColor;
			_gunLight.color = _bulletColor;
			Shoot ();
		}
		
		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		if (_firingTimer >= TimeBetweenBullets * _effectsDisplayTime) {
			DisableEffects ();
		}
	}
	
	public void DisableEffects() {
		// Disable the line renderer and the light.
		_gunLight.enabled = false;
	}
	
	void Shoot() {
		// Reset the timer.
		_firingTimer = 0f;
		
		// Play the gun shot audioclip.
		_gunAudio.pitch = Random.Range(1.2f, 1.3f);
		_gunAudio.Play ();
		
		// Enable the light.
		_gunLight.intensity = 1 + (0.5f * (NumberOfBullets - 1));
		_gunLight.enabled = true;
		
		// Stop the particles from playing if they were, then start the particles.
		_gunParticles.Stop();
		_gunParticles.startSize = 1 + (0.1f * (NumberOfBullets - 1));
		//_gunParticles.Play();
		
		for (int i = 0; i < NumberOfBullets; i++) {
			// Make sure our bullets spread out in an even pattern.
			float angle = i * AngleBetweenBullets - ((AngleBetweenBullets / 2) * (NumberOfBullets - 1));
			Quaternion rot = Firepoint.rotation * Quaternion.AngleAxis(angle, Vector3.up);
			GameObject instantiatedBullet = Instantiate(Bullet, Firepoint.position, rot) as GameObject;
			instantiatedBullet.GetComponent<Bullet>().bulletColor = _bulletColor;
		}
	}
}
