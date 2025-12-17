using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class ManejadorRespostas : MonoBehaviour
{
    [SerializeField] private RectTransform caixaResposta;
    [SerializeField] private RectTransform templateBotaoResposta;
    [SerializeField] private RectTransform containerResposta;

    private UIDialogo uiDialogo;
    private EventoResposta[] eventoRespostas;

    private List<GameObject> botoesRespostaTemporarios = new List<GameObject>();    

    private void Start()
    {
        uiDialogo = GetComponent<UIDialogo>(); 

    }

    public void AddEventosReposta(EventoResposta[] eventoRespostas)
    {
        this.eventoRespostas = eventoRespostas;    
    }
    public void MostrarRespostas(Resposta[] respostas)
    {

        float alturaCaixaResposta = 0;

        for(int i = 0; i < respostas.Length; i++)
        {
            Resposta resposta = respostas[i];
            int indexResposta = i;

            GameObject botaoResposta = Instantiate(templateBotaoResposta.gameObject, containerResposta);
            botaoResposta.gameObject.SetActive(true);
            botaoResposta.GetComponent<TMP_Text>().text = resposta.TextoResposta;
            botaoResposta.GetComponent<Button>().onClick.AddListener(() => OnRespostaEscolhida(resposta, indexResposta));

            botoesRespostaTemporarios.Add(botaoResposta);

            alturaCaixaResposta += templateBotaoResposta.sizeDelta.y;
        }   

        caixaResposta.sizeDelta = new Vector2(caixaResposta.sizeDelta.x, alturaCaixaResposta);
        caixaResposta.gameObject.SetActive(true);
    }   

    private void OnRespostaEscolhida(Resposta resposta, int indexResposta )
    {

        uiDialogo.FinalizarEscolhaResposta();

        caixaResposta.gameObject.SetActive(false);

        foreach(GameObject botao in botoesRespostaTemporarios)
        {
            Destroy(botao);
        }

        botoesRespostaTemporarios.Clear();


        if (eventoRespostas != null && indexResposta <= eventoRespostas.Length)
        {
                eventoRespostas[indexResposta].OnRespostaEscolhida?.Invoke();
        }    

        eventoRespostas = null;

        if (resposta.ObjetoDialogo)
        {
            uiDialogo.MostrarDialogo(resposta.ObjetoDialogo);
        }
        else
        {
            uiDialogo.FecharCaixaDialogo();
        }
    }
}
    