using UnityEngine;
using System.Collections;

public class booController : MonoBehaviour {

    public int speed;

    public Vector3 rotationSpeed;

    public GameObject player;

    public float enemyLookDistance;

    private float playerDistance;

    private Rigidbody rb;
    private Rigidbody rbPlayer;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rbPlayer = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.LookAt(rbPlayer.transform);
            transform.position += transform.forward * Time.deltaTime * speed;
        }

    }
}
