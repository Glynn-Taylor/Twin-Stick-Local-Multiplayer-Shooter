using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class ControllerButton : MonoBehaviour {
	public string Player="P1";
	public string Button="Shoot";
	public bool Deselectable = false;
	public UnityEvent OnSelect;
	public UnityEvent OnDeselect;


	private bool _Selected=false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown (Player +"_"+ Button)) {
			if(_Selected)
				OnSelect.Invoke();
			else
				if(Deselectable)
					OnDeselect.Invoke();
				else
					OnSelect.Invoke();
			_Selected=!_Selected;
		}
	}
}
