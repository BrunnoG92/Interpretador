using System.Text.RegularExpressions;

namespace Models.Analises;

public class Analises_Texto
{
    static string RemoverAcentos(string texto)
    {
        Dictionary<string, string> acentos = new Dictionary<string, string>
        {
            { "á", "a" },
            { "à", "a" },
            { "â", "a" },
            { "ã", "a" },
            { "é", "e" },
            { "ê", "e" },
            { "í", "i" },
            { "ó", "o" },
            { "ô", "o" },
            { "õ", "o" },
            { "ú", "u" },
            { "ü", "u" },
            { "ç", "c" }
        };

        foreach (var accent in acentos)
        {
            texto = texto.Replace(accent.Key, accent.Value);
        }

        return texto;
    }
    public static string ClassificarTexto(string texto, Dictionary<string, List<string>> categorias)
{
    texto = RemoverAcentos(texto.ToLower());

    // Verificar se o texto inteiro está na lista
    foreach (var categoria in categorias)
    {
        if (categoria.Value.Contains(texto))
        {
            return categoria.Key;
        }
    }

    // Se o texto inteiro nao estiver na lista, dividir e comparar as palavras
    foreach (var categoria in categorias)
    {
        bool contemPalavras = categoria.Value.Exists(str =>
        {
            if (str.Contains(" "))
            {
                string[] palavras = str.Split(" ");

                // Procurar pelas duas primeiras palavras no texto
                string duasPrimeirasPalavras = string.Join(" ", palavras.Take(2));
                Regex regex = new Regex($@"\b{duasPrimeirasPalavras}\b", RegexOptions.IgnoreCase);

                return regex.IsMatch(texto);
            }
            else
            {
                Regex regex = new Regex($@"\b{str}\b", RegexOptions.IgnoreCase);
                return regex.IsMatch(texto);
            }
        });

        if (contemPalavras)
        {
            return categoria.Key;
        }
    }

    return "Outros Atendimentos";
}
    public static string ExecutarClassificacao(string mensagemCliente)
    {
        // Exemplo de chamada do método ClassificarTexto após normalizar o texto
        string resultado = ClassificarTexto(mensagemCliente, ObterCategorias());
        return resultado;
    }

    // Método para obter as categorias 
    static Dictionary<string, List<string>> ObterCategorias()
    {
        return new Dictionary<string, List<string>>
        {
            { "Lentidão", new List<string>
                {
                @"oscilando",
                "internet ruim",
                "conexão ruim",
                "falha sinal",
                "internet horrivel",
                "sem net",
                "wifi problema",
                "lento",
                "instabilidade",
                "nao está chegando",
                "travando",
                "sem sinal",
                "lentidão",
                "lenta",
                "internet pessima",
                "internet ruim"
                }
            },
            { "Troca de Senha", new List<string>
                {
                "esqueci senha",
                "senha incorreta",
                "trocar senha",
                "problema acesso",
                "senha expirada"
                }
            },
            { "Falha Telefonia", new List<string>
                {
                "nao escuto",
                "ligacao muda",
                "ruido na ligação",
                "nao faz chamadas",
                "falha telefonia"
                }
            },
            { "Falha TV", new List<string>
                {
               
                "canais fora",
                "tv travando",
                "falha TV"
                }
            },
            { "Falha Internet", new List<string>
                {
                "sem conexão",
                "obtendo ip",
                "funcionando",
                "segura",
                "internet nao funciona",
                "nao conecta",
                "site nao abre",
                "sem internet",
                "internet caindo",
                "caindo"
                }
            },
            { "Financeiro", new List<string>
                {
                "financeiro",
                "data",
                "carne",
                "nota fiscal",
                "nf",
                "negociar",
                "negociacao",
                "renegociacao",
                "renegociar",
                "cobranca",
                "fatura",
                "faturas",
                "mudar endereco"
            }
            },
            { "Comercial", new List<string>
            {
                "carne",
                "comercial",
                "novo ponto",
                "mudanca",
                "contrato",
                "falar com atendente"
            }
            },
            { "Desbloqueio", new List<string>
            {
                "paguei",
                "pagamento",
                "pago",
                "libera",
                "desbloqueio",
                "desbloqueia",
                "desbloquear",
                "comprovante pagamento",
                "comprovante"
            }
            },
            { "Vendas", new List<string>
            {
            "planos",
            "promocao",
            "Olá! Estou fazendo contato atraves do site Melhor Plano",
            "Olá! Gostaria de saber mais sobre o plano",
            "aumentar velocidade",
            "Gostaria de saber mais sobre o plano"
            }
            },
            { "Cancelamento", new List<string>
            {
            "cancelar",
            "cancelar internet",
            "cancelamento",
            "cancela"
            }
            },
            { "Boleto", new List<string>
            {
            "boleto",
            "conta",
            "boleta",
            "fatura",
            "2 via",
            "codigo",
            "codigo barras",
            "barras",
            "barra",
            "pix",
            "segunda via",
            "segunda"
            }
            },
            { "Saudacao", new List<string>
            {
                "olá",
                "oi",
                "bom dia",
                "boa tarde",
                "boa noite",
                "oi "

            }
            },
            { "Atualizacao Cadastral", new List<string>
            {
                "esse numero nao e",
                "remover",
                "nao sou",
                "apagar"


            }
            }

        };
    }
    public static string RetornaClassificacao(string texto)
    {
        return ExecutarClassificacao(texto);
    }

}

