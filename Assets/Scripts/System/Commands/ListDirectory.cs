using System;

public class ListDirectory : ICommand {
	public string Description { get { return "list directory contents"; } }

	public bool IsValid(string command) {
		return command.Equals ("ls", StringComparison.CurrentCultureIgnoreCase)
			|| command.Equals ("cd", StringComparison.CurrentCultureIgnoreCase);
	}

	public string Do(string[] args) {

		return string.Empty;
	}
}
