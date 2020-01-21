using AutoMapper;
using PollingApp.Api.Dtos;
using PollingApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollingApp.Api
{
    public class MapperConfigurations : Profile
    {
        public MapperConfigurations()
        {
            CreateMap<QuestionModel, QuestionDto>().ReverseMap();
            CreateMap<ChoiceModel, ChoiceDto>().ReverseMap();
        }
    }
}
