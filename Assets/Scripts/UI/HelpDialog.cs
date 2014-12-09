using UnityEngine;
using System.Collections;

public class HelpDialog : MonoBehaviour {

	public void OnBegin() {
		Messenger.Broadcast ("startGame");
		gameObject.SetActive (false);
	}
}
