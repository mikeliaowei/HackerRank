using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningSumArray
{
	/*
	 * 
		Generate a random sentence of length N from a given text. Logic:

		Randomly select a word
		The next word will be the subsequent contagious word from the previously selected word. If the next word occurs multiple times in the sentence, select randomly.
		Continue until length fulfill required length
		Special case: If the chosen word has no next word (last word in the sentence, use first word as the "next word" - assume circular)

		Example
		text = "hello this is a flexport interview and this is a hello and that was cool"
		n = 4

		say first word randomly selected is "flexport"
		next word after "flexport" in the text is "interview". "interview" only occurs once.
		next word after "interview" in the text is "and". "and" occurs twice at index 6 & 11. Randomly select one of the indices. Say we select the one at index 6
		next word after "and" at index 6 is "this". "this" occurs at twice at index 1 & 7. Randomly selected at 7.
		Output: "flexport interview and this"

	 */
	public class RandomSentenceGeneration
	{
	}
}
