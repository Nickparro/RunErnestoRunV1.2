using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float minHeight, maxHeight; // Altura m�nima y m�xima a la que la c�mara puede seguir al objetivo

    private Vector2 lastPos; // �ltima posici�n de la c�mara

    private Transform target; // Objetivo que la c�mara sigue

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position; // Guarda la posici�n inicial de la c�mara
        SetTarget(GameObject.FindGameObjectWithTag("Player").transform); // Establece el objetivo de la c�mara como el objeto con el tag "Player"
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x + 25, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);
            // Actualiza la posici�n de la c�mara, manteniendo la coordenada x del objetivo y limitando la coordenada y dentro de los valores minHeight y maxHeight

            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);
            // Calcula la cantidad de movimiento de la c�mara

            lastPos = transform.position; // Actualiza la �ltima posici�n de la c�mara
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget; // Establece un nuevo objetivo para la c�mara
    }
}
