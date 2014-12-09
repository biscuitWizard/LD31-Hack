using System;

public class Help : ICommand {
	public string Description { get { return "The help system"; } }

	public bool IsValid(string command) {
		return command.Equals ("help", StringComparison.CurrentCultureIgnoreCase);
	}

	public string Do(string[] args) {
		return string.Empty;
	}
}
