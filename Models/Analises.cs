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
            if (categoria.Value.Any(str => texto.Contains(RemoverAcentos(str.ToLower()))))
            {
                return categoria.Key;
            }
        }

        // Se o texto inteiro nao estiver na lista, verificar se todas as palavras estão presentes
        foreach (var categoria in categorias)
        {
            bool todasPalavrasPresentes = categoria.Value.All(str =>
            {
                return texto.Contains(RemoverAcentos(str.ToLower()));
            });

            if (todasPalavrasPresentes)
            {
                return categoria.Key;
            }
        }

        // Se nenhuma das condições acima for atendida, verificar se pelo menos duas palavras estão presentes

        // Quebra o texto em uma lista de palavras
        string[] palavrasTexto = texto.Split(new[] { ' ', '\t', '\n', '\r', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var categoria in categorias)
        {
            // Quebra as palavras-chave, considerando palavras compostas
            string[] palavrasChave = categoria.Value
                .SelectMany(str => str.Split(' '))
                .ToArray();

            // Conta quantas palavras-chave estão presentes na lista de palavras do texto
            int contadorPalavrasPresentes = palavrasChave.Count(str => palavrasTexto.Contains(str));

            // Verifica se pelo menos duas palavras-chave foram encontradas
            if (contadorPalavrasPresentes >= 2)
            {
                Console.WriteLine($"Palavras-chave encontradas: {string.Join(", ", palavrasChave.Where(str => palavrasTexto.Contains(str)))}");
                return categoria.Key;
            }
        }

        // Se não houver correspondência
        Console.WriteLine("Nenhuma correspondência encontrada.");
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
             { "Falha Internet", new List<string>
                {
                "problema rede",
                "sem sinal",
                "luz vermelha",
                "sem acesso",
                "sem conexão",
                "sem enternet",
                "obtendo ip",
                "funcionando",
                "segura",
                "nao funciona",
                "nao conecta",
                "site nao abre",
                "sem internet",
                "caindo",
                "suporte tecnico"
               }
            },
            { "Lentidão", new List<string>
                {
                @"oscilando",
                "orrivel",
                "falta conexao",
                "ruim",
                "conexao ruim",
                "falha sinal",
                "horrivel",
                "net esta lenta",
                "baixa velocidade",
                "nao esta passando",
                "wifi problema",
                "lento",
                "instabilidade",
                "nao está chegando",
                "travando",
                "sem sinal",
                "lentidao",
                "lenta",
                "internet pessima"
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
                "telefone sem linha",
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


            { "Comercial", new List<string>
            {
                "mudar plano",
                "mudar internet",
                "atendente",
                "me atender",
                "comercial",
                "mudanca",
                "contrato",
                "falar com atendente"
            }
            },
            { "Desbloqueio", new List<string>
            {
                "paguei",
                "desbloqueo",
                "ja esta pago",
                "pagamento",
                "pago",
                "libera",
                "desbloqueio",
                "desbloqueio em confianca",
                "desbloqueio confianca",
                "desbloqueio de confianca",
                "desbloqueia",
                "desbloquear",
                "comprovante pagamento",
                "comprovante"
            }
            },
            { "Vendas", new List<string>
            {
            "adquirir plano",
            "planos",
            "novo ponto",
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
             { "Financeiro", new List<string>
                {
                "cartao",
                "credito",
                "debito",
                "debitado",
                "conseguindo pagar",
                "financeiro",
                 "endereco",
                "data",
                "carne",
                "nota fiscal",
                "nf",
                "mudar endereco"
            }
            },
            { "Boleto", new List<string>
            {
            "qr code",
            "boleto",
            "mandar boleto",
            "manda boleto",
            "manda boleta",
            "boleta",
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
            { "Saudação", new List<string>
            {
                "ola",
                "oi",
                "bom dia",
                "boa tarde",
                "boa noite"

            }
            },
            { "Atualizacao Cadastral", new List<string>
            {
                "esse numero nao e",
                "remover",
                "nao sou",
                "apagar",
                "essa pessoa"


            }
            },
             { "Renegociar", new List<string>
            {
                "renegociacao",
                "renegociar",
                "negociar"

            }

            }


        };
    }
    public static string RetornaClassificacao(string texto)
    {
        return ExecutarClassificacao(texto);
    }
     public static string FormataFalha(string texto)
    {
        string resultado = Regex.Replace(texto, @"\[.*?\]\n?", string.Empty); // Remove o padrão [.*?]\n
        resultado = resultado.Replace("Prezado Cliente. Estamos com uma falha massiva em sua região.", string.Empty).Trim();
        string textoFinal = Regex.Replace(resultado, "[\"\\[\\]]", string.Empty); // Remove as aspas
        return textoFinal;
    }

}

