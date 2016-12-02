using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class playerController : MonoBehaviour {

    private int coins;
    private int stars;
    private int lifes;
    private int redCoins;
    private int health;

    public Text coinText;
    public Text starText;
    public Text lifeText;
    public Text redCoinsText;
    public Text healthText;

    private Animator anim;

    public float Turning = 0.0f;

    public float LastJump = 0.0f;
    public int Jump = 0;

    // Use this for initialization
    void Start () {
        coins = 0;
        stars = 0;
        lifes = 3;
        redCoins = 0;
        health = 100;

        setCoinText();
        setStarText();
        setLifesText();
        setRedCoinsText();

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        LastJump += 1 * Time.deltaTime;

        if (LastJump < 1)
        {
            if (Jump == 1)
            {
                GetComponent<ThirdPersonCharacter>().m_JumpPower = 13;
            }
            else if (Jump == 2)
            {
                GetComponent<ThirdPersonCharacter>().m_JumpPower = 16;
            }
        }
        else
        {
            GetComponent<ThirdPersonCharacter>().m_JumpPower = 7;
            Jump = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LastJump = 0.0f;
            Jump++;
        }

        if (health >= 100)
        {
            health = 100;
        }

        if (health <= 100)
        {
            // Mario dies.
        }

        setHealthText();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            coins++;
            if (coins >= 100)
            {
                lifes++;
                coins = 0;
            }
            health = health + 20;
            setCoinText();
            setLifesText();
        }

        if (other.gameObject.CompareTag("red_coin"))
        {
            other.gameObject.SetActive(false);
            redCoins++;
            if (redCoins >= 8)
            {
                // Star appears.
            }
            setRedCoinsText();
        }

        // The following content doesn't work.

        /*
        if (other.gameObject.CompareTag("green_mushroom"))
        {
            other.gameObject.SetActive(false);
            lifes++;
            setLifesText();
        }
        */

        /*
        if (other.gameObject.CompareTag("goomba") || other.gameObject.CompareTag("boo"))
        {
            health = health - 20;
            setHealthText();
        }
        */

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
    }

    void setCoinText()
    {
        coinText.text = "Coins: " + coins.ToString();
    }

    void setStarText()
    {
        starText.text = "Stars: " + stars.ToString();
    }
    void setLifesText()
    {
        lifeText.text = "Lifes: " + lifes.ToString();
    }

    void setRedCoinsText()
    {
        redCoinsText.text = "Red coins: " + redCoins.ToString() + "/8";
    }

    void setHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

}
