using UnityEngine;
using System.Collections;

public class booController : MonoBehaviour {

    public int speed;

    public Vector3 rotationSpeed;

    public GameObject player;

    public float enemyLookDistance;
    public float angle;

    private float playerDistance;
    
    private Rigidbody rbPlayer;

    // Use this for initialization
    void Start()
    {
        rbPlayer = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Angle(player.transform.forward, transform.position - player.transform.position) > angle)
        {
            transform.LookAt(rbPlayer.transform);
            transform.position += transform.forward * Time.deltaTime * speed;
        }

    }
}
