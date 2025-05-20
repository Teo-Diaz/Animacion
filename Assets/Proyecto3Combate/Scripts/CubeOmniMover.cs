using UnityEngine;

/// <summary>
/// Movimiento omnidireccional sencillo para un cubo.
/// •  WASD  → desplazamiento en plano X-Z
/// •  Q/E   → bajar / subir en el eje Y
/// •  Velocidad regulable desde el Inspector
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class CubeOmniMover : MonoBehaviour
{
    [Header("Ajustes de movimiento")]
    [SerializeField] private float moveSpeed = 5f;        // m/s
    [SerializeField] private float verticalSpeed = 3f;    // m/s (subir/bajar)
    [SerializeField] private float gravity = -9.81f;

    private CharacterController cc;
    private Vector3 verticalVel;                           // para la gravedad

    private void Awake() => cc = GetComponent<CharacterController>();

    private void Update()
    {
        // ----- Entrada horizontal y vertical (plano X-Z) -----
        float h = Input.GetAxisRaw("Horizontal");   // A/D  o  ←/→
        float v = Input.GetAxisRaw("Vertical");     // W/S  o  ↑/↓

        Vector3 move = new Vector3(h, 0f, v);

        // Opcional: normalizar para evitar más velocidad en diagonal
        if (move.sqrMagnitude > 1f) move.Normalize();
        move *= moveSpeed;

        // ----- Entrada vertical (eje Y) -----
        float yInput = 0f;
        if (Input.GetKey(KeyCode.E)) yInput = 1f;   // subir
        else if (Input.GetKey(KeyCode.Q)) yInput = -1f;  // bajar

        move.y = yInput * verticalSpeed;

        // ----- Gravedad -----
        if (cc.isGrounded && verticalVel.y < 0) verticalVel.y = 0;
        verticalVel.y += gravity * Time.deltaTime;

        // ----- Aplicar movimiento -----
        cc.Move((move + verticalVel) * Time.deltaTime);
    }
}

