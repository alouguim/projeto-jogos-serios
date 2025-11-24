using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float rotacaoX = 0f;
    private float sensibilidadeX = 30f;
    private float sensibilidadeY = 30;
    
    public void ProcessarOlhar(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        rotacaoX -= (mouseY * Time.deltaTime) * sensibilidadeY;
        rotacaoX = Mathf.Clamp(rotacaoX, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(rotacaoX, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * sensibilidadeX);

    }
}
