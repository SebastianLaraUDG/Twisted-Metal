using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Arma : MonoBehaviour
{
    [SerializeField] private GameObject balaPrefab;
    [SerializeField] private Transform origenSpawnBalas;
    private Rigidbody rb;

    private InputAction shootAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponentInParent<Rigidbody>();
        
        ControlAuto controlAuto = GetComponentInParent<ControlAuto>();
        switch (controlAuto.jugador_actual)
        {
            case 1:
                shootAction = InputSystem.actions.FindAction("AttackJ1");
                break;
            case 2:
                shootAction = InputSystem.actions.FindAction("AttackJ2");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Dispara las balas
        if (shootAction.IsPressed())
        {
            GameObject bala = Instantiate<GameObject>(balaPrefab, origenSpawnBalas.position, transform.rotation);
                // Aniade fuerza
                bala.GetComponent<Rigidbody>().linearVelocity = transform.forward * -10f * rb.linearVelocity.magnitude;
        }
    }
}
