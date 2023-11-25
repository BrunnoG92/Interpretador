using Microsoft.AspNetCore.Mvc;
using Models.Analises;
using Newtonsoft.Json.Linq;
namespace Interpretador.Controllers;

[ApiController]
[Route("api/v1/interpretador")]
    public class InterpretadorController : ControllerBase
    {
        public class AnalisarAtendimentoModel
        {
            public string Texto { get; set; }
        }

        [HttpPost]
        public ActionResult AnalisarAtendimento([FromBody] AnalisarAtendimentoModel modelo)
        {
            if (modelo == null || string.IsNullOrEmpty(modelo.Texto))
            {
                return BadRequest("O JSON não contém um campo 'texto' válido.");
            }

            string texto = modelo.Texto;
            Console.WriteLine("Mensagem Cliente: " + texto);
            string resultado = Analises_Texto.RetornaClassificacao(texto);
            Console.WriteLine("Classificação Atendimento: " + resultado);
            return Ok(resultado);
        }
    }