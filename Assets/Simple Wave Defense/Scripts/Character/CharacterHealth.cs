using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody))]
public class CharacterHealth : MonoBehaviour {
	private const float MAX_WEIGHT = 30f;

	public int MaxHealth;
	public Slider HealthBarPrefab;
	[Range(0,MAX_WEIGHT)]
	public float Weight=1;
	public float HealthbarOffset = 0.2f;

	private int _Health;
	GameObject _healthbarManager;
	WaveManager _waveManager;
	ScoreManager _scoreManager;
	PickupManager _pickupManager;
	CharacterDeath _DeathHandler;
	Slider _healthUI;
	bool _isDead=false;

	public bool Alive{
		get{
			return !_isDead;
		}
	}
	// Use this for initialization
	void Awake() {
		_Health = MaxHealth;
		_healthbarManager = GameObject.Find("HealthbarCanvas");
		if (!_healthbarManager)
			CreateHealthBarManager ();
		_waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
		_scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		_pickupManager = GameObject.Find("PickupManager").GetComponent<PickupManager>();
		_DeathHandler = GetComponent<CharacterDeath> ();
		if (_DeathHandler == null) {
			Debug.LogWarning("<CharacterHealth[Awake]>: "+gameObject.name+" has no death handler, adding default");
			_DeathHandler=(CharacterDeath)gameObject.AddComponent<GenericDeath>();
		}
	}
	void Start(){
		// Instantiate our health bar GUI slider.
		_healthUI = Instantiate(HealthBarPrefab, gameObject.transform.position, Quaternion.identity) as Slider;
		_healthUI.gameObject.transform.SetParent(_healthbarManager.transform, false);
		_healthUI.GetComponent<Healthbar>().Target = gameObject;
		_healthUI.GetComponent<Healthbar>().YOffset = GetComponent<CapsuleCollider>().height+HealthbarOffset;
		_healthUI.gameObject.SetActive(false);
		//Only do on first take damage???
		_healthUI.gameObject.SetActive(true);
	}
	
	public void TakeDamage(int amount, Vector3 hitPoint) {
		
		// If the enemy is dead there's no need to take damage so exit the function.
		if (_isDead)
			return;
		
		GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * -300*(MAX_WEIGHT-Weight)/MAX_WEIGHT, hitPoint);
		
		// Reduce the current health by the amount of damage sustained.
		_Health -= amount;
		
		// Set the health bar's value to the current health.
		if (_Health <= MaxHealth) {
			_healthUI.gameObject.SetActive(true);
		}
		int sliderValue = (int) Mathf.Round(((float)_Health / (float)MaxHealth) * 100);
		_healthUI.value = sliderValue;
		
		// If the current health is less than or equal to zero the enemy is dead.
		if (_Health <= 0) {
			Death ();
		}
	}

	void Death ()
	{
		_isDead = true;
		Destroy (_healthUI.gameObject);
		_DeathHandler.OnDeath (this);
	}

	void CreateHealthBarManager ()
	{
		GameObject hbManager = new GameObject ("HealthbarCanvas");
		Canvas canvas = hbManager.AddComponent<Canvas>();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		GraphicRaycaster gr = hbManager.AddComponent<GraphicRaycaster>();
		_healthbarManager = hbManager;
	}
}
