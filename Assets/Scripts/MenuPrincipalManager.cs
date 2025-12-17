using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{

    [SerializeField] private string nomeCena;
    public void Jogar()
    {
        SceneManager.LoadScene(nomeCena);
    }


}
