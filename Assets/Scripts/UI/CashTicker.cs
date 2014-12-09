using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class CashTicker : MonoBehaviour {
	public float Cash;

	private Text _tickerText;

	// Use this for initialization
	void Awake () {
		Messenger<float>.AddListener ("addCash", OnAddCash);
		Messenger.AddListener ("startGame", OnStartGame);
		_tickerText = GetComponent<Text> ();
	}

	protected virtual void OnAddCash(float newCash) {
		Cash += newCash;

		_tickerText.text = Cash.ToString ("C");
	}

	protected virtual void OnStartGame() {
		Cash = 0;
		_tickerText.text = 0.ToString ("C");
	}
}
