using System.Collections.Generic;
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
		var replyText = $"Something more elaborative: always same message";
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
