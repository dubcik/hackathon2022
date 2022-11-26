using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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
					var responseanswer = "Your Line Manager is https://projectplan.amdaris.com/profile";
					switch (Regex.Match(turnContext.Activity.Text, patern_for_manager, RegexOptions.Compiled).ToString())
					{

						case "manager":
							response_message = responseanswer;
							break;
						case "Manager":
							response_message = responseanswer;
							break;
					}
				}
				else if (Regex.IsMatch(turnContext.Activity.Text, patern_for_holiday))
				{
					//hi-bob my time off
					var responseanswer = "You can check holiday here: https://app.hibob.com/time-off/my-time-off";
					switch (Regex.Match(turnContext.Activity.Text, patern_for_holiday, RegexOptions.Compiled).ToString())
					{
						case "holiday":
							response_message = responseanswer;
							break;
						case "Holiday":
							response_message = responseanswer;
							break;
						case "holyday":
							response_message = responseanswer;
							break;
						case "Holyday":
							response_message = responseanswer;
							break;
					}
				}
				else if (Regex.IsMatch(turnContext.Activity.Text, patern_for_learning))
				{
					var responseanswer = "You can find information about your question here: " + System.Environment.NewLine +
								//sharepoint learning page
								"https://amdaris.sharepoint.com/sites/AmdarisLearning  " + System.Environment.NewLine +
								"or read some Book here: " + System.Environment.NewLine +
								//sharepoint Ebook
								"https://amdaris.sharepoint.com/Employee%20Documents/Forms/AllItems.aspx?id=%2FEmployee%20Documents%2FEBooks&viewid=6a0b0218%2Dbb16%2D4d10%2D952b%2D906db233ae10 " + System.Environment.NewLine +
								"or find other documents here: " + System.Environment.NewLine +
								//sharepoint search "learning"
								"https://amdaris.sharepoint.com/_layouts/15/osssearchresults.aspx?u=https%3A%2F%2Famdaris%2Esharepoint%2Ecom&k=learning&ql=2057";
					switch (Regex.Match(turnContext.Activity.Text, patern_for_learning, RegexOptions.Compiled).ToString())
					{
						case "learn":
							response_message = responseanswer;
							break;
						case "learning":
							response_message = responseanswer;
							break;
						case "Learning":
							response_message = responseanswer;
							break;
					}
				}
				else if (Regex.IsMatch(turnContext.Activity.Text, patern_for_employee))
				{
					var responseanswer = "You can find information about your local department here:" + System.Environment.NewLine +
						//hi-bob dicertory
						"https://app.hibob.com/people/directory" + System.Environment.NewLine +
						"or you can check your organisation levels here:" + System.Environment.NewLine +
						//hi-bob org chart
						"https://app.hibob.com/employeeDirectory/company-org-chart";
					switch (Regex.Match(turnContext.Activity.Text, patern_for_employee, RegexOptions.Compiled).ToString())
					{
						case "employee":
							response_message = responseanswer;
							break;
						case "employer":
							response_message = responseanswer;
							break;
						case "colleque":
							response_message = responseanswer;
							break;
						case "member":
							response_message = responseanswer;
							break;
						case "worker":
							response_message = responseanswer;
							break;
					}
				}
				else if (Regex.IsMatch(turnContext.Activity.Text, patern_for_IT))
				{
					var responseanswer = "You can make new ticket here:" + System.Environment.NewLine +
						//IT ticket
						"https://amdaris.atlassian.net/servicedesk/customer/portal/2";
					switch (Regex.Match(turnContext.Activity.Text, patern_for_IT, RegexOptions.Compiled).ToString())
					{
						case "IT":
							response_message = responseanswer;
							break;
						case "it":
							response_message = responseanswer;
							break;
						case "It":
							response_message = responseanswer;
							break;
						case "iT":
							response_message = responseanswer;
							break;
						case "technical help":
							response_message = responseanswer;
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
