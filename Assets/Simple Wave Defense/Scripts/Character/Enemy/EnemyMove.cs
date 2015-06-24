using UnityEngine;
using System.Collections;

[RequireComponent(typeof (CharacterHealth))]
public class EnemyMove : MonoBehaviour {
	Transform _Target;
	public Transform Target{
		get{
			return _Target;
		}
	}
	// Reference to this enemy's health.
	CharacterHealth _Health;
	public bool Alive{
		get{
			return _Health.Alive;
		}
	}
	// Reference to the nav mesh agent.
	NavMeshAgent _Navigation;   
	// Reference to the animator.
	Animator _Animation;
	float _CheckPlayerClosestTimer;
	const float CHECK_PLAYER_CLOSEST_TIME=1f;

	void Awake(){
		_Target = GameObject.FindGameObjectWithTag ("Player").transform;
		_Health = GetComponent<CharacterHealth> ();
		_Navigation = GetComponent <NavMeshAgent> ();
		_Animation = GetComponent<Animator>();

		//SetRandomNavTarget();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (_Health.Alive) {
			CheckClosestPlayer();
			if(_Target)
				_Navigation.SetDestination(_Target.position);
			float currentSpeed = _Navigation.velocity.magnitude;
			_Animation.speed = currentSpeed;
			
		}
	}

	void CheckClosestPlayer ()
	{
		_CheckPlayerClosestTimer+=Time.deltaTime;
		if(_CheckPlayerClosestTimer>CHECK_PLAYER_CLOSEST_TIME){
			_Target=FindClosestLivingPlayer();
			_CheckPlayerClosestTimer=0;
		}
	}

	Transform FindClosestLivingPlayer ()
	{
		GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
		int bestIndex=0;
		float bestDistance=999;
		for (int i=0; i<Players.Length; i++) {
			if(Players[i].GetComponent<CharacterHealth>().Alive){
				float distance = Vector3.Distance(Players[i].transform.position,transform.position);
				if(distance<bestDistance){
					bestDistance=distance;
					bestIndex=i;
				}
			}
		}
		return Players [bestIndex].transform;
	}
}
