using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[RequireComponent(typeof(RectTransform))]
public class MemoryBoardController : MonoBehaviour {

	public Color[] Colors = new Color[] {
		Color.cyan,
		Color.yellow,
		Color.green,
		Color.red
	};

	public MemoryCellController[] MemoryCells;

	public GameObject MemoryCellPrefab;
	public int GridHeight = 12;
	public int GridWidth = 7;
	public string AddressCharacters = "ABCDEF0123456789";

	private RectTransform _memoryBoard;
	private Dictionary<Vector2, MemoryCellController> _memoryCells = new Dictionary<Vector2, MemoryCellController>();
	private List<MemoryCellController> _memoryKeys = new List<MemoryCellController> ();

	// Use this for initialization
	void Awake () {
		_memoryBoard = GetComponent<RectTransform> ();

		PopulateBoard ();
	}

	public void Regenerate(int difficulty) {
		SetEnabled (false);
		var uniqueCells = new List<MemoryCellController> ();
		var bonusCount = 0;

		foreach (var cell in _memoryCells.Values) {
			// Reset corrupted state
			cell.SetCorrupted(false);

			// Check for dead cell
			if(difficulty > Random.Range (0, 10)) {
				cell.SetColor (Color.grey);
				cell.SetText("0xFF");
				continue;
			}

			// Check for bonus cell
			if(difficulty / 2 > Random.Range (0, 10)
			   && bonusCount < difficulty) {
				cell.SetColor (Colors[Random.Range (0, Colors.Length)]);
				cell.SetText(GenerateAddress().Replace ("0x", "1x"));
				cell.IsBonusCell = true;
				bonusCount++;
				continue;
			}

			cell.SetColor(Colors[Random.Range (0, Colors.Length)]);
			cell.SetText (GenerateAddress());

			// Keep generating until we reach a unique address/color combo.
			while(uniqueCells.Contains (cell)) {
				cell.SetColor(Colors[Random.Range (0, Colors.Length)]);
				cell.SetText (GenerateAddress());
			}
			

			uniqueCells.Add (cell);
		}

		foreach (var cell in MemoryCells) {
			cell.gameObject.SetActive (false);
		}

		// Choose the keys random.
		_memoryKeys.Clear ();
		for (var i = 0; i < difficulty; i++) {
			var cell = _memoryCells.Values
				.Where(c => c.GetText() != "0xFF")
					.Where ( c => !c.IsBonusCell)
					.Where (c => !_memoryKeys.Any (k => k.GetText() == c.GetText() 
					                               && k.GetColor() == c.GetColor()))
				.OrderBy(_ => Guid.NewGuid()).First();
			_memoryKeys.Add (cell);

			MemoryCells[i].gameObject.SetActive(true);
			MemoryCells[i].SetText(cell.GetText());
			MemoryCells[i].SetColor (cell.GetColor());
		}

		SetEnabled (true);
	}

	public string GenerateAddress() {
		return string.Format ("0x{0}{1}", AddressCharacters [Random.Range (0, AddressCharacters.Length)],
		                      AddressCharacters [Random.Range (0, AddressCharacters.Length)]);
	}

	// Generates the cells for the board.
	void PopulateBoard() {
		for (var x = 0; x < GridWidth; x++) {
			for(var y = 0; y < GridHeight; y++) {
				var memoryCell = ((GameObject)Instantiate(MemoryCellPrefab)).GetComponent<RectTransform>();
				_memoryCells.Add (new Vector2(x, y), memoryCell.GetComponent<MemoryCellController>());
				memoryCell.SetParent(_memoryBoard.transform);
				memoryCell.anchoredPosition = new Vector2(50 + (x * 100), -15 + (y * -30));
				memoryCell.name = string.Format ("Memory Cell ({0},{1})", x, y);
			}
		}
	}

	public void CrashStack() {
		StartCoroutine (CrashCoroutine ());
	}

	IEnumerator CrashCoroutine() {
		for (var y = 0; y < GridHeight; y++) {
			for(var x = 0; x < GridWidth; x++) {
				var cell = _memoryCells[new Vector2(x, y)];
				cell.enabled = false;
				cell.SetCorrupted(true);
				cell.IsBonusCell = false;
				yield return new WaitForSeconds(0.02f);
			}
		}

		Messenger.Broadcast ("stackCrashComplete");
	}

	public void SetEnabled(bool enabled) {
		foreach(var cell in _memoryCells.Values) {
			cell.enabled = enabled;
		}
	}

	public List<MemoryCellController> GetMemoryKeys() {
		return _memoryKeys;
	}
}
