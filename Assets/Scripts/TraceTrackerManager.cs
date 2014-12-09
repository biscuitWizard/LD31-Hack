using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TraceTrackerManager : MonoBehaviour {

	public ProgressBar ProgressBar;
	public Text CountdownText;
	public string CountdownFormat = "{0} seconds";
	public float RemainingTime = 0;
	public bool Tracing;

	private float _totalTime;

	// Use this for initialization
	void Awake () {
		Messenger.AddListener ("gameOver", OnGameOver);
	}
	
	// Update is called once per frame
	void Update () {
		if(!Tracing || RemainingTime == 0)
			return;

		RemainingTime -= Time.deltaTime;

		if(RemainingTime < 0)
			RemainingTime = 0;

		ProgressBar.SetProgress (1 - (RemainingTime / _totalTime));
		CountdownText.text = string.Format (CountdownFormat, Mathf.RoundToInt (RemainingTime));

		if (RemainingTime == 0) {
			Messenger.Broadcast ("traceCompleted");
		}
	}

	public void StartTrace(float seconds) {
		_totalTime =
			RemainingTime = seconds;
		Tracing = true;
	}

	public void StopTrace() {
		Tracing = false;
		RemainingTime = 0;
		_totalTime = 0;
	}

	protected virtual void OnGameOver() {
		StopTrace ();
	}
}
