using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using System.Configuration;
using Microsoft.Bot.Connector;
using System.Threading;
using System.Linq;
using Autofac;
using Botflix.Services;
using Botflix.Model;
using Botflix.Extensions;

namespace Botflix.Dialogs
{
    [Serializable]
    public class BotflixDialog : BaseLuisDialog<object>
    {


        string qnaSubscriptionKey = ConfigurationManager.AppSettings["QnaSubscriptionKey"];
        string qnaKnowledgebaseId = ConfigurationManager.AppSettings["QnaKnowledgebaseId"];
        string userId;
        [NonSerialized]
        BotMovieTipsService botMovieTipsService;

        public BotflixDialog(string userId)
        {
            this.userId = userId;
            botMovieTipsService = new BotMovieTipsService();

        }

        [LuisIntent("Cumprimento")]
        public async Task Cumprimento(IDialogContext context, LuisResult result)
        {
            var qnaService = new QnAMakerService(new QnAMakerAttribute(qnaSubscriptionKey, qnaKnowledgebaseId, "Buguei aqui, pera!  ¯＼(º_o)/¯"));
            var qnaMaker = new QnAMakerDialog(qnaService);
            await qnaMaker.MessageReceivedAsync(context, Awaitable.FromItem(ArgumentoStatic.Argument));
        }

        [LuisIntent("Apresentacao")]
        public async Task Apresentacao(IDialogContext context, LuisResult result)
        {
            var qnaService = new QnAMakerService(new QnAMakerAttribute(qnaSubscriptionKey, qnaKnowledgebaseId, "Buguei aqui, pera!  ¯＼(º_o)/¯"));
            var qnaMaker = new QnAMakerDialog(qnaService);
            await qnaMaker.MessageReceivedAsync(context, Awaitable.FromItem(ArgumentoStatic.Argument));
        }

        [LuisIntent("Criticas")]
        public async Task Criticas(IDialogContext context, LuisResult result)
        {
            var category = result?.Entities?.Where(x => x.Type == "categoria").FirstOrDefault()?.Entity?.ToString();
            var message = context.MakeMessage();
            message.Text = result.Query;
            await context.Forward(new BaseQnaMakerDialog(category, userId), AfterQnaDialog, message, CancellationToken.None);
        }

        private async Task AfterQnaDialog(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var messageHandled = await result;
        }

        protected override Task MessageReceived(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
            return base.MessageReceived(context, item);
        }

        [LuisIntent("Favoritos")]
        public async Task Favoritos(IDialogContext context, LuisResult result)
        {
            var mediaId = result?.Entities?.Where(x => x.Type == "mediaId").FirstOrDefault()?.Entity?.ToString();

            if (!(String.IsNullOrEmpty(userId) && String.IsNullOrEmpty(mediaId)))
            {
                var favoriteMedia = new FavoriteMedia()
                {
                    IdUser = userId,
                    IdMedia = Convert.ToInt32(mediaId),
                    TransationDate = DateTime.Now
                };
                if (botMovieTipsService == null)
                    botMovieTipsService = new BotMovieTipsService();
                await botMovieTipsService.ConfigureAuthentication();
                await botMovieTipsService.SendFavoriteMedia(favoriteMedia);
            }
        }


        [LuisIntent("Sugestao")]
        public async Task Sugestao(IDialogContext context, LuisResult result)
        {
            var entity = result?.Entities?.Where(x => x.Type == "categoria").FirstOrDefault()?.Entity?.ToString();
            Category category = Category.anyway;

            if (!String.IsNullOrEmpty(entity))
                category = (Category)Enum.Parse(typeof(Category), entity);

            var movieService = new TheMovieDBService();

            if (botMovieTipsService == null)
                botMovieTipsService = new BotMovieTipsService();

            await botMovieTipsService.ConfigureAuthentication();
            var favoriteMedias = await botMovieTipsService.GetFavoriteMedias(userId);

            if (favoriteMedias.Count == 0)
            {
                await context.PostAsync($@"Aaah, ainda não te conheço o suficiente para conseguir te indicar um filme que vc goste!  ¯\_(⊙︿⊙)_/¯");
                await context.PostAsync($@"Você pode me pedir criticas sobre filmes / serie e falar quais você gosta pra eu aprender mais sobre você S2");
                return;
            }

            int random = new Random().Next(favoriteMedias.Count);
            var favoriteMediaChoiced = favoriteMedias[random];
            var mediasRecommendated = await movieService.GetRecommendation(favoriteMediaChoiced.IdMedia, EnumExtensions.GetDescription(category));

            var msg = context.MakeMessage();
            msg.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            foreach (var media in mediasRecommendated)
            {
                var card = new CardPersonalizedService().CreateCard(media);
                msg.Attachments.Add(card.ToAttachment());
            }

            await context.PostAsync(msg);


            //Implementar Form @Migg
            //FormDialog<Form.Sugestao> sugestionForm = new FormDialog<Form.Sugestao>(new Form.Sugestao(), Form.Sugestao.BuildForm, FormOptions.PromptInStart);
            //context.Call(sugestionForm, SugestaoFormCompleteAsync);
        }

        private async Task SugestaoFormCompleteAsync(IDialogContext context, IAwaitable<Form.Sugestao> result)
        {
            var sugest = await result;
            context.Wait(MessageReceived);
        }

        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Eita, não consegui entender a frase {result.Query}");
        }
    }
}