using Echo.Bot.Repository;
using Microsoft.Bot.Builder;
using System.Text.RegularExpressions;
using System;
using Echo.Bot.Bots;
using System.Linq;

namespace Echo.Bot.Parser
{
	public class ParseQuery
	{
		public string ParseResponse(string textToParse)
		{
			string patern_for_manager = @"(?#\s*\W*\s*\w*\s*)(?:Manager)|(?:manager)(?#\s*\W*\s*\w*\s*)";
			string patern_for_holiday = @"(?#\s*\W*\s*\w*\s*)(?:Holiday)|(?:holiday)|(?:holyday)|(?:Holyday)(?#\s*\W*\s*\w*\s*)";
			string patern_for_learning = @"(?#\s*\W*\s*\w*\s*)(?:L*l*earning)|(?:learn)(?#\s*\W*\s*\w*\s*)";
			string patern_for_employee = @"(?#\s*\W*\s*\w*\s*)(?:employee)|(?:employer)|(?:colleque)|(?:member)|(?:worker)(?#\s*\W*\s*\w*\s*)";
			string patern_for_IT = @"(?#\s*\W*\s*\w*\s*)(?:IT)|(?:It)|(?:iT)|(?:technical\s*help)(?#\s*\W*\s*\w*\s*)";
			string patern_first_response = @"(?#\s*\W*\s*\w*\s*)(?:General\s+question|General|general)|(?:Benefits\s+and\s+certifications|benefits\s+and\s+certifications|Benefits\s+and\s+Certifications)|(?:Finance\s+department|finance\s+department|finance\s+Department)|(?:Talent\s+department|talent\s+department)|(?:Workforce\s+Department|workforce\s+Department)(?#\s*\W*\s*\w*\s*)";
			string patern_Local_Partner = @"(?#\s*\W*\s*\w*\s*)(?:Local\s*\w*\s*Partner|local\s*\w*\s*Partner|Local\s*\w*\s*partner|local\s*\w*\s*partner)(?#\s*\W*\s*\w*\s*)";
			string patern_Certifications = @"(?#\s*\W*\s*\w*\s*)(?:certifications(?>\s*\w*\s*)*cover|Certifications(?>\s*\w*\s*)*cover|Certifications(?>\s*\w*\s*)*Cover)(?#\s*\W*\s*\w*\s*)";
			string patern_Business_Trip = @"(?#\s*\W*\s*\w*\s*)(?:business\s+trip|Business\s+trip|Business\s+Trip|business\s+Trip)(?#\s*\W*\s*\w*\s*)";
			string patern_Current_Projects = @"(?#\s*\W*\s*\w*\s*)(?:current\s+projects|Current\s+projects|Current\s+Projects|current\s+Projects)(?#\s*\W*\s*\w*\s*)";
			CsvRepository csv = new CsvRepository();
			string response_message = string.Empty;
			string key_For_Csv = string.Empty;

			try
			{
				if (Regex.IsMatch(textToParse, patern_first_response))
				{
					switch (Regex.Match(textToParse, patern_first_response, RegexOptions.Compiled).ToString().ToLower())
					{
						case "general question" or "general":
							key_For_Csv = "general question";
							response_message = csv[key_For_Csv];
							break;
						case "benefits and certifications" or "benefits" or "certifications":
							key_For_Csv = "benefits and certifications";
							response_message = csv[key_For_Csv];
							break;
						case "finance department" or "finance":
							key_For_Csv = "finance department";
							response_message = csv[key_For_Csv];
							break;
						case "talent department" or "talent":
							key_For_Csv = "talent department";
							response_message = csv[key_For_Csv];
							break;
						case "workforce department" or "workforce":
							key_For_Csv = "workforce department";
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else
					throw new Exception();
			}
			catch
			{
				if (Regex.IsMatch(textToParse, patern_for_manager))
				{
					key_For_Csv = "manager";
					switch (Regex.Match(textToParse, patern_for_manager, RegexOptions.Compiled).ToString().ToLower())
					{
						case "manager":
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else if (Regex.IsMatch(textToParse, patern_for_holiday))
				{
					key_For_Csv = "holiday";
					switch (Regex.Match(textToParse, patern_for_holiday, RegexOptions.Compiled).ToString().ToLower())
					{
						case "holiday" or "holyday":
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else if (Regex.IsMatch(textToParse, patern_for_learning))
				{
					key_For_Csv = "learning";
					switch (Regex.Match(textToParse, patern_for_learning, RegexOptions.Compiled).ToString().ToLower())
					{
						case "learn" or "learning":
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else if (Regex.IsMatch(textToParse, patern_for_employee))
				{
					key_For_Csv = "employee";
					switch (Regex.Match(textToParse, patern_for_employee, RegexOptions.Compiled).ToString().ToLower())
					{
						case "employee" or "employer" or "colleque" or "member" or "worker":
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else if (Regex.IsMatch(textToParse, patern_for_IT))
				{
					key_For_Csv = "IT";
					switch (Regex.Match(textToParse, patern_for_IT, RegexOptions.Compiled).ToString().ToLower())
					{
						case "it" or "technical help":
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else if (Regex.IsMatch(textToParse, patern_Local_Partner))
				{
					
					key_For_Csv = "Local People Partner";
					switch (Regex.Match(textToParse, patern_Local_Partner, RegexOptions.Compiled).ToString().ToLower())
					{
						case "local people partner":
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else if (Regex.IsMatch(textToParse, patern_Certifications))
				{
					key_For_Csv = "Certifications cover";
					switch (Regex.Match(textToParse, patern_Certifications, RegexOptions.Compiled).ToString().ToLower())
					{
						case "certifications does amadris cover":
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else if (Regex.IsMatch(textToParse, patern_Business_Trip))
				{
					key_For_Csv = "Business trip";
					switch (Regex.Match(textToParse, patern_Business_Trip, RegexOptions.Compiled).ToString().ToLower())
					{
						case "business trip":
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else if (Regex.IsMatch(textToParse, patern_Current_Projects))
				{
					key_For_Csv = "Current projects";
					switch (Regex.Match(textToParse, patern_Current_Projects, RegexOptions.Compiled).ToString().ToLower())
					{
						case "current projects":
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else
				{
					key_For_Csv = "default";
					response_message = csv[key_For_Csv];

					var variants = new SpellChecker(new CsvRepository()).GetVariants(textToParse);

					if (variants.Count > 0)
					{
						var averageCost = 0;

						foreach (var variant in variants)
						{
							averageCost += variant.cost;
						}

						averageCost /= variants.Count;

						response_message += String.Format("{0}Similar questions:{0}", Environment.NewLine);

						var alternatives = variants.Where(variant => variant.cost <= averageCost).Select(word => word.question);

						response_message += string.Join(Environment.NewLine, alternatives);
					}
				}
			}

			return response_message;
		}
	}
}
