using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextEditor
{
    public class TextEditor
    {
        /**
   * TextEditor implementation.
   * All methods that modify the document must return the current state of the text.
   */

        string recentActivity = "",
                finalString = "",
                insert = "",
                copiedString = "",
                pastedString = "",
                selectedString = "",
                docName = "";
        char deletedChar = ' ';

        int copyIndex = 0, cursorIndex = 0;

        Dictionary<string, string> docsDic = new Dictionary<string, string>();


        public string Append(string text)
        {
            recentActivity = "APPEND";
            insert = text;
            string temp = finalString;
            if(cursorIndex == 0)
			{
                finalString = text + finalString;
			}
			else
			{                
                if (!string.IsNullOrEmpty(selectedString))
                {
                    finalString = finalString.Replace(selectedString, "");
                    temp = finalString;
                }

                if (cursorIndex < temp.Length)
                {
                    finalString = temp.Substring(0, cursorIndex) + text + temp.Substring(cursorIndex);
                }else
				{
                    finalString = temp + text;
				}
            }

            cursorIndex = finalString.Length - 1;

            return finalString;
        }

        public void Move(int position)
        {
            if (position >= 0 && position < finalString.Length)
            {
                cursorIndex = position;
            }
            else
            {
                throw new Exception("The move index out of current document valid range!");
            }
        }

        public string Delete()
        {
            recentActivity = "DELETE";
            deletedChar = finalString.ElementAt(cursorIndex);
            string temp = finalString;
            finalString = temp.Substring(0, cursorIndex) + temp.Substring(cursorIndex + 1);

            return finalString;
        }

        public void Select(int leftPosition, int rightPosition)
        {
            if (leftPosition >= 0 && rightPosition < finalString.Length)
            {
                selectedString = finalString.Substring(leftPosition, rightPosition - leftPosition);
                cursorIndex = leftPosition;
            }
        }

        public string Cut()
        {
            recentActivity = "CUT";
            return selectedString;
        }

        public string Paste()
        {
            Append(selectedString);
            recentActivity = "PASTE";
            pastedString = selectedString;
            return finalString;
        }

        public string Undo()
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
            return finalString;
        }

        public string Redo()
        {
            // TODO: implement
            return null;
        }

        public void Create(string name)
        {
            // TODO: implement
        }

        public void Switch(string name)
        {
            // TODO: implement
        }
    }
 }
