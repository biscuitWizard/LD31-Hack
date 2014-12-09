using UnityEngine;
using System.Collections;

public interface ICommand {
	string Description { get; }

	// Return if the command matches what was typed.
	bool IsValid(string command);

	// Execute the command
	string Do(string[] args);
}
