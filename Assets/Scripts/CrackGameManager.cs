using UnityEngine;
using System.Collections;

public class CrackGameManager : MonoBehaviour {

	public StackCrackerManager StackManager;
	public BankTransferController BankController;
	public GameOverController GameOverScreen;
	public TraceTrackerManager Tracer;
	public int BonusCashMultiplier = 1000;
	public CrackDifficulty[] DifficultyOrder = new CrackDifficulty[] {
		CrackDifficulty.VeryEasy,
		CrackDifficulty.Easy,
		CrackDifficulty.Easy,
		CrackDifficulty.Medium,
		CrackDifficulty.Medium,
		CrackDifficulty.Hard,
		CrackDifficulty.Hard
	};

	private int _round;

	// Use this for initialization
	void Awake () {
		Messenger<CrackDifficulty, float>.AddListener ("crackComplete", OnCrackComplete);
		Messenger.AddListener ("startGame", OnStartGame);
		Messenger.AddListener ("bankTransferCompleted", OnTransferCompleted);
		Messenger.AddListener ("traceCompleted", OnTraceCompleted);
	}

	void OnStartGame() {
		_round = 0;
		StackManager.gameObject.SetActive (true);
		StartNewRound ();
		Tracer.StartTrace (90);
	}

	void StartNewRound() {
		var newDifficulty = DifficultyOrder[_round];
		Messenger<CrackDifficulty>.Broadcast("difficultyChange", newDifficulty);
		StackManager.InitializeStack (newDifficulty);
	}

	protected virtual void OnCrackComplete(CrackDifficulty difficulty, float time) {
		if(!Tracer.Tracing)
			return;

		var cashReward = BonusCashMultiplier * (float)difficulty;
		cashReward += time * (BonusCashMultiplier / (10 - (int)difficulty));
		Messenger<float>.Broadcast ("addCash", cashReward);

		BankController.gameObject.SetActive (true);
		BankController.StartTransfer (cashReward);
	}

	protected virtual void OnTransferCompleted() {
		BankController.gameObject.SetActive (false);

		_round++;

		if (_round > DifficultyOrder.Length - 1) {
			GameOverScreen.gameObject.SetActive(true);
			Messenger.Broadcast("gameOver");
			return;
		}
		
		StartNewRound ();
	}

	protected virtual void OnTraceCompleted() {
		StackManager.StopHack ();
		GameOverScreen.gameObject.SetActive (true);
		Messenger.Broadcast ("gameOver");
	}
}
