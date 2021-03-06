﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Botflix.Dialogs;
using Botflix.Services;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Botflix
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                ArgumentoStatic.Argument = activity;
                await Conversation.SendAsync(activity, () => new BotflixDialog(activity.From.Id));
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                IntroductBotForNewUsers(message);
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

        private void IntroductBotForNewUsers(Activity activity)
        {
            if (activity.MembersAdded != null && activity.MembersAdded.Any())
            {
                var botId = activity.Recipient.Id;
                var test = activity.MembersAdded.Select(m => m).Where(m => m.Id != botId).ToList();

                if (activity.MembersAdded.Any(m => m.Id != botId))
                {
                    var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                    var announcer = new AnnouncerService();
                    var reply = activity.CreateReply();

                    reply.Attachments.Add(announcer.GenerateIntroduction().ToAttachment());
                    connector.Conversations.ReplyToActivityAsync(reply);
                }
            }
        }
    }
}