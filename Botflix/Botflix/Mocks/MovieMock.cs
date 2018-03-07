using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botflix.Mocks
{
    public class MovieMock
    {
        public static readonly string Content = @"
{
    'adult': false,
    'backdrop_path': '/rSVBuINAkGgJknWDbYOHZRJkCGF.jpg',
    'belongs_to_collection': {
        'id': 237445,
        'name': 'Inspetor Bugiganga',
        'poster_path': '/wP1En2StHVqmpsHRhTKlzFjTQyC.jpg',
        'backdrop_path': '/AosJpZ6TvYFYtRzvgFfb82g2S70.jpg'
    },
    'budget': 75000000,
    'genres': [
        {
            'id': 28,
            'name': 'Ação'
        },
        {
            'id': 12,
            'name': 'Aventura'
        },
        {
            'id': 35,
            'name': 'Comédia'
        },
        {
            'id': 10751,
            'name': 'Família'
        }
    ],
    'homepage': '',
    'id': 332,
    'imdb_id': 'tt0141369',
    'original_language': 'en',
    'original_title': 'Inspector Gadget',
    'overview': 'Após uma tentativa de resgate atrapalhada, o ingênuo segurança John Brown (Matthew Broderick) tem seu corpo dividido em mil pedaços pelo perigoso dr. Claw (Rupert Everett). Vendo a oportunidade como uma chance de demonstrar sua nova criação, a dra. Brenda (Joely Fisher) reconstitui o corpo de John, tornando-o agora um robô. Nasce então o inspetor Bugiganga, que logo se torna a mais importante ferramenta de combate ao crime na cidade.',
    'popularity': 8.639485000000001,
    'poster_path': '/AjgI4aibetGzyKrmsUXWtGoVmew.jpg',
    'production_companies': [
        {
            'name': 'Walt Disney Pictures',
            'id': 2
        },
        {
            'name': 'Caravan Pictures',
            'id': 175
        },
        {
            'name': 'DiC Entertainment',
            'id': 20477
        }
    ],
    'production_countries': [
        {
            'iso_3166_1': 'US',
            'name': 'United States of America'
        }
    ],
    'release_date': '1999-07-23',
    'revenue': 0,
    'runtime': 0,
    'spoken_languages': [
        {
            'iso_639_1': 'en',
            'name': 'English'
        },
        {
            'iso_639_1': 'no',
            'name': 'Norsk'
        },
        {
            'iso_639_1': 'fr',
            'name': 'Français'
        },
        {
            'iso_639_1': 'es',
            'name': 'Español'
        }
    ],
    'status': 'Released',
    'tagline': 'O maior herói jamais criado.',
    'title': 'Inspetor Bugiganga',
    'video': false,
    'vote_average': 4.3,
    'vote_count': 403
}";
    }
}