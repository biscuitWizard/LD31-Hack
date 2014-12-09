using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TextAsset))]
public class MemoryStackTextGenerator : MonoBehaviour {

	// Max lines of text to keep in memory
	public int MaxLines = 20;
	// Max delay between dumps in seconds.
	public double MaxDelay = 0.4;
	// Min delay between dumps in seconds.
	public double MinDelay = 0.02;
	// Max lines per dump
	public int MaxDumpLines = 12;
	// Min lines per dump
	public int MinDumpLines = 5;
	// Min Memory Line Length
	public int MinAddressLength = 4;
	// Max Memory Line Length
	public int MaxAddressLength = 15;	

	public char[] AddressSpaceCharacters = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'A', 'B', 'C', 'D', 'E', 'F' };
	public string AddressCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789{}[]!@#$%^&*()";

	public List<string> _buffer = new List<string>();

	private Text _text;
	private bool _dumping;

	// Use this for initialization
	void Start () {
		_text = GetComponent<Text> ();

		DumpMemory ();
	}

	void Update() {
		if (_dumping)
			return;

		var delay = Random.Range ((float)MinDelay, (float)MaxDelay);

		StartCoroutine (DumpDelay (delay));
	}

	IEnumerator DumpDelay(float delay) {
		_dumping = true;
		yield return new WaitForSeconds(delay);
		DumpMemory ();

		_dumping = false;
	}

	void DumpMemory() {

		var linesToDump = Random.Range (MinDumpLines, MaxDumpLines);

		for (var i = 0; i < linesToDump; i++) {
			var memoryLine = string.Format ("<color=#00ff00ff>{0} {1}\t{2} {3}</color>",
			                                GenerateAddressSpace(),
			                                GenerateMemoryString(),
			                                GenerateAddressSpace(),
			                                GenerateMemoryString());
			_buffer.Add (memoryLine);
		}
		_buffer.Add (string.Empty);

		RefreshBuffer ();

		if (MaxLines < _buffer.Count) {
			_buffer.RemoveRange (0, _buffer.Count - MaxLines);
		}
	}

	string GenerateAddressSpace() {
		return string.Format ("0x{0}{1}{2}{3}", AddressSpaceCharacters [Random.Range (0, AddressSpaceCharacters.Length)],
		                      AddressSpaceCharacters [Random.Range (0, AddressSpaceCharacters.Length)],
		                      AddressSpaceCharacters [Random.Range (0, AddressSpaceCharacters.Length)],
		                      AddressSpaceCharacters [Random.Range (0, AddressSpaceCharacters.Length)]);
	}

	string GenerateMemoryString() {
		return new string(
			Enumerable.Repeat(AddressCharacters, Random.Range (MinAddressLength, MaxAddressLength))
			.Select(s => s[Random.Range (0, s.Length)])
			.ToArray());
	}

	void RefreshBuffer() {
		var sb = new StringBuilder ();

		foreach (var line in _buffer) {
			sb.AppendLine(line);
		}

		_text.text = sb.ToString ();
	}

	public void SetCrashed(bool crashed) {
		StopAllCoroutines ();

		_dumping = crashed;
	}
}
