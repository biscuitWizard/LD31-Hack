using System;

public class ChangeDirectory : ICommand {
	public string Description { get { return "change current working directory"; } }

	public bool IsValid(string command) {
		return string.Equals (command, "ls", StringComparison.CurrentCultureIgnoreCase)
			|| string.Equals (command, "cd", StringComparison.CurrentCultureIgnoreCase);
	}

	public string Do(string[] args) {
		return string.Empty;
	}
}
