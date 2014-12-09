using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {

	// Statistics
	public float Cash;
	public List<float> LevelTimes;

	// Use this for initialization
	void Awake () {
		Messenger<CrackDifficulty, float>.AddListener ("crackComplete", OnCrackComplete);
		Messenger<float>.AddListener ("addCash", OnAddCash);
		Messenger.AddListener ("startGame", OnStartGame);
	}

	protected virtual void OnCrackComplete(CrackDifficulty difficulty, float time) {
		LevelTimes.Add (time);
	}
	
	protected virtual void OnAddCash(float amount) {
		Cash += amount;
	}

	protected virtual void OnStartGame() {
		Cash = 0;
		LevelTimes.Clear ();
	}
}
