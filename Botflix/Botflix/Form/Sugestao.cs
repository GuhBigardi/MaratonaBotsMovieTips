using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;

namespace Botflix.Form
{
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "Desculpe não entendi \"{0}\".")]
    public class Sugestao
    {
        public Tipo Tipo { get; set; }
        public Categoria Categoria { get; set; }
        public Humor Humor { get; set; }

        public static IForm<Sugestao> BuildForm()
        {
            var form = new FormBuilder<Sugestao>();
            form.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Buttons;
            form.Configuration.Yes = new string[] { "sim", "yes", "s", "y", "yep" };
            form.Configuration.No = new string[] { "não", "nao", "no", "not", "n" };
            form.Message("Humm, Será um prazer ajudar você a escolher algo para assitir.");
            form.OnCompletion(async (context, sugestao) =>
            {
                await context.PostAsync("Não sei se vira, assiste qualquer merda, poe na cultura!");
            });
            return form.Build();
        }

    }


    [Describe("Filme ou Serie")]
    public enum Tipo
    {
        [Terms("filme", "filmi", "firmi", "movie", "filmes", "filminho", "filmao", "filmezera")]
        [Describe("Filme")]
        Filme = 1,

        [Terms("serie", "sereia", "series", "serizinha", "seriado", "novela")]
        [Describe("Serie")]
        Serie,

        [Terms("qualquer", "tanto faz", "qualquer um", "sei la", "me surpreenda", "sovai", "voce que manda", "Anyway", "nao sei", "sei nao")]
        [Describe("Qualquer")]
        Qualquer
    }

    [Describe("Categoria")]
    public enum Categoria
    {

        [Terms("comedia", "risada", "huehuebr", "rir", "kkkkkk", "Engracado")]
        [Describe("Comedia")]
        Comedia = 1,

        [Terms("drama", "tensao", "apreensao")]
        [Describe("Drama")]
        Drama,

        [Terms("acao", "lutinha", "luta", "tapa", "soco", "porrada", "tiro", "bomba", "guerra", "carro", "corrida")]
        [Describe("Ação")]
        Acao,

        [Terms("suspense", "sequestro", "furtivo")]
        [Describe("Suspense")]
        Suspense,

        [Terms("Ficcao", "historinha", "Ficção", "Ficção cientifica", "mentirinha", "super heroi", "monstro", "tubarao voador", "tubarao na neve", "Dinofauro", "Dinossauro", "viagem no tempo", "desastres naturais")]
        [Describe("Ficção cientifica")]
        Ficcao,

        [Terms("romance", "love", "amor", "10/10", "crush", "melo dramatico", "melacao", "beijo", "amorzinho", "t grande")]
        [Describe("Romance")]
        Romance
    }

    [Describe("Como está seu humor?")]
    public enum Humor
    {
        [Terms("feliz", "happy", "alegre", "sorrindo", "rsrs", "risos", "hahaha")]
        [Describe("Feliz")]
        Feliz = 1,

        [Terms("triste", "bad", "bad vibes", "tisti", "chateado", "na bad", "cortando os pulsos", "na foça", "terminei o relacionamento")]
        [Describe("Triste")]
        Triste,

        [Terms("bravo", "mad", "puto", "mata um", "raiva", "pistola", "raivinha")]
        [Describe("Bravo")]
        Bravo,

        [Terms("qualquer", "tanto faz", "qualquer um", "sei la", "me surpreenda", "sovai", "voce que manda", "Anyway", "nao sei", "sei nao")]
        [Describe("Sei la")]
        Qualquer
    }
}