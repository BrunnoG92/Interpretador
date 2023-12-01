using Microsoft.AspNetCore.Mvc;
using Models.Analises;
using Newtonsoft.Json.Linq;
namespace Interpretador.Controllers;

[ApiController]
[Route("api/v1/interpretador")]
public class InterpretadorController : ControllerBase
{
    static int Qtd = 0;
    public class AnalisarAtendimentoModel
    {
        public string Texto { get; set; }
    }
    public class FalhaRede
    {
        public string Texto { get; set; }
    }
    public class RespostaAtendimento
    {
        public string resposta { get; set; }
    }
    public class RespostaFalha
    {
        public string resposta { get; set; }
    }

    [HttpPost]
    public ActionResult AnalisarAtendimento([FromBody] AnalisarAtendimentoModel modelo)
    {
        if (modelo == null || string.IsNullOrEmpty(modelo.Texto))
        {
            return BadRequest("O JSON não contém um campo 'texto' válido.");
        }

        string texto = modelo.Texto;
        Qtd++;
        Console.WriteLine($"Mensagem Cliente N° {Qtd}: " + texto);
        string resultado = Analises_Texto.RetornaClassificacao(texto);
        Console.WriteLine("Classificação Atendimento: " + resultado);

        RespostaAtendimento respostaModel = new RespostaAtendimento
        {
            resposta = resultado
        };

        return Ok(respostaModel);
    }
    [Route("falha")]
    [HttpGet("FormatarFalha/{texto}")]
    public ActionResult FormatarFalha(string texto)
    {
        if (texto == null)
        {
            return BadRequest("Mensagem vazia.");
        }

        string resultado = Analises_Texto.FormataFalha(texto);
        return Ok(resultado);
    }
}