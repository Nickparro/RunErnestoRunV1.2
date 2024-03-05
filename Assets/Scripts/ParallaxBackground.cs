using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float speed;
    public float start;
    public float end;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);

        if (transform.position.x <= end)
        {
            transform.position = new Vector2(start, transform.position.y);
        }
    }
}