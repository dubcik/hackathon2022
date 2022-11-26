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
		var response_message = new Parser.ParseQuery().ParseResponse(turnContext.Activity.Text);

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
		" - Workforce Department(admin / IT)" + System.Environment.NewLine +
		"etc. (manager, learning, employee, technical help, ";
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
