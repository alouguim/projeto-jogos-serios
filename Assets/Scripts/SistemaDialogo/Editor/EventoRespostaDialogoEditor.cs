using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EventoRespostaDialogo))]

public class EventoRespostaDialogoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EventoRespostaDialogo eventoResposta = (EventoRespostaDialogo)target;   

        if (GUILayout.Button("Atualizar"))
        {
            eventoResposta.OnValidate();
        }
    }
}
