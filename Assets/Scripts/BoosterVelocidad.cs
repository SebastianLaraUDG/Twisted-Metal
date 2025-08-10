using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoosterVelocidad : MonoBehaviour
{
    public float fuerzaImpulso = 30000f;
    public ForceMode forceMode = ForceMode.Impulse;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.transform.parent.name);
        if (other.gameObject.transform.parent.CompareTag("Player 1") || other.gameObject.transform.parent.CompareTag("Player 2"))
        {
            other.attachedRigidbody.AddForce(transform.forward * fuerzaImpulso, ForceMode.Impulse);
            Debug.LogWarning("Aplicada fuerza");
        }
        else
            Debug.Log("No detectado el jugador pero si hay colision");
    }


}
