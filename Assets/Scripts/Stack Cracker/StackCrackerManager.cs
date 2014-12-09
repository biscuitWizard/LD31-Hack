using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class StackCrackerManager : MonoBehaviour {

	public MemoryBoardController MemoryBoard;
	public MemoryRefreshTimer RefreshTimer;
	public MemoryStackLoader RefreshLoader;
	public RectTransform FirewallCrashMessage;
	public MemoryStackTextGenerator StackText;

	private CrackDifficulty _difficulty;
	private List<MemoryCellController> _corruptedCells = new List<MemoryCellController>();

	// Use this for initialization
	void Awake () {
		Messenger<MemoryCellController>.AddListener ("memoryCellClicked", OnMemoryCellClicked); // Await clicks from cells.
		Messenger.AddListener ("stackTimerExpired", OnStackTimerExpired);
		Messenger.AddListener ("stackLoadingComplete", OnStackLoadComplete);
		Messenger.AddListener ("stackCrashComplete", OnStackCrashComplete);
	}

	// Initialize a new hack attempt.
	public void InitializeStack(CrackDifficulty difficulty) {
		_difficulty = difficulty;
		RebuildStack ();
		StackText.SetCrashed (false);
	}

	// Rebuild the stack in the current hack attempt.
	public void RebuildStack() {
		_corruptedCells.Clear ();
		MemoryBoard.Regenerate ((int)_difficulty);
		
		switch (_difficulty) {
		case CrackDifficulty.VeryEasy:
			RefreshTimer.StartTimer(20);
			break;
		case CrackDifficulty.Easy:
			RefreshTimer.StartTimer(16);
			break;
		case CrackDifficulty.Medium:
			RefreshTimer.StartTimer(12);
			break;
		case CrackDifficulty.Hard:
			RefreshTimer.StartTimer(8);
			break;
		}
	}

	public void StopHack() {
		MemoryBoard.CrashStack ();
		RefreshTimer.StopTimer ();
		StackText.SetCrashed (true);
	}

	// Check to see if the stack has crashed.
	bool IsCrashed(IEnumerable<MemoryCellController> keys) {		
		return keys.Count() == keys.Intersect (_corruptedCells).Count ();
	}
	
	protected virtual void OnMemoryCellClicked(MemoryCellController memoryCell) {
		memoryCell.SetCorrupted (true);
		_corruptedCells.Add (memoryCell);

		Messenger<string>.Broadcast ("print", string.Format ("StackCracker> set {0} {1}",
		                                                     memoryCell.GetText (),
		                                                     MemoryBoard.GenerateAddress()));

		var keys = MemoryBoard.GetMemoryKeys ();
		// If this is not a key, go into special parsing and return,
		// We won't need to check for a victory condition.
		if (!keys.Contains (memoryCell)) {
			if(memoryCell.IsBonusCell) {
				//if(Random.Range (0, 2) > 0) {
					RefreshTimer.AddTime(1.5f);
					Messenger<string>.Broadcast ("print", "> error: line 2839 -> system clock sync mis-aligned\n> 1.5 seconds added to clock");
				//}
			} else {
				RefreshTimer.SubtractTime(1f);
				Messenger<string>.Broadcast ("print", "> memory fault corrected\n> system administrator has been notified");
			}
			return;
		}

		// Check for victory condition
		if (IsCrashed (keys)) {
			Messenger<string>.Broadcast ("print", "> !!!SYSTEM FAULT!!! firewall.exe has crashed");

			// We won!
			MemoryBoard.CrashStack();
			FirewallCrashMessage.gameObject.SetActive(true);
			RefreshTimer.StopTimer();
			StackText.SetCrashed(true);
		}
	}

	protected virtual void OnStackTimerExpired() {
		MemoryBoard.SetEnabled (false);
		RefreshLoader.gameObject.SetActive (true);
		RefreshLoader.StartLoading (1.5f);
	}

	protected virtual void OnStackLoadComplete() {
		RefreshLoader.gameObject.SetActive (false);
		RebuildStack ();
		MemoryBoard.SetEnabled (true);
	}

	protected virtual void OnStackCrashComplete() {
		FirewallCrashMessage.gameObject.SetActive(false);
		Messenger<CrackDifficulty, float>.Broadcast ("crackComplete", _difficulty, RefreshTimer.RemainingTime);
	}
}
