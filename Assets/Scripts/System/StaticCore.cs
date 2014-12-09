using System;
using System.Collections.Generic;

// The static core is a virtual/psuedo operating system manager. Each 'virtual system' has a core
// that simulates its environment.
public class StaticCore {
	private readonly List<ICommand> _commands = new List<ICommand>();
	private readonly FileSystem _fileSystem;

	public StaticCore() {

	}

	public void AddCommand(ICommand command) {

	}

	public void AddDirectory(string directoryName, Directory root) {

	}

	public Directory GetDirectory(string path) {
		return null;
	}

	public File GetFile(string path) {
		return null;
	}
}
