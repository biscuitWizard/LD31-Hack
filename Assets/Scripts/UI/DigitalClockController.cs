using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class DigitalClockController : MonoBehaviour {
	
	public string TimeFormat = "hh:mm tt MM/dd/yyyy";
	public float BlinkInterval = 1;

	private Text _text;
	private float _timeSinceLastBlink;
	private bool _blinking;

	void Start() {
		_text = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		var timeString = DateTime.Now.ToString(TimeFormat);

		if (_timeSinceLastBlink > BlinkInterval) {
			_blinking = !_blinking;
			_timeSinceLastBlink = 0;
		}

		_text.text = _blinking ? timeString.Replace (":", " ") : timeString;

		_timeSinceLastBlink += Time.deltaTime;
	}
}
