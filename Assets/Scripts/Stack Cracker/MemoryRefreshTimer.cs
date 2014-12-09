using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Mask))]
public class MemoryRefreshTimer : MonoBehaviour {

	public Text CountdownText;
	public RectTransform RefreshBar;
	public float ProgressStep = 12.2f;
	public int StepCount = 10;
	public int Progress = 0;
	public float RemainingTime;
	public bool Stopped;

	private float _startPosition;
	private float _totalTime;

	// Use this for initialization
	void Start () {
		_startPosition = ProgressStep * -1 * StepCount;
		Stopped = false;
	}

	public void StopTimer() {
		Stopped = true;
	}

	// Starts the countdown timer.
	public void StartTimer(float seconds) {
		Stopped = false;
		_totalTime = seconds;
		RemainingTime = seconds;
		Progress = StepCount;
	}
	public void SubtractTime(float seconds) {
		_totalTime -= seconds;
		RemainingTime -= seconds;
	}

	public void AddTime(float seconds) {
		_totalTime += seconds;
		RemainingTime += seconds;
	}
	
	// Update is called once per frame
	void Update () {
		if (RemainingTime == 0 || Stopped)
			return;

		// Update time.
		RemainingTime -= Time.deltaTime;
		if (RemainingTime <= 0) {
			RemainingTime = 0;
			Messenger.Broadcast("stackTimerExpired");
		}

		Progress = Mathf.CeilToInt (RemainingTime / _totalTime * 10);

		// Update the text on the countdown.
		CountdownText.text = RemainingTime.ToString ("F") + " seconds";
		// Update the progress bar.
		RefreshBarPosition ();
	}

	void RefreshBarPosition() {
		RefreshBar.anchoredPosition = new Vector2(_startPosition + (Progress * ProgressStep),
		                                          RefreshBar.anchoredPosition.y);
	}
}
