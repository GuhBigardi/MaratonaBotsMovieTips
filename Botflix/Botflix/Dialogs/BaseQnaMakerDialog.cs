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
        public BaseQnaMakerDialog() : base(GetNewService()) { }

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
            var question = answer.Questions.FirstOrDefault();
            TheMovieDBService movieService = new TheMovieDBService();
            var movie = await movieService.GetMovieByName(question.ToString());
            if (movie != null)
            {
                var card = CreateCard(movie);
                var msg = context.MakeMessage();
                msg.Attachments.Add(card.ToAttachment());

                await context.PostAsync(msg);
            }
        }

        private HeroCard CreateCard(Movie movie)
        {
            var heroCard = new HeroCard
            {
                Title = movie.title,
                Subtitle = movie.overview,
                Images = new List<CardImage>
                {
                    new CardImage(movie.poster_path,
                    movie.title)
                },
                Buttons = new List<CardAction>
                {
                    new CardAction
                    {
                        Text = "Gostei",
                        DisplayText = "Gostei",
                        Title = "Gostei",
                        Type = ActionTypes.PostBack,
                        Value = true

                    }

                }
            };

            return heroCard;
        }
    }
}