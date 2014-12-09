using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	public Text ProgressText;
	public RectTransform ProgressBarHandle;
	public float ProgressStep = 1f;
	public int StepCount = 127;
	public int Progress = 0;

	private float _startPosition;

	// Use this for initialization
	void Start () {
		_startPosition = ProgressStep * -1 * StepCount;
	}

	// Ranges from 0 (no prog) to 1 (100%)
	public void SetProgress(float progress) {
		Progress = Mathf.CeilToInt (StepCount * progress);
		ProgressText.text = Mathf.RoundToInt(progress * 100) + "%";
		RefreshBarPosition ();
	}

	void RefreshBarPosition() {
		ProgressBarHandle.anchoredPosition = new Vector2(_startPosition + (Progress * ProgressStep),
		                                                 ProgressBarHandle.anchoredPosition.y);
	}
}
