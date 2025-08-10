using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    [Header("Para el jugador 1")]
    [SerializeField] private Slider r_slider;
    [SerializeField] private Slider g_slider;
    [SerializeField] private Slider b_slider;

    [SerializeField] private MeshRenderer autoMesh;
    [SerializeField, Min(0)] private int indiceMaterialColor;

    [Header("Para el jugador 2")]
    [SerializeField] private Slider r_slider2;
    [SerializeField] private Slider g_slider2;
    [SerializeField] private Slider b_slider2;

    [SerializeField] private MeshRenderer auto2Mesh;
    [SerializeField, Min(0)] private int indiceMaterialColor2;

    // Colores a guardarse para la personalizacion
    Color colorAutoJugador1;
    Color colorAutoJugador2;

    // Evita que se destruya el objeto para acceder a su informacion en la siguiente escena
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        r_slider.onValueChanged.AddListener(delegate { ActualizaColor(1); });
        g_slider.onValueChanged.AddListener(delegate { ActualizaColor(1); });
        b_slider.onValueChanged.AddListener(delegate { ActualizaColor(1); });

        r_slider2.onValueChanged.AddListener(delegate { ActualizaColor(2); });
        g_slider2.onValueChanged.AddListener(delegate { ActualizaColor(2); });
        b_slider2.onValueChanged.AddListener(delegate { ActualizaColor(2); });


        // Guarda el color default te los autos en caso de que el jugador no los modifique
        if (autoMesh != null)
        {
            colorAutoJugador1 = autoMesh.materials[indiceMaterialColor].color;
        }
        if (auto2Mesh != null)
        {
            colorAutoJugador2 = auto2Mesh.materials[indiceMaterialColor].color;
        }
    }

    private void ActualizaColor(int jugador)
    {
        switch (jugador)
        {
            case 1:
                if (indiceMaterialColor >= autoMesh.materials.Length) return;

                print(autoMesh.materials[indiceMaterialColor].name);
                autoMesh.materials[indiceMaterialColor].color = new Color(r_slider.value / 255f, g_slider.value / 255f, b_slider.value / 255f);
                // Imprimir el nombre del material para depuracion
                print(autoMesh.materials[indiceMaterialColor].name);
                // Imprime valores de rgb
                Debug.LogWarning("RGB: " + autoMesh.materials[indiceMaterialColor].color.ToString());
                break;

            case 2:
                if (indiceMaterialColor2 >= auto2Mesh.materials.Length) return;

                print(auto2Mesh.materials[indiceMaterialColor2].name);
                auto2Mesh.materials[indiceMaterialColor2].color = new Color(r_slider2.value / 255f, g_slider2.value / 255f, b_slider2.value / 255f);
                // Imprimir el nombre del material para depuracion
                print(auto2Mesh.materials[indiceMaterialColor].name);
                // Imprime valores de rgb
                Debug.LogWarning("RGB: " + auto2Mesh.materials[indiceMaterialColor].color.ToString());

                break;
        }
    }

    /// <summary>
    /// Diseniada para llamarse al dar clic en el boton de inicio del juego
    /// </summary>
    public void GuardaInformacionColores()
    {
        if (autoMesh != null)
        {
            colorAutoJugador1 = autoMesh.materials[indiceMaterialColor].color;
        }
        if (auto2Mesh != null)
        {
            colorAutoJugador2 = auto2Mesh.materials[indiceMaterialColor2].color;
        }
    }


    /// <summary>
    /// Diseniada para llamarse en el Start de los autos jugadores.
    /// </summary>
    /// <param name="jugador">1 o 2 para el jugador del numero respectiv</param>
    /// <param name="mallaJugador">La malla del auto a modificar</param>
    /// <param name="indiceMaterial">El indice del material a modificar su color</param>
    public void CargaInformacionColores(int jugador, MeshRenderer mallaJugador, int indiceMaterial)
    {
        switch (jugador)
        {
            case 1:
                mallaJugador.materials[indiceMaterial].color = colorAutoJugador1;
                break;
            case 2:
                mallaJugador.materials[indiceMaterial].color = colorAutoJugador2;
                break;
        }
    }
}
