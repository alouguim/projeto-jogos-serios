using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;

public class ManejadorRespostas : MonoBehaviour
{
    [SerializeField] private RectTransform caixaResposta;
    [SerializeField] private RectTransform templateBotaoResposta;
    [SerializeField] private RectTransform containerResposta;

    private UIDialogo uiDialogo;

    private List<GameObject> botoesRespostaTemporarios = new List<GameObject>();    

    private void Start()
    {
        uiDialogo = GetComponent<UIDialogo>();  
    }

    public void MostrarRespostas(Resposta[] respostas)
    {
        float alturaCaixaResposta = 0;

        foreach (Resposta resposta in respostas)
        {

            GameObject botaoResposta = Instantiate(templateBotaoResposta.gameObject, containerResposta);
            botaoResposta.gameObject.SetActive(true);
            botaoResposta.GetComponent<TMP_Text>().text = resposta.TextoResposta;
            botaoResposta.GetComponent<Button>().onClick.AddListener(() => OnRespostaEscolhida(resposta));

            botoesRespostaTemporarios.Add(botaoResposta);

            alturaCaixaResposta += templateBotaoResposta.sizeDelta.y;
        }

        caixaResposta.sizeDelta = new Vector2(caixaResposta.sizeDelta.x, alturaCaixaResposta);
        caixaResposta.gameObject.SetActive(true);
    }   

    private void OnRespostaEscolhida(Resposta resposta)
    {
        caixaResposta.gameObject.SetActive(false);

        foreach(GameObject botao in botoesRespostaTemporarios)
        {
            Destroy(botao);
        }

        botoesRespostaTemporarios.Clear();

        uiDialogo.MostrarDialogo(resposta.ObjetoDialogo);
    }
}
