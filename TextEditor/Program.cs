using System;

namespace TextEditor
{
	public class Program
	{
		static void Main(string[] args)
		{

			TextEditor te = new TextEditor();

			//Console.WriteLine(te.Append("Hello! world!"));
			//te.Move(5);
			//Console.WriteLine(te.Delete());
			//Console.WriteLine(te.Append(","));

			Console.WriteLine(te.Append("Hello, world!"));
			te.Select(5, 12);
			Console.WriteLine(te.Cut());
			te.Move(4);
			Console.WriteLine(te.Paste());
			Console.WriteLine(te.Undo());
			Console.WriteLine(te.Paste());



		}
	}
}
