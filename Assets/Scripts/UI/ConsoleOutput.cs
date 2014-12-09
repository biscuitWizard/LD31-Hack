using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class ConsoleOutput : MonoBehaviour {

	public Scrollbar ConsoleScrollBar;

	private Text _text;

	void Awake() {
		_text = GetComponent<Text> ();
		Messenger<string>.AddListener ("print", OnPrint);
		//Messenger<string, Color>.AddListener ("print", OnPrintColor);
	}

	protected virtual void OnPrint(string msg) {
		_text.text += "\n" + msg;

		// Scroll to bottom.
		ConsoleScrollBar.value = 0;
	}

	protected virtual void OnPrintColor(string msg, Color color) {
		_text.text += "\n" + msg;

		// Scroll to bottom.
		ConsoleScrollBar.value = 0;
	}
}
