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
		var patern_for_manager = @"(?#\s*\W*\s*\w*\s*)(?:[Manager]{7})|(?:[manager]{7})(?#\s*\W*\s*\w*\s*)";
		switch (Regex.Match(turnContext.Activity.Text, patern_for_manager, RegexOptions.Compiled).ToString())
		{
			case "manager":
				response_message = "Your Line Manager is " + "https://projectplan.amdaris.com/profile";
				break;
			case "Manager":
				response_message = "Your Line Manager is " + "https://projectplan.amdaris.com/profile";
				break;

				}
		string replyText = $"Response: {response_message}";
		Debug.WriteLine(replyText);
		var response = await turnContext.SendActivityAsync(
			MessageFactory.Text(replyText, replyText),
			cancellationToken);
	}

	protected override async Task OnMembersAddedAsync(
		IList<ChannelAccount> membersAdded,
		ITurnContext<IConversationUpdateActivity> turnContext,
		CancellationToken cancellationToken)
	{
		const string welcomeText = "Hello and welcome!";
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
