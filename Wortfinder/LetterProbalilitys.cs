using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace Wortfinder
{
	// TODO: Rework: unuseful class "Letter"
	public class LettersGerman : ILetterProbability
	{
		public List<Letter> GetList()
		{
			List<Letter> list = new List<Letter> {
				new Letter('E', 0.1740m),
				new Letter('N', 0.0978m),
				new Letter('I', 0.0755m),
				new Letter('S', 0.0727m),
				new Letter('R', 0.0700m),
				new Letter('A', 0.0651m),
				new Letter('T', 0.0615m),
				new Letter('D', 0.0508m),
				new Letter('H', 0.0476m),
				new Letter('U', 0.0435m),
				new Letter('L', 0.0344m),
				new Letter('C', 0.0306m),
				new Letter('G', 0.0301m),
				new Letter('M', 0.0253m),
				new Letter('B', 0.0189m),
				new Letter('W', 0.0189m),
				new Letter('F', 0.0166m),
				new Letter('K', 0.0121m),
				new Letter('Z', 0.0131m),
				new Letter('P', 0.0089m),
				new Letter('V', 0.0077m),
				new Letter('ß', 0.0061m),
				new Letter('J', 0.0057m),
				new Letter('Y', 0.0040m),
				new Letter('X', 0.0046m),
				new Letter('Q', 0.0045m)};
			return list;
		}
	}
}
