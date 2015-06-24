using UnityEngine;
using System.Collections;

[RequireComponent(typeof (EnemyMove))]
public class EnemyAttack : MonoBehaviour {

	// The time in seconds between each attack.
	public float AttackCooldown = 0.5f;  
	// The amount of health taken away per attack.
	public int AttackDamage = 10;               
	public float AttackRange = 1;

	EnemyMove _Movement;
	float _attackTimer;                               
	
	void Awake() {
		// Setting up the references.
		_Movement = GetComponent<EnemyMove>();
	}
	
	void Update() {
		// Add the time since Update was last called to the timer.
		_attackTimer += Time.deltaTime;
		
		// If the timer exceeds the time between attacks, the player is in range,
		// we are alive and the player is alive then attack.
		if (_attackTimer >= AttackCooldown && _Movement.Alive && _Movement.Target && Vector3.Distance(transform.position,_Movement.Target.position)<AttackRange && _Movement.Target.GetComponent<CharacterHealth>().Alive) {
			Attack();
		}
	}
	
	
	void Attack() {
		// Reset the timer.
		_attackTimer = 0f;
		// Damage the player.
		_Movement.Target.GetComponent<CharacterHealth>().TakeDamage(AttackDamage,_Movement.Target.position);
	}
}