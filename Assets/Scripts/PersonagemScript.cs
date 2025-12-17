using UnityEngine;
using UnityEngine.UI;

public class ControlaImagemUI : MonoBehaviour
{
    [SerializeField] private Image imagemUI;

    private void Awake()
    {
        imagemUI.gameObject.SetActive(false);
    }

    public void Mostrar(Sprite sprite)
    {
        imagemUI.sprite = sprite;
        imagemUI.gameObject.SetActive(sprite != null);
    }

    public void Esconder()
    {
        imagemUI.gameObject.SetActive(false);
    }
}
