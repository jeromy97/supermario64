using UnityEngine;
using System.Collections;

public class GoombaController : MonoBehaviour {


    public int speed;

    public Vector3 rotationSpeed;

    public GameObject thirdPerson;

    public float enemyLookDistance;

    private float playerDistance;

    private Rigidbody rb;
    private Rigidbody rbThirdPerson;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rbThirdPerson = thirdPerson.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        playerDistance = Vector3.Distance(rbThirdPerson.transform.position, transform.position);

        if (playerDistance <= enemyLookDistance)
        {
            transform.LookAt(rbThirdPerson.transform);
        }
        else
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }

        transform.position += transform.forward * Time.deltaTime * speed;

    }
}
