﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    private int coins;
    private int stars;
    private int lifes;

    public Text coinText;
    public Text starText;
    public Text lifeText;

    private Animator anim;

    public float Turning = 0.0f;

    // Use this for initialization
    void Start () {
        coins = 0;
        stars = 0;
        lifes = 3;

        setCoinText();
        setStarText();
        setLifesText();

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            coins++;
            setCoinText();
        }
    }

    void Attack()
    {
        anim.Play("Armature|ArmatureAction.001");
        RaycastHit hit;

        if (Physics.Raycast(transform.FindChild("Eyes").position, transform.FindChild("Eyes").forward, out hit, 0.7f))
        {
            GameObject target = hit.collider.gameObject;
            print("WE HIT " + target.name);
            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(50);
            }

            if (target.tag.ToString() == "Goomba")
            {
                //Gooi naar achter
                print("HET IS EEN GOOMBA VERDOMME");
                target.transform.rotation = transform.rotation;
                //target.GetComponent<Rigidbody>().AddForce(target.transform.forward.x, target.transform.forward.y, target.transform.forward.z + 400);//new Vector3(400, 100, 0));
                //Debug.Log(transform.forward.x + ", " + transform.forward.y + ", " + transform.forward.z);
                Vector3 vec = target.transform.position;
                vec -= this.transform.position;
                vec.Normalize();
                vec *= 15;
                target.GetComponent<Rigidbody>().velocity = Vector3.zero;
                target.GetComponent<Rigidbody>().AddForce(vec, ForceMode.Impulse); // target_.rigidbody.AddExplosionForce(force,target_.transform.position,10,0,ForceMode.Impulse); } 

            }
        }
<<<<<<< HEAD
=======

        if (other.gameObject.CompareTag("green_mushroom"))
        {
            other.gameObject.SetActive(false);
            lifes++;
            setLifesText();
        }


>>>>>>> fe8468d3be170a1018779739f1ab830c9e0c9e60
    }

    void setCoinText()
    {
        //coinText.text = "Coins: " + coins.ToString();
    }

    void setStarText()
    {
        //starText.text = "Stars: " + stars.ToString();
    }
    void setLifesText()
    {
        //lifeText.text = "Lifes: " + lifes.ToString();
    }


}
