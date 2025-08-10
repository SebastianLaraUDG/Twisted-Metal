using NUnit.Framework;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

/*
 Maneja el tiempo de vida y destruccion
de las balas
 */


public class Bala : MonoBehaviour
{
    [SerializeField] private float tiempoVida = 3f;
    [SerializeField] private float danio = 5f;

    private ParticleSystem myParticleSystem;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    public GameObject spark;

    private void Awake()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Destruir", tiempoVida);
    }

    private void Destruir()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        print("other: " + other.gameObject.name);

        HealthComponent otherHealth = other.gameObject.GetComponentInParent<HealthComponent>();
        if (otherHealth)
        {
            otherHealth.TakeDamage(danio);
            print("aplicado danio");
            Destruir();
        }
        myParticleSystem.Play();
    }

}
