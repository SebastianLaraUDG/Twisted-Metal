using UnityEngine;

/*
 * Segumiento a un objetivo
 *
 */
public class Gimbal : MonoBehaviour
{
    [SerializeField] private Transform objetivo;
    [SerializeField] private float velocidadRotacion = 0.5f;
    [SerializeField] private float sensibilidadRotacionMouse = 1f;
    public ControlAuto autoControlado;

    private Transform rotY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotY = transform.GetChild(0);
    }

    private void LateUpdate()
    {
        transform.position = objetivo.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, objetivo.rotation, velocidadRotacion * Time.deltaTime);

        // Rotar con movimiento del mouse
        rotY.Rotate(new Vector3(0f, /*Input.mousePositionDelta.x*/autoControlado.valorCambioX * sensibilidadRotacionMouse, 0f));

        // rotY.rotation = Quaternion.Slerp(rotY.rotation, objetivo.rotation, velocidadRotacion * Time.deltaTime);

        //rotY.rotation = Quaternion.Euler(0f, objetivo.rotation.y, 0f);

    }
}
