using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {

	[System.Serializable]
	public class Weapon
	{
		public string Name="";
		public int MaxShots=-1;
		[HideInInspector]
		public int ShotsLeft = -1;
		public bool Piercing = false;
		public int DamagePerShot = 20; 
		public int NumberOfBullets = 1;
		public float FiringCooldown = 0.15f;
		public float BulletSpread = 10f;
		public float Range = 100f;
		public float Speed = 600f;
		public GameObject Graphic;
		public Color BulletColor=Color.yellow;
		public AudioClip FireClip;

		public void OnPickup(){
			ShotsLeft = MaxShots;
		}
	}

	public LayerMask shootableMask;
	public GameObject Bullet;
	public Transform Firepoint;
	public string player;
	public Weapon DefaultWeapon = new Weapon();
	public Weapon[] ExtraWeapons;


	private Weapon _EquippedWeapon;
	float _firingTimer;         
	RaycastHit shootHit;     
	ParticleSystem _gunParticles;     
	LineRenderer _gunLine;         
	AudioSource _gunAudio;
	Light _gunLight;         
	// The proportion of the timeBetweenBullets that the effects will display for.
	float _effectsDisplayTime = 0.5f;


	
	void Awake() {
		// Set up the references.
		_gunParticles = GetComponent<ParticleSystem> ();
		_gunAudio = GetComponent<AudioSource> ();
		_gunLight = GetComponent<Light> ();
		_EquippedWeapon = DefaultWeapon;
	}
	
	void Update() {

		_firingTimer += Time.deltaTime;
		
		// If the Fire1 button is being press and it's time to fire...
		if ((Input.GetButton (player+"_Shoot") || Input.GetAxisRaw(player+"_Shoot")>0.5f) && _firingTimer >= _EquippedWeapon.FiringCooldown) {
			_gunParticles.startColor = _EquippedWeapon.BulletColor;
			_gunLight.color = _EquippedWeapon.BulletColor;
			Shoot ();
		}
		
		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		if (_firingTimer >= _EquippedWeapon.FiringCooldown * _effectsDisplayTime) {
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
		_gunLight.intensity = 1 + (0.5f * (_EquippedWeapon.NumberOfBullets - 1));
		_gunLight.enabled = true;
		
		// Stop the particles from playing if they were, then start the particles.
		_gunParticles.Stop();
		_gunParticles.startSize = 1 + (0.1f * (_EquippedWeapon.NumberOfBullets - 1));
		//_gunParticles.Play();
		
		for (int i = 0; i < _EquippedWeapon.NumberOfBullets; i++) {
			// Make sure our bullets spread out in an even pattern.
			float angle = ((float)i/(float)_EquippedWeapon.NumberOfBullets) * _EquippedWeapon.BulletSpread - (_EquippedWeapon.BulletSpread / 2);
			Quaternion rot = Quaternion.Euler(Firepoint.rotation.eulerAngles +Vector3.up*angle);
			GameObject instantiatedBullet = Instantiate(Bullet, Firepoint.position, rot) as GameObject;
			Bullet bullet = instantiatedBullet.GetComponent<Bullet>();
			bullet.bulletColor = _EquippedWeapon.BulletColor;
			bullet.damage=_EquippedWeapon.DamagePerShot;
			bullet.speed=_EquippedWeapon.Speed;
			bullet.piercing=_EquippedWeapon.Piercing;
		}
		if (_EquippedWeapon.ShotsLeft > 0) {
			_EquippedWeapon.ShotsLeft--;
			if(_EquippedWeapon.ShotsLeft==0)
				Equip(DefaultWeapon);

		}
	}
	public void AddWeapon(string name){
		Weapon wep = null;
		for (int i=0; i<ExtraWeapons.Length; i++) {
			if(ExtraWeapons[i].Name==name){
				wep=ExtraWeapons[i];
				Equip(wep);
			}
		}
	}
	public void Equip(Weapon wep){
		_EquippedWeapon.Graphic.SetActive(false);
		wep.OnPickup();
		_EquippedWeapon=wep;
		_EquippedWeapon.Graphic.SetActive(true);
		_gunAudio.clip = _EquippedWeapon.FireClip;
	}
}
