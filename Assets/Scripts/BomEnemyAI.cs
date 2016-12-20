using UnityEngine;
using System.Collections;

public class BomEnemyAI : MonoBehaviour {


	public float TargetDistance;
	public float enemyLookDistance;
	public float attackDistance;
	public float enemyMovingSpeed;
	public float maxSpeed;
	public float damping;
	public float explosiontime;
	public Transform fpsTarget;
	Rigidbody rb;
	Renderer myRender;

	void Start()
	{
		myRender = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		TargetDistance = Vector3.Distance(fpsTarget.position, transform.position);
		if (TargetDistance < enemyLookDistance)
		{
			myRender.material.color = Color.red;
			lookAtPlayer();
			print("Look at Player");
		}

		if (TargetDistance < attackDistance)
		{
			attackplease();
			print("ATTACK");
		}
		else
		{
			myRender.material.color = Color.white;
		}

		if (rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}


	}

	void lookAtPlayer()
	{
		Quaternion rotation = Quaternion.LookRotation(fpsTarget.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
	}

    void OnCollisionEnter(Collision other)
    {


        if (other.gameObject.CompareTag("Player"))
        {
            print("OnCollisionEnter " + other.gameObject.tag);
            float dist = Vector3.Distance(other.gameObject.transform.position, transform.position);
            print(dist);
        }
    }


    void attackplease()
	{
		rb.AddForce(transform.forward * enemyMovingSpeed);



		if(explosiontime > 0) 
			explosiontime -= Time.deltaTime;
		else {

			
			//var exp = GetComponent<ParticleSystem>();
			//exp.Play();

            playerController.

			Destroy(this.gameObject);

            
            

        }


    }







//enemy wander around//

public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
	{
		Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

		randomDirection += origin;

		NavMeshHit navHit;

		NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

		return navHit.position;
	}


}
