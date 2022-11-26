using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Echo.Bot.Repository;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Echo.Bot.Bots;

public class EchoBot : ActivityHandler
{
	protected override async Task OnMessageActivityAsync(
		ITurnContext<IMessageActivity> turnContext,
		CancellationToken cancellationToken)
	{
		var response_message = "";
		var patern_for_manager = @"(?#\s*\W*\s*\w*\s*)(?:Manager)|(?:manager)(?#\s*\W*\s*\w*\s*)";
		var patern_for_holiday = @"(?#\s*\W*\s*\w*\s*)(?:Holiday)|(?:holiday)|(?:holyday)|(?:Holyday)(?#\s*\W*\s*\w*\s*)";
		var patern_for_learning = @"(?#\s*\W*\s*\w*\s*)(?:L*l*earning)|(?:learn)(?#\s*\W*\s*\w*\s*)";
		var patern_for_employee = @"(?#\s*\W*\s*\w*\s*)(?:employee)|(?:employer)|(?:colleque)|(?:member)|(?:worker)(?#\s*\W*\s*\w*\s*)";
		var patern_for_IT = @"(?#\s*\W*\s*\w*\s*)(?:IT)|(?:It)|(?:iT)|(?:technical\s*help)(?#\s*\W*\s*\w*\s*)";
		var patern_first_response = @"(?#\s*\W*\s*\w*\s*)(?:General\s+question|General|general)|(?:Benefits\s+and\s+certifications|Benefits|benefits|certifications|Certifications|certification|Certification)|(?:Finance\s+department|Finance|finance\s+department|finance)|(?:Talent\s+department|talent\s+department|Talent|talent|Talents|talents)|(?:Workforce\s+Department|workforce\s+Department|Workforce|workforce)(?#\s*\W*\s*\w*\s*)";
		CsvRepository csv = new CsvRepository();
		var key_For_Csv = string.Empty;
			try
			{
				if (Regex.IsMatch(turnContext.Activity.Text, patern_first_response))
				{

				}
				else
					throw new Exception();
			}
			catch
			{
				if (Regex.IsMatch(turnContext.Activity.Text, patern_for_manager))
				{
					//advance profile
					key_For_Csv = "manager";
					switch (Regex.Match(turnContext.Activity.Text, patern_for_manager, RegexOptions.Compiled).ToString().ToLower())
					{
						case "manager":
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else if (Regex.IsMatch(turnContext.Activity.Text, patern_for_holiday))
				{
					//hi-bob my time off
					key_For_Csv = "holiday";
					switch (Regex.Match(turnContext.Activity.Text, patern_for_holiday, RegexOptions.Compiled).ToString().ToLower())
					{
						case "holiday" or "holyday":
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else if (Regex.IsMatch(turnContext.Activity.Text, patern_for_learning))
				{
					key_For_Csv = "learning";
					switch (Regex.Match(turnContext.Activity.Text, patern_for_learning, RegexOptions.Compiled).ToString().ToLower())
					{
						case "learn" or "learning":
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else if (Regex.IsMatch(turnContext.Activity.Text, patern_for_employee))
				{
					key_For_Csv = "employee";
					switch (Regex.Match(turnContext.Activity.Text, patern_for_employee, RegexOptions.Compiled).ToString().ToLower())
					{
						case "employee" or "employer" or "colleque" or "member" or "worker":
							response_message = csv[key_For_Csv];
							break;
					}
				}
				else if (Regex.IsMatch(turnContext.Activity.Text, patern_for_IT))
				{
					key_For_Csv = "IT";
					switch (Regex.Match(turnContext.Activity.Text, patern_for_IT, RegexOptions.Compiled).ToString().ToLower())
					{
						case "it" or "technical help":
							response_message = csv[key_For_Csv];
							break;
					}
				}
			}
		
		Debug.WriteLine(response_message);
		var response = await turnContext.SendActivityAsync(
			MessageFactory.Text(response_message, response_message),
			cancellationToken);
	}

	protected override async Task OnMembersAddedAsync(
		IList<ChannelAccount> membersAdded,
		ITurnContext<IConversationUpdateActivity> turnContext,
		CancellationToken cancellationToken)
	{
		string welcomeText = "Hello and welcome!" + System.Environment.NewLine +
		"Select subject for your question" + System.Environment.NewLine +
		" - General question" + System.Environment.NewLine +
		" - Benefits and certifications" + System.Environment.NewLine +
		" - Finance department" + System.Environment.NewLine +
		" - Talent department" + System.Environment.NewLine +
		" - Workforce Department(admin / IT)";
		foreach (var member in membersAdded)
		{
			if (member.Id != turnContext.Activity.Recipient.Id)
			{
				await turnContext.SendActivityAsync(
					MessageFactory.Text(welcomeText, welcomeText),
					cancellationToken);
			}
		}
	}
}
