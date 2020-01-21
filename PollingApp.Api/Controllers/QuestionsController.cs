using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PollingApp.Admin;
using PollingApp.Api.Dtos;
using PollingApp.Core;
using System.Collections.Generic;
using System.Linq;

namespace PollingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsAdmin _questionsAdmin;
        private readonly IMapper _mapper;

        public QuestionsController(IQuestionsAdmin questionsAdmin, IMapper mapper)
        {
            _questionsAdmin = questionsAdmin;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IList<QuestionDto>> Get(
            [FromQuery]int limit = 10, [FromQuery]int offset = 0, [FromQuery]string filter = null
            )
        {
            return _questionsAdmin.GetAll(limit, offset, filter).Select(x => _mapper.Map<QuestionDto>(x)).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<QuestionDto> Get(string id)
        {
            return _mapper.Map<QuestionDto>(_questionsAdmin.GetById(id));
        }

        [HttpPost]
        public void Post([FromBody] QuestionDto dto)
        {
            var question = _mapper.Map<QuestionModel>(dto);

            _questionsAdmin.SaveOrUpdate(question);
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody] QuestionDto dto)
        {
            var question = _mapper.Map<QuestionModel>(dto);

            _questionsAdmin.SaveOrUpdate(question,id);
        }
    }
}
