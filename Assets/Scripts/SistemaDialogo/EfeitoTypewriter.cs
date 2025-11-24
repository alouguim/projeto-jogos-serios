using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EfeitoTypewriter : MonoBehaviour
{
    [SerializeField] private float velocidadeDigitacao = 50f;

    private readonly List<Pontuacao> pontuacoes = new List<Pontuacao>()
    {
        new Pontuacao(new HashSet<char>(){'.', '!', '?'}, 0.6f),
        new Pontuacao(new HashSet<char>(){',', ';', ':'}, 0.3f)
    };
    private Coroutine coroutineAtual;
    private bool emExecucao;
    private bool completarImediatamente;

    public bool EmExecucao => emExecucao;

    public Coroutine Run(string textoParaDigitar, TMP_Text labelTexto)
    {
        // Cancela qualquer efeito anterior
        if (coroutineAtual != null)
        {
            StopCoroutine(coroutineAtual);
        }

        coroutineAtual = StartCoroutine(TypeText(textoParaDigitar, labelTexto));
        return coroutineAtual;
    }

    public void CompletarImediatamente()
    {
        completarImediatamente = true;
    }

    private IEnumerator TypeText(string textoParaDigitar, TMP_Text labelTexto)
    {
        emExecucao = true;
        completarImediatamente = false;
        labelTexto.text = string.Empty;

        float t = 0;
        int charIndex = 0;

        while (charIndex < textoParaDigitar.Length)
        {

            int indexUltimoCaractere = charIndex;

            if (completarImediatamente)
            {
                labelTexto.text = textoParaDigitar;
                break;
            }

            t += Time.deltaTime * velocidadeDigitacao;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textoParaDigitar.Length);

            for (int i = indexUltimoCaractere; i < charIndex; i++) 
            {
                bool isLast = i >= textoParaDigitar.Length - 1;
                labelTexto.text = textoParaDigitar.Substring(0, i + 1);

                if (TemPontuacao(textoParaDigitar[i], out float tempoEspera) && !isLast && !TemPontuacao(textoParaDigitar[i + 1], out _))
                { 
                    yield return new WaitForSeconds(tempoEspera);   
                }
            }

            yield return null;
        }

        labelTexto.text = textoParaDigitar;
        emExecucao = false;
    }

    private bool TemPontuacao(char caractere, out float tempoEspera)
    {
        foreach(Pontuacao categoriaPontuacoes in pontuacoes )
        {
            if (categoriaPontuacoes.Pontuacoes.Contains(caractere)) 
            {
                tempoEspera = categoriaPontuacoes.TempoEspera;
                return true;    
            }
        }

        tempoEspera = default;
        return false;
    }

    private readonly struct Pontuacao
    {
        public readonly HashSet<char> Pontuacoes;    
        public readonly float TempoEspera;  

        public Pontuacao(HashSet<char> pontuacoes, float tempoEspera)
        {
            Pontuacoes = pontuacoes;   
            TempoEspera= tempoEspera;   
        }
    }
}
