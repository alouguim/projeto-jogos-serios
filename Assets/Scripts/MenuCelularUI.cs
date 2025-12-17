using UnityEngine;
using UnityEngine.InputSystem;

public class MenuCelularUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject painelCelular;

    private bool aberto;
    public bool Aberto => aberto;

    void Start()
    {
        if (painelCelular != null)
            painelCelular.SetActive(false);

        aberto = false;
    }

    void Update()
    {
        // Aqui você pode trocar pela sua action depois,
        // mas mantendo simples por enquanto
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (aberto)
                Fechar();
            else
                Abrir();
        }
    }

    public void Abrir()
    {
        if (aberto) return;

        aberto = true;

        if (painelCelular != null)
            painelCelular.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("📱 Celular ABERTO (jogo pausado)");
    }

    public void Fechar()
    {
        if (!aberto) return;

        aberto = false;

        if (painelCelular != null)
            painelCelular.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("📱 Celular FECHADO (jogo retomado)");
    }
}
