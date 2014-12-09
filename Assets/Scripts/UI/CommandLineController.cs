using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(InputField))]
public class CommandLineController : MonoBehaviour {
	InputField _inputField;

	// Use this for initialization
	void Start () {
		_inputField = GetComponent<InputField> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnSubmitInput() {
		if (Input.GetKeyDown (KeyCode.Return)) {
			_inputField.text = string.Empty;	
		}
	}
}
