using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using System.Configuration;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System.Threading;
using System.Linq;
using Autofac;

namespace Botflix.Dialogs
{
    [Serializable]
    public class BotflixDialog : BaseLuisDialog<object>
    {

        string qnaSubscriptionKey = ConfigurationManager.AppSettings["QnaSubscriptionKey"];
        string qnaKnowledgebaseId = ConfigurationManager.AppSettings["QnaKnowledgebaseId"];


        [LuisIntent("Cumprimento")]
        public async Task Cumprimento(IDialogContext context, LuisResult result)
        {
            var qnaService = new QnAMakerService(new QnAMakerAttribute(qnaSubscriptionKey, qnaKnowledgebaseId, "Buguei aqui, pera!  ¯＼(º_o)/¯"));
            var qnaMaker = new QnAMakerDialog(qnaService);
            await qnaMaker.MessageReceivedAsync(context, Awaitable.FromItem(ArgumentoStatic.Argument));
        }

        [LuisIntent("Favoritos")]
        public async Task Favoritos(IDialogContext context, LuisResult result)
        {
            //var botMovieTipsService = Dependency
        }

        [LuisIntent("Criticas")]
        public async Task Criticas(IDialogContext context, LuisResult result)
        {
            var entitie = result?.Entities?.FirstOrDefault()?.Entity?.ToString();
            var message = context.MakeMessage();
            message.Text = result.Query;
            await context.Forward(new BaseQnaMakerDialog(entitie), AfterQnaDialog, message, CancellationToken.None);
        }

        private async Task AfterQnaDialog(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var messageHandled = await result;
            context.Wait(MessageReceived);
        }

        [LuisIntent("Sugestao")]
        public async Task Sugestao(IDialogContext context, LuisResult result)
        {
            FormDialog<Form.Sugestao> sugestionForm = new FormDialog<Form.Sugestao>(new Form.Sugestao(), Form.Sugestao.BuildForm, FormOptions.PromptInStart);
            context.Call(sugestionForm, SugestaoFormCompleteAsync);
        }

        private async Task SugestaoFormCompleteAsync(IDialogContext context, IAwaitable<Form.Sugestao> result)
        {
            var adress = await result;
            context.Wait(MessageReceived);
        }

        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Eita, não consegui entender a frase {result.Query}");
        }
    }
}