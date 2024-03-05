using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float speed;
    public float start;
    public float end;

    private void Update()
    {
        MoveBackground();
    }

    void MoveBackground()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);

        if (transform.position.x <= end)
        {
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        transform.position = new Vector2(start, transform.position.y);
    }
}
