using UnityEngine;

/* Script para que un objeto
 * gire (rotacion tipo yaw)
 */
public class RotarObjeto : MonoBehaviour
{
    [SerializeField] private float velocidadRotacion = 1f;
    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, velocidadRotacion * Time.deltaTime);
    }
}
