using UnityEngine;
using System.Collections;

public class GoombaController : MonoBehaviour {


    public int speed;

    public Vector3 rotationSpeed;

    public GameObject player;

    public float enemyLookDistance;

    private float playerDistance;

    private Rigidbody rb;
    private Rigidbody rbPlayer;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rbPlayer = player.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        playerDistance = Vector3.Distance(rbPlayer.transform.position, transform.position);

        if (playerDistance <= enemyLookDistance)
        {
            transform.LookAt(rbPlayer.transform);
        }
        else
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }

        transform.position += transform.forward * Time.deltaTime * speed;

    }
}
