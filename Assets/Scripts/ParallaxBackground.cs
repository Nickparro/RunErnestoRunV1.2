using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float speed;
    public float start;
    public float end;

    Vector3 startPos;
    public float offset;

    private void Start() 
    {
        startPos = transform.position;
        offset = GetComponent<BoxCollider2D>().size.x / 2;
    }
    void Update()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);

        // if (transform.position.x <= end)
        // {
        //     transform.position = new Vector2(start, transform.position.y);
        // }

        if(transform.position.x < startPos.x - offset)
        {
            transform.position = startPos;
        }
    }
}