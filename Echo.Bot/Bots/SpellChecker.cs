using Echo.Bot.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Echo.Bot.Bots
{
	internal class SpellChecker
	{
		private readonly IDictionary<string, string> data;
		private IList<string> historyMatches;
		private IList<CostWord> costList;
		private const string anychar = ".*";

		public SpellChecker(IDictionary<string, string> externalData)
		{
			data = externalData;
			costList = new List<CostWord>();
			historyMatches = new List<string>();
		}

		internal IList<CostWord> GetVariants(string sentance)
		{
			int limit = sentance.Length - 1;
			string frage = sentance.ToLower();

			historyMatches.Clear();

			for (int cost = 0; cost <= limit; cost++)
			{
				for (int pos = 0; pos < frage.Length - cost; pos++)
				{
					string regex = string.Format("{0}{1}{0}", anychar, sentance.ToLower().Remove(pos, cost).Insert(pos, anychar));
					string match = data.Keys.FirstOrDefault(question => Regex.IsMatch(question.ToLower(), regex));

					if (!string.IsNullOrWhiteSpace(match) && !historyMatches.Contains(match))
					{
						var costWord = new CostWord()
						{
							question = match,
							regex = regex,
							cost = cost
						};

						costList.Add(costWord);
						historyMatches.Add(match);
					}
				}
			}

			return costList;
		}
	}
}
