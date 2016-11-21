using UnityEngine;
using System.Collections;

public class BomEnemyAI : MonoBehaviour {


	public float fpsTargetDistance;
	public float enemyLookDistance;
	public float attackDistance;
	public float enemyMovingSpeed;
	public float maxSpeed;
	public float damping;
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
		fpsTargetDistance = Vector3.Distance(fpsTarget.position, transform.position);
		if (fpsTargetDistance < enemyLookDistance)
		{
			myRender.material.color = Color.red;
			lookAtPlayer();
			print("Look at Player");
		}

		if (fpsTargetDistance < attackDistance)
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

	void attackplease()
	{
		rb.AddForce(transform.forward * enemyMovingSpeed);
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
