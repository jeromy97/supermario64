using UnityEngine;
using System.Collections;

public class thwompController : MonoBehaviour {

    public float height;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private bool moveUp;

    private Rigidbody rb;

    private float fallSpeed;
    private float floatSpeed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveUp = true;
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x, startPosition.y + height, startPosition.z);

        fallSpeed = 20;
        floatSpeed = 5;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (moveUp == true)
        {
            if (transform.position.y >= endPosition.y)
            {
                moveUp = false;
            }
            else
            {
                rb.velocity = new Vector3(0.0f, floatSpeed, 0.0f);
            }
        }
        else
        {
            if (transform.position.y <= startPosition.y)
            {
                moveUp = true;
            }
            else
            {
                rb.velocity = new Vector3(0.0f, -fallSpeed, 0.0f);
            }
        }
        
    }

}
