using System.Collections;
using UnityEngine;
using TMPro;

public class UIDialogo : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text labelTexto;
    [SerializeField] private TMP_Text labelNome;
    [SerializeField] private GameObject caixaDialogo;
    [SerializeField] private ControlaImagemUI controlaImagemUI;

    public bool TaAberto { get; private set; }
    public bool EstaEscolhendoResposta { get; private set; }

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
        EstaEscolhendoResposta = false;

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
        if (EstaEscolhendoResposta) return;

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
            EstaEscolhendoResposta = true;
            manejadorRespostas.MostrarRespostas(dialogoAtual.Respostas);
        }
        else
        {
            ExecutarEventosDialogo();
            FecharCaixaDialogo();
        }
    }

    private IEnumerator MostrarLinhaAtual()
    {
        LinhaDialogo linha = dialogoAtual.Dialogo[indiceAtual];

        // Nome do personagem
        if (labelNome != null)
        {
            labelNome.text = linha.NomePersonagem;
        }

        // Retrato
        if (linha.Retrato != null)
        {
            controlaImagemUI.Mostrar(linha.Retrato);
        }
        else
        {
            controlaImagemUI.Esconder();
        }

        // Texto (digitando)
        yield return efeitoTypewriter.Run(linha.Texto, labelTexto);
    }



    private void ExecutarEventosDialogo()
    {
        EventoRespostaDialogo eventoDialogo = GetComponent<EventoRespostaDialogo>();
        if (eventoDialogo == null) return;

        var eventos = eventoDialogo.EventosFinalizarDialogo;
        if (eventos == null) return;

        foreach (var evento in eventos)
        {
            evento?.OnRespostaEscolhida?.Invoke();
        }
    }

    public void FinalizarEscolhaResposta()
    {
        EstaEscolhendoResposta = false;
    }

    public void FecharCaixaDialogo()
    {
        TaAberto = false;
        EstaEscolhendoResposta = false;

        caixaDialogo.SetActive(false);
        labelTexto.text = string.Empty;

        if (labelNome != null)
        {
            labelNome.text = string.Empty;
        }

        dialogoAtual = null;
        controlaImagemUI.Esconder();
    }


}
