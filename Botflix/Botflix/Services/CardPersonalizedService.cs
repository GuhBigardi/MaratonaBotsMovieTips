using Botflix.Model;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace Botflix.Services
{
    public class CardPersonalizedService
    {
        public HeroCard CreateCard(Media media)
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
                        Value = $"gostei do Filme {media.id}"

                    }

                }
            };

            return heroCard;
        }
    }
}