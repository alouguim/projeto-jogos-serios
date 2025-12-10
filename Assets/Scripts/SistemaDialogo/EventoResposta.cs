using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventoResposta
{
    [HideInInspector] public string nome;
    [SerializeField] private UnityEvent onRespostaEscolhida;

    public UnityEvent OnRespostaEscolhida => onRespostaEscolhida;


}
