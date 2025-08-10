using UnityEngine;

public class CargaDeColores : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField,Min(0)] private int indiceMaterial;
    [SerializeField, Range(1, 2)] private int jugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        ColorManager colorManager = GameObject.FindAnyObjectByType<ColorManager>();
        if (colorManager != null)
        {
            colorManager.CargaInformacionColores(jugador, meshRenderer, indiceMaterial);
        }
    }
}
