using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class DifficultyStatus : MonoBehaviour {

	public string StringFormat = "Difficulty: {0}";

	private Text _text;

	// Use this for initialization
	void Awake () {
		Messenger<CrackDifficulty>.AddListener ("difficultyChange", OnDifficultyChange);
		_text = GetComponent<Text> ();
	}
	
	protected virtual void OnDifficultyChange(CrackDifficulty difficulty) {
		_text.text = string.Format (StringFormat, difficulty.ToString ());
	}
}
