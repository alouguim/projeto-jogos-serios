using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    public Interagivel Interagivel { get; set; }
    public bool SprintAtivo { get; set; }


    private CharacterController controle;
    private Vector3 velocidadePlayer;
    private float velocidadeBase = 5f;
    private float velocidadeCorrendo = 7f;
    private bool noChao;
    public float gravidade = -9.8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controle = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        noChao = controle.isGrounded;
        
    }

    public void ProcessarMovimento(Vector2 input)
    {

        Vector3 moverDirecao = Vector3.zero;
        moverDirecao.x = input.x;
        moverDirecao.z = input.y;

        float velocidadeAtual = SprintAtivo ? velocidadeCorrendo : velocidadeBase;

        controle.Move(transform.TransformDirection(moverDirecao) * velocidadeAtual * Time.deltaTime);
        velocidadePlayer.y += gravidade * Time.deltaTime;
        if(noChao && velocidadePlayer.y < 0)
        {
            velocidadePlayer.y = -2f;
        }
        controle.Move(velocidadePlayer * Time.deltaTime);
        
    }
}
