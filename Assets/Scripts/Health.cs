using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int HealthPoints = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(HealthPoints <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    public void TakeDamage(int damage)
    {
        HealthPoints -= damage;
    }
}
