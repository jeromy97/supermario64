using UnityEngine;
using System.Collections;

public class thwompController : MonoBehaviour {

    private Vector3 startPosition;
    private Vector3 endPosition;

    private bool moveUp;

    private Rigidbody rb;

    private float fallSpeed;
    private float floatSpeed;

    private int delay;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveUp = true;
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x, startPosition.y + 20, startPosition.z);

        fallSpeed = 20;
        floatSpeed = 5;
        delay = 2;

        move();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        move();
    }

    void move(string direction)
    {

    }

    /*
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
*/

    void move()
    {
        if (moveUp == true)
        {
            if (transform.position.y >= endPosition.y)
            {
                moveUp = false;
                StartCoroutine(ExecuteAfterTime(delay));
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
                StartCoroutine(ExecuteAfterTime(delay));
            }
            else
            {
                rb.velocity = new Vector3(0.0f, -fallSpeed, 0.0f);
            }
        }
    }

}
