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
		private CostList costList;
		private const string anychar = ".*";

		public SpellChecker(IDictionary<string, string> externalData)
		{
			data = externalData;
			costList = new();
			historyMatches = new List<string>();
		}

		internal CostList GetVariants(string sentance)
		{
			int limit = sentance.Length - 1;
			string frage = sentance.ToLower();

			costList.question = frage;
			costList.regexes = new();
			historyMatches.Clear();

			for (int cost = 0; cost <= limit; cost++)
			{

				for (int pos = 0; pos < frage.Length - cost; pos++)
				{
					string regex = string.Format("{0}{1}{0}", anychar, sentance.ToLower().Remove(pos, cost).Insert(pos, anychar));
					string match = data.Keys.FirstOrDefault(question => Regex.IsMatch(question.ToLower(), regex));

					if (!string.IsNullOrWhiteSpace(match) && !historyMatches.Contains(match))
					{
						costList.regexes.TryAdd(cost, regex);
						historyMatches.Add(match);
					}
				}
			}

			return costList;
		}
	}
}
