using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameOverController : MonoBehaviour {

	public ScoreManager ScoreManager;
	public Text ScoreSummary;
	public Text FinalScore;

	private string _scoreSummaryFormat;
	private string _finalScoreFormat;

	void Awake() {
		_scoreSummaryFormat = ScoreSummary.text;
		_finalScoreFormat = FinalScore.text;

		Messenger.AddListener ("gameOver", OnGameOver);
	}

	protected virtual void OnGameOver() {
		ScoreSummary.text = string.Format (_scoreSummaryFormat,
		                                   ScoreManager.Cash.ToString("C"),
		                                   Total(ScoreManager.LevelTimes) / ScoreManager.LevelTimes.Count,
		                                   Total(ScoreManager.LevelTimes));
		FinalScore.text = string.Format (_finalScoreFormat, Mathf.RoundToInt ((ScoreManager.Cash 
		                                                     * (Total (ScoreManager.LevelTimes) / ScoreManager.LevelTimes.Count))
		                                 / Total (ScoreManager.LevelTimes)));
	}



	float Total(IList<float> numbers) {
		var total = 0f;
		foreach(var n in numbers)
			total += n;
		return total;
	}

	public void OnRestart() {
		Messenger.Broadcast ("startGame");
		gameObject.SetActive (false);
	}
}
