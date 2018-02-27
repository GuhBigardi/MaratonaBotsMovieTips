using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using System.Configuration;

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

        [LuisIntent("Criticas")]
        public async Task Criticas(IDialogContext context, LuisResult result)
        {
            var qnaService = new QnAMakerService(new QnAMakerAttribute(qnaSubscriptionKey, qnaKnowledgebaseId, "Buguei aqui, pera! ¯＼(º_o)/¯"));
            var qnaMaker = new QnAMakerDialog(qnaService);
            await qnaMaker.MessageReceivedAsync(context, Awaitable.FromItem(ArgumentoStatic.Argument));
        }

        [LuisIntent("Sugestao")]
        public async Task Sugestao(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Puts, ainda não sei dar sugestões (╯3╰)");
        }


        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Eita, não consegui entender a frase {result.Query}");
        }
    }
}