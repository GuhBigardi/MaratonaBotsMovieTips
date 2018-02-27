using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using System.Configuration;

namespace Botflix.Dialogs
{
    [Serializable]
    [LuisModel("502bc406-379a-4d0a-a468-8c751465257d", "a53bef52e5b147f781e9bd6260f83f79")]
    public class BotflixDialog : LuisDialog<object>
    {
        string qnaSubscriptionKey = ConfigurationManager.AppSettings["QnaSubscriptionKey"];
        string qnaKnowledgebaseId = ConfigurationManager.AppSettings["QnaKnowledgebaseId"];

     
        [LuisIntent("Cumprimento")]
        public async Task Cumprimento(IDialogContext context, LuisResult result)
        {
            var qnaService = new QnAMakerService(new QnAMakerAttribute(qnaSubscriptionKey, qnaKnowledgebaseId, "Tente Novamente"));
            var qnaMaker = new QnAMakerDialog(qnaService);
            await qnaMaker.MessageReceivedAsync(context, Awaitable.FromItem(ArgumentoStatic.Argument));
        }

        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Desculpe, não consegui entender a frase {result.Query}");
        }
    }
}