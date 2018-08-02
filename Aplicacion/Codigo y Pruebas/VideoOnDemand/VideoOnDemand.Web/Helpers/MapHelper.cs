using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Helpers
{
    public class MapHelper
    {
        internal static IMapper mapper;

        static MapHelper()
        {
            var config = new MapperConfiguration(x => {
                x.CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
<<<<<<< HEAD
                x.CreateMap<Media, MediaViewModel>().ReverseMap();
                x.CreateMap<Genero, GeneroViewModel>().ReverseMap();
=======
                x.CreateMap<Persona, PersonaViewModel>().ReverseMap();
>>>>>>> df84d6a78af5cd3051a257fcbee6d8db99039ad1

               });
            mapper = config.CreateMapper();
        }

        public static T Map<T>(object source)
        {
            return mapper.Map<T>(source);
        }
    }
}