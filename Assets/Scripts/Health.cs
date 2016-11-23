using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int HealthPoints = 100;
    public GameObject deaddrop;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(HealthPoints <= 0)
        {
            if (deaddrop != null)
            {
                Vector3 deadspot = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                Instantiate(deaddrop, deadspot, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        HealthPoints -= damage;
    }
}
