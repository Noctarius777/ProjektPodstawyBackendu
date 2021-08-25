using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjektPodstawyBackendu.Domain;
using System.Linq;

namespace ProjektPodstawyBackendu.Controllers
{
    [ApiController]
    [Route("kurs")]
    public class KursController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IEmailService _emailService;
        public KursController(IConfiguration configuration,
                              IMessagesRepository messagesRepository,
                              IEmailService emailService, IEmailService emailService1)
        {
            _configuration = configuration;
            _messagesRepository = messagesRepository;
            _emailService = emailService1;
        }

        [Authorize("Administrator")]
        [Route("GetSomeSecretData")]
        public IActionResult GetSomeSecretData()
        {
            return Ok("SomeSecretKey");
        }

        [HttpPost]
        [Route("sendMessage")]

        public IActionResult SendMessage([FromBody]MessageDto messageDto)
        {
            var messageEntity = new MessageEntity
            {
                Content = messageDto.Content,
                FirstNameAuthor = messageDto.Author.Split(" ").First(),
                LastNameAuthor = messageDto.Author.Split(" ").Skip(1).First(),

            };

            _emailService.SendMessageEmail("rwgoqbibgsvsgujosp@rffff.net", messageDto.Content);
            var result = _messagesRepository.Add(messageEntity);
            if (result)
            {
                return Ok(messageDto);
            }
            return NotFound();

        }


        [Route("getMessages")]
        public IActionResult GetMessage()
        {
            var messages = _messagesRepository.GetAll();

            var messagesDto = messages.Select(x => new MessageDto
            
            {
                Content = x.Content,
                Author = x.FirstNameAuthor + " " + x.LastNameAuthor

            });
                       
            return Ok(messagesDto); 
        }
    }
}
