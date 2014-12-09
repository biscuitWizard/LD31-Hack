public class ExecuteFile : ICommand {

	public string Description { get { return ""; } }

	private readonly FileSystem _files;

	public ExecuteFile(FileSystem fileSystem) {
		_files = fileSystem;
	}

	public bool IsValid(string command) {
		// Get the files in the current directory and check their names.
		return false;
	}

	public string Do(string[] args) {
		return string.Empty;
	}
}