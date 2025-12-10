using System.Collections;
using UnityEngine;
using TMPro;

public class UIDialogo : MonoBehaviour
{
    [SerializeField] private TMP_Text labelTexto;
    [SerializeField] private GameObject caixaDialogo;

    public bool TaAberto { get; private set; }

    private ManejadorRespostas manejadorRespostas;
    private EfeitoTypewriter efeitoTypewriter;
    private ObjetoDialogo dialogoAtual;
    private int indiceAtual;

    void Start()
    {
        efeitoTypewriter = GetComponent<EfeitoTypewriter>();
        manejadorRespostas = GetComponent<ManejadorRespostas>();
        FecharCaixaDialogo();
    }

    public void MostrarDialogo(ObjetoDialogo objetoDialogo)
    {
        dialogoAtual = objetoDialogo;
        indiceAtual = 0;
        TaAberto = true;
        caixaDialogo.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(MostrarLinhaAtual());
    }
    
    public void AddEventosResposta(EventoResposta[] eventoRespostas)
    {
        manejadorRespostas.AddEventosReposta(eventoRespostas);
    }
    public void PassarDialogo()
    {
        if (!TaAberto || dialogoAtual == null) return;

        if (efeitoTypewriter.EmExecucao)
        {
            efeitoTypewriter.CompletarImediatamente();
            return;
        }

        indiceAtual++;

        if (indiceAtual < dialogoAtual.Dialogo.Length)
        {
            StopAllCoroutines();
            StartCoroutine(MostrarLinhaAtual());
        }
        else if (dialogoAtual.TemRespostas)
        {
            manejadorRespostas.MostrarRespostas(dialogoAtual.Respostas);
        }
        else
        {
            FecharCaixaDialogo();
        }
    }

    private IEnumerator MostrarLinhaAtual()
    {
        string linha = dialogoAtual.Dialogo[indiceAtual];
        yield return efeitoTypewriter.Run(linha, labelTexto);
    }

    public void FecharCaixaDialogo()
    {
        TaAberto = false;
        caixaDialogo.SetActive(false);
        labelTexto.text = string.Empty;
        dialogoAtual = null;
    }
}
