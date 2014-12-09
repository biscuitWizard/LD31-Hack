using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class MemoryStackLoader : MonoBehaviour {

	public ProgressBar LoadingBar;

	private float _remainingTime;
	private float _totalTime;
	private bool _isLoading;
	
	void Update() {
		if(!_isLoading)
			return;

		_remainingTime -= Time.deltaTime;

		if (_remainingTime <= 0) {
			_isLoading = false;
			_remainingTime = 0;
		}

		LoadingBar.SetProgress (1 - (_remainingTime / _totalTime));

		if(_remainingTime == 0)
			Messenger.Broadcast("stackLoadingComplete");
	}

	public void StartLoading(float seconds) {
		_isLoading = true;
		_totalTime = seconds;
		_remainingTime = seconds;
	}
}
