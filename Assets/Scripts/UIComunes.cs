using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIComunes : MonoBehaviour
{
    public static void CerrarJuego()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
 Application.Quit();
#endif
    }
    public static void CambiaEscena(int indiceEscena)
    {
        SceneManager.LoadScene(indiceEscena);
    }
}
