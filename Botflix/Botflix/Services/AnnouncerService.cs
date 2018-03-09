using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Botflix.Services
{
    [Serializable]
    public class AnnouncerService
    {
        private string botImageUrl;
        private string myNameIs;

        public AnnouncerService()
        {
            myNameIs = "Hey, eu sou BotFlix!";
            botImageUrl = ConfigurationManager.AppSettings["BotImageUrl"];
        }

        public HeroCard GenerateIntroduction() => new HeroCard()
        {
            Title = myNameIs,
            Subtitle = "Sou especialista em filmes e series.  ◕ ◡ ◕",
            Text = WhatIDo(),
            Images = new List<CardImage>()
            {
                new CardImage(botImageUrl, "Eu sou o Botflix")
            },
        };

        private string WhatIDo() => @"Fui criado pelo senhor (meu criador) para ajudar você a passar mais tempo assistindo filmes e series maravilhosas, do que ficar horas escolhendo um filme •(⌚_⌚)•! 
                                      Converse comigo, me peça uma crítica de um filme que você esteja pensando, me fale se gostou do filme e eu aprenderei os filmes e series que você gosta, ai quando 
                                      você me pedir indicações de filmes eu saberei o que te indicar! ♥‿♥";

       
    }
}