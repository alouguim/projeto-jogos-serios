using UnityEngine;

public class AtivadorDialogo : MonoBehaviour, Interagivel
{
    [SerializeField] private ObjetoDialogo objetoDialogo;

    public void AtualizarObjetoDialogo(ObjetoDialogo objetoDialogo)
    {
        this.objetoDialogo = objetoDialogo; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out InputManager inputManager))
        {
            inputManager.Interagivel = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out InputManager inputManager))
        {
            if(inputManager.Interagivel is AtivadorDialogo ativadorDialogo && ativadorDialogo == this)
            inputManager.Interagivel = null;
        }
    }
    public void Interagir(InputManager inputManager)
    {
        foreach (EventoRespostaDialogo eventoResposta in GetComponents<EventoRespostaDialogo>())
        {
            if (eventoResposta.ObjetoDialogo == objetoDialogo)
            {
                inputManager.UIDialogo.AddEventosResposta(eventoResposta.Eventos);
                break;
            }
        }
        inputManager.UIDialogo.MostrarDialogo(objetoDialogo);
    }

}
