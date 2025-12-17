using UnityEngine;
using System;

public class EventoRespostaDialogo : MonoBehaviour
{
    [SerializeField] private ObjetoDialogo objetoDialogo;
    [SerializeField] private EventoResposta[] eventos;
    [SerializeField] private EventoResposta[] eventosFinalizarDialogo;


    public ObjetoDialogo ObjetoDialogo => objetoDialogo;
    public EventoResposta[] Eventos => eventos;
    public EventoResposta[] EventosFinalizarDialogo => eventosFinalizarDialogo;


    public void OnValidate()
    {
        if (objetoDialogo == null) return;
        if (objetoDialogo.Respostas != null)
        {
            if (eventos == null)
            {
                eventos = new EventoResposta[objetoDialogo.Respostas.Length];
            }
            else
            {
                Array.Resize(ref eventos, objetoDialogo.Respostas.Length);
            }

            for (int i = 0; i < objetoDialogo.Respostas.Length; i++)
            {
                Resposta resposta = objetoDialogo.Respostas[i];

                if (eventos[i] != null)
                {
                    eventos[i].nome = resposta.TextoResposta;
                    continue;
                }

                eventos[i] = new EventoResposta { nome = resposta.TextoResposta };
            }
        }
    }

}
