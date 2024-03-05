using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float minHeight, maxHeight; // Altura mínima y máxima a la que la cámara puede seguir al objetivo

    private Vector2 lastPos; // Última posición de la cámara

    private Transform target; // Objetivo que la cámara sigue

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position; // Guarda la posición inicial de la cámara
        SetTarget(GameObject.FindGameObjectWithTag("Player").transform); // Establece el objetivo de la cámara como el objeto con el tag "Player"
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);
            // Actualiza la posición de la cámara, manteniendo la coordenada x del objetivo y limitando la coordenada y dentro de los valores minHeight y maxHeight

            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);
            // Calcula la cantidad de movimiento de la cámara

            lastPos = transform.position; // Actualiza la última posición de la cámara
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget; // Establece un nuevo objetivo para la cámara
    }
}
