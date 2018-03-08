using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Botflix.Model;
using Botflix.Services;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Botflix.Dialogs
{
    [Serializable]
    public class BaseQnaMakerDialog : QnAMakerDialog
    {
        private Category category;
        public BaseQnaMakerDialog(string category) : base(GetNewService())
        {
            switch (category)
            {
                case "serie":
                    this.category = Category.tv;
                    break;
                case "filme":
                    this.category = Category.movie;
                    break;
                default:
                    this.category = Category.anyway;
                    break;
            }
        }

        private static IQnAService[] GetNewService()
        {
            var subscriptionKey = ConfigurationManager.AppSettings.Get("QnaSubscriptionKey");
            var knowledgebaseid = ConfigurationManager.AppSettings.Get("QnaKnowledgebaseId");
            var defaultMessage = "Buguei aqui, pera!  ¯＼(º_o)/¯";
            var qnaModel = new QnAMakerAttribute(subscriptionKey, knowledgebaseid, defaultMessage);
            return new IQnAService[] { new QnAMakerService(qnaModel) };
        }

        protected override async Task DefaultWaitNextMessageAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {

            var answer = result.Answers.FirstOrDefault();
            if (answer != null)
            {
                var question = answer.Questions.FirstOrDefault();
                TheMovieDBService movieService = new TheMovieDBService();
                var media = await movieService.GetMediaByName(question.ToString(), category);

                if (media != null)
                {
                    var card = CreateCard(media);
                    var msg = context.MakeMessage();
                    msg.Attachments.Add(card.ToAttachment());

                    await context.PostAsync(msg);

                    context.Done<string>("Oi");

                }
            }
        }

        private HeroCard CreateCard(Media media)
        {
            var heroCard = new HeroCard
            {
                Title = media.title,
                Subtitle = media.overview,
                Images = new List<CardImage>
                {
                    new CardImage(media.Image,
                    media.title)
                },
                Buttons = new List<CardAction>
                {
                    new CardAction
                    {
                        Text = "Gostei",
                        DisplayText = "Gostei",
                        Title = "Gostei",
                        Type = ActionTypes.PostBack,
                        Value = "Gostei"

                    }

                }
            };

            return heroCard;
        }
    }
}