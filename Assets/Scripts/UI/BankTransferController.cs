using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class BankTransferController : MonoBehaviour {

	public Text TransferRemaining;
	public ProgressBar TransferProgress;
	public Text ToText;
	public Text FromText;

	private float _totalAmount;
	private float _remainingAmount;
	private bool _transferring;

	public void StartTransfer(float amount) {
		_remainingAmount = amount;
		_totalAmount = amount;
		_transferring = true;
	}

	void Update() {
		if(!_transferring)
			return; // Nothing to do.

		//_remainingAmount -= Time.deltaTime * (_totalAmount / 6);
		_remainingAmount = Mathf.Max (Mathf.Lerp (_remainingAmount, -200, Time.deltaTime / 2), 0);


		TransferRemaining.text = _remainingAmount.ToString ("C");
		TransferProgress.SetProgress (1 - (_remainingAmount / _totalAmount));

		if (_remainingAmount <= 0) {
			_transferring = false;
			Messenger.Broadcast ("bankTransferCompleted");
		}
	}
}
