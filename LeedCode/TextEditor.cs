using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningSumArray
{
	public class TextEditor
	{
        /*
         * Dropbox codesignal OA
         * append
         * move (cursor to that position)
         * backspace
         * select
         * copy
         * paste
         * undo
         * redo
         * createDocument
         * switchDocument
         * 
         */

        public static string getMyString(string[] myString)
        {
            string finalString = "",
                    insert = "",
                    copiedString = "",
                    pastedString = "",
                    docName = "";
            char deletedChar = ' ';
            int copyIndex = 0,
                cursorIndex = 0;
            string recentActivity = "";

            Dictionary<string, string> docDit = new Dictionary<string, string>();


            foreach (string operation in myString)
            {
                if (operation.Contains("INSERT") || operation.Contains("APPEND"))
                {
                    recentActivity = "INSERT";
                    insert = operation.Split(new[] { "INSERT " }, StringSplitOptions.None)[1].Trim();
                    finalString += insert;
                }
                else if (operation.Contains("DELETE") || operation.Contains("BACKSPACE"))
                {
                    recentActivity = "DELETE";
                    deletedChar = finalString.ElementAt(finalString.Length - 1);
                    finalString = finalString.Substring(0, finalString.Length - 1);
                }
                else if (operation.Contains("COPY"))
                {
                    copyIndex = Int16.Parse(operation.Split(new[] { "COPY " }, StringSplitOptions.None)[1].Trim());
                    copiedString = finalString.Substring(copyIndex);
                }
                else if (operation.Contains("PASTE"))
                {
                    recentActivity = "PASTE";
                    pastedString = copiedString;
                    finalString += copiedString;
                }
                else if (operation.Contains("MOVE"))
                {
                    cursorIndex = Int16.Parse(operation.Split(new[] { "MOVE " }, StringSplitOptions.None)[1].Trim());
                    //make sure cursor index in the valid range
                    if (cursorIndex >= finalString.Length)
                        cursorIndex = finalString.Length - 1;
                    else
                        throw new Exception("");

                }
                else if (operation.Contains("UNDO"))
                {
                    if (recentActivity.Equals("INSERT", StringComparison.InvariantCultureIgnoreCase))
                    {
                        finalString = finalString.Substring(0, finalString.Length - insert.Length);
                    }
                    else if (recentActivity.Equals("DELETE", StringComparison.InvariantCultureIgnoreCase))
                    {
                        finalString += Char.ToString(deletedChar);
                    }
                    else if (recentActivity.Equals("PASTE", StringComparison.InvariantCultureIgnoreCase))
                    {
                        finalString += finalString.Split(new[] { pastedString }, StringSplitOptions.None)[0];
                    }
                }
                else if (operation.Contains("REDO"))
                {
                    if (recentActivity.Equals("INSERT", StringComparison.InvariantCultureIgnoreCase))
                    {
                        finalString = finalString + insert;
                    }
                    else if (recentActivity.Equals("DELETE", StringComparison.InvariantCultureIgnoreCase))
                    {
                        deletedChar = finalString.ElementAt(finalString.Length - 1);
                        finalString = finalString.Substring(0, finalString.Length - 1);
                    }
                    else if (recentActivity.Equals("PASTE", StringComparison.InvariantCultureIgnoreCase))
                    {
                        pastedString = copiedString;
                        finalString += copiedString;
                    }
                }
                else if (operation.Contains("CREATEDOCUMENT"))
                {
                    docName = operation.Split(new[] { "CREATEDOCUMENT " }, StringSplitOptions.None)[1].Trim();
                    if(docDit.ContainsKey(docName))
					{
                        finalString = docDit[docName];
					}
					else
					{
                        docDit.Add(docName, "");
					}
                }
                else if (operation.Contains("SWITCHDOCUMENT"))
                {
                    docName = operation.Split(new[] { "SWITCHDOCUMENT " }, StringSplitOptions.None)[1].Trim();
                    if (docDit.ContainsKey(docName))
                    {
                        finalString = docDit[docName];
                    }
                }

                //Save current operation into file dictionary
                if(!operation.Contains("CREATEDOCUMENT") && operation.Contains("SWITCHDOCUMENT") && operation.Contains("MOVE"))
				{
                    if(docDit.ContainsKey(docName))
					{
                        docDit[docName] = finalString;
					}
				}
            }

            return finalString;
        }


        public String performEditorActions(String[][] actions)
        {
            Stack<String> result = new Stack<String>();
            Stack<String[]> undo = new Stack<String[]>();
            Stack<String[]> redo = new Stack<String[]>();
            foreach (String[] action in actions)
            {
                switch (action[0])
                {
                    case "INSERT":
                        result.Push(action[1]);
                        undo.Push(action);
                        break;
                    case "DELETE":
                        undo.Push(new String[] { action[0], result.Pop() });
                        break;
                    case "UNDO":
                        string[] actionundo = undo.Pop();
                        if (action[0].Equals("INSERT"))
                        {
                            result.Pop();
                        }
                        else
                        {
                            result.Push(action[1]);
                        }
                        redo.Push(actionundo);
                        break;
                    case "REDO":
                        if (redo.Count != 0)
                        {
                            string[] actionredo = redo.Pop();
                            if (action[0].Equals("INSERT"))
                            {
                                result.Push(action[1]);
                                undo.Push(action);
                            }
                            else
                            {
                                action[1] = result.Pop();
                                undo.Push(actionredo);
                            }
                        }
                        break;
                }
            }

            StringBuilder sb = new StringBuilder();
            while (result.Count != 0)
            {
                sb.Append(result.Pop());
            }

            return sb.ToString();
        }
    }
}
