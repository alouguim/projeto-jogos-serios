using UnityEngine;

[CreateAssetMenu(menuName = "Dialogo/Objeto Dialogo")]
public class ObjetoDialogo : ScriptableObject
{
    [SerializeField] private LinhaDialogo[] dialogo;
    public LinhaDialogo[] Dialogo => dialogo;

    public Resposta[] Respostas;

    public bool TemRespostas =>
        Respostas != null && Respostas.Length > 0;
}
