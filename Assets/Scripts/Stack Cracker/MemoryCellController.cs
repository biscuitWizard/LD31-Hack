using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

[RequireComponent(typeof(RectTransform))]
public class MemoryCellController : MonoBehaviour {

	public Text UncorruptedCellText;
	public RectTransform CorruptedCell;
	public RectTransform UncorruptedCell;
	public bool IsMemoryKey;
	public bool IsBonusCell;

	private bool _corrupted;

	public void OnClick() {
		if(enabled && !IsMemoryKey)
			Messenger<MemoryCellController>.Broadcast ("memoryCellClicked", this);
	}

	public void SetColor(Color color) {
		UncorruptedCell.GetComponent<Image>().color = color;
	}

	public void SetText(string text) {
		UncorruptedCellText.text = text;
	}

	public string GetText() {
		return UncorruptedCellText.text;
	}

	public Color GetColor() {
		return UncorruptedCell.GetComponent<Image>().color;
	}

	public void SetCorrupted(bool corrupted) {
		_corrupted = corrupted;

		if (_corrupted) {
			CorruptedCell.gameObject.SetActive(true);
			UncorruptedCell.gameObject.SetActive(false);
		} else {
			CorruptedCell.gameObject.SetActive (false);
			UncorruptedCell.gameObject.SetActive(true);
		}

	}

	public bool GetCorrupted() {
		return _corrupted;
	}

	public override bool Equals(Object obj)
	{
		// If parameter is null return false.
		if (obj == null)
		{
			return false;
		}
		
		// If parameter cannot be cast to Point return false.
		var p = obj as MemoryCellController;
		if ((Object)p == null)
		{
			return false;
		}
		
		// Return true if the fields match:
		return GetColor () == p.GetColor()
				&& GetText () == p.GetText ();
	}

	public static bool operator ==(MemoryCellController a, MemoryCellController b)
	{
		// If both are null, or both are same instance, return true.
		if (Object.ReferenceEquals(a, b))
		{
			return true;
		}
		
		// If one is null, but not both, return false.
		if (((object)a == null) || ((object)b == null))
		{
			return false;
		}
		
		// Return true if the fields match:
		return a.GetColor () == b.GetColor()
			&& a.GetText () == b.GetText ();
	}

	public static bool operator !=(MemoryCellController a, MemoryCellController b)
	{
		// If both are null, or both are same instance, return true.
		if (Object.ReferenceEquals(a, b))
		{
			return true;
		}
		
		// If one is null, but not both, return false.
		if (((object)a == null) || ((object)b == null))
		{
			return false;
		}
		
		// Return true if the fields match:
		return a.GetColor () != b.GetColor()
			|| a.GetText () != b.GetText ();
	}
}
