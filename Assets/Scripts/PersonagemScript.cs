using UnityEngine;
using UnityEngine.UI;

public class ControlaImagemUI : MonoBehaviour
{
    [SerializeField] private Image imagemUI;

    private void Awake()
    {
        imagemUI.gameObject.SetActive(false);
    }

    public void Mostrar()
    {
        imagemUI.gameObject.SetActive(true);
    }

    public void Esconder()
    {
        imagemUI.gameObject.SetActive(false);
    }
}
