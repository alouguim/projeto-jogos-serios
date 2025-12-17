using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private UIDialogo uiDialogo;
    [SerializeField] private MenuCelularUI menuCelular;

    public UIDialogo UIDialogo => uiDialogo;
    public Interagivel Interagivel { get; set; }

    private InputSystem_Actions inputActions;
    private PlayerMotor motor;
    private PlayerLook olhar;

    private bool MovimentoBloqueado =>
    (uiDialogo != null && uiDialogo.TaAberto) ||
    (menuCelular != null && menuCelular.Aberto);


    void Awake()
    {
        inputActions = new InputSystem_Actions();
        motor = GetComponent<PlayerMotor>();
        olhar = GetComponent<PlayerLook>();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void FixedUpdate()
    {
        if (MovimentoBloqueado) return;

        motor.ProcessarMovimento(
            inputActions.Player.Move.ReadValue<Vector2>()
        );
    }

    void LateUpdate()
    {
        if (MovimentoBloqueado) return;

        olhar.ProcessarOlhar(
            inputActions.Player.Look.ReadValue<Vector2>()
        );
    }


    void Update()
    {
        // Sprint só quando não estiver bloqueado
        if (!MovimentoBloqueado)
        {
            motor.SprintAtivo = inputActions.Player.Sprint.IsPressed();
        }

        if (inputActions.Player.Interact.triggered)
        {
            // 👉 prioridade: diálogo
            if (uiDialogo != null && uiDialogo.TaAberto)
            {
                uiDialogo.PassarDialogo();
            }
            else if (Interagivel != null)
            {
                Interagivel.Interagir(this);
            }
        }
    }

}
