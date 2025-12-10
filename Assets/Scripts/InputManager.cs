using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private UIDialogo uiDialogo;

    public UIDialogo UIDialogo => uiDialogo;
    public Interagivel Interagivel { get; set; }
    private InputSystem_Actions inputActions;
    private PlayerMotor motor;
    private PlayerLook olhar;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        motor = GetComponent<PlayerMotor>();
        olhar = GetComponent<PlayerLook>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void FixedUpdate()
    {
        if (uiDialogo.TaAberto) return;   // trava movimento

        motor.ProcessarMovimento(inputActions.Player.Move.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        if (uiDialogo.TaAberto) return;   // trava câmera

        olhar.ProcessarOlhar(inputActions.Player.Look.ReadValue<Vector2>());
    }

    void Update()
    {

        motor.SprintAtivo = inputActions.Player.Sprint.IsPressed();

        if (inputActions.Player.Interact.triggered)
        {
            Debug.Log("🟢 Tecla de interação pressionada!");

            if (uiDialogo.TaAberto)
            {
                Debug.Log("📜 Dialogo aberto → Passando diálogo");
                uiDialogo.PassarDialogo();
            }
            else if (Interagivel != null)
            {
                Debug.Log($"🔹 Interagindo com");
                Interagivel.Interagir(this);
            }
            else
            {
                Debug.Log("⚪ Nada pra interagir no momento");
            }
        }
    }
}
