using Echo.Bot.Common;
using Echo.Bot.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Echo.Bot.Bots
{
	internal class SpellChecker
	{
		private readonly CsvRepository csv;
		private IList<string> historyMatches;
		private CostList costList;
		private const string anychar = ".*";

		public SpellChecker()
		{
			csv = new CsvRepository();
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

			for(int cost = 1; cost <= limit; cost++)
			{

				for (int pos = 0; pos < frage.Length - cost; pos++)
				{
					string regex = string.Format("{1}{0}{1}", frage.Remove(pos, cost).Insert(pos, anychar), anychar);
					string match = csv.Keys.FirstOrDefault(question => Regex.IsMatch(question.ToLower(), regex));

					if (!string.IsNullOrWhiteSpace(match) && !historyMatches.Contains(match))
						costList.regexes.TryAdd(cost, regex);
				}
			}

			return costList;
		}
	}
}
