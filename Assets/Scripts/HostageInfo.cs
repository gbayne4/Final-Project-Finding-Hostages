using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class HostageInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private int speed = 15;

    [SerializeField]
    public GameObject hostage1, hostage2, hostage3, hostage4, hostage5;
    [SerializeField]
    public GameObject hostage1dead, hostage2dead, hostage3dead, hostage4dead, hostage5dead;

    NavMeshAgent agent;
    public static bool checkDead = false;

    private bool following = false, dead = false, playerKilled;

    private int timer = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (Random.Range(0, 10) % 2 == 0) { transform.position = new Vector3(Random.Range(0, 143), 1, Random.Range(0, 143)); }
        else { transform.position = new Vector3(Random.Range(0, 80), 1, Random.Range(0, 80)); }
    }
    private void OnCollisionEnter(Collision collision)
    {

        //if you hit an obstacle
        if ((collision.gameObject.tag == "Player") && (!following))
        {
           // Debug.Log("hitting");
            if (Input.GetKey(KeyCode.E)) //if you choose to kill the hostage
            {
                following = true;
                Debug.Log("follow player");
                ScoreManager.hostages -= 1;
                ScoreManager.hostagesSaved += 1;

                MessageShow.displayMessage = true;

                MessageShow.message = "Hostage saved.";
            }

            if (Input.GetKey(KeyCode.Space) && !dead)
            {
                checkDead = true;
                dead = true;
                playerKilled = true;
                Death();
                BloodDrop.totalBlood += 300; //get blood from killing hostages
                ScoreManager.hostages -= 1;
                ScoreManager.hostagesDeadByYou += 1;

                MessageShow.displayMessage = true;

                MessageShow.message = "You killed a hostage.";
            }


        }

        if ((collision.gameObject.tag == "Monster") && !dead)
        {
            Debug.Log("hit by monster");
            dead = true;
            Death();
            ScoreManager.hostages -= 1;
            ScoreManager.hostagesDeadByMonster += 1;

            MessageShow.displayMessage = true;

            MessageShow.message = "A hostage just died";
        }
    }

    private void Update()
    {
        if (following) { FollowingPlayer();}
        if (dead) { Death(); } 
    }
    void FollowingPlayer()
    {
        timer++;
        if (timer > 20) //gives the monstert time to recalibrate
        {
            Destroy(gameObject);
            timer = 0;
        }//I initally wanted to make them follow the player, but it kept giving me bugs :(
        //agent.SetDestination(player.transform.position);
        // transform.LookAt(player.transform.position);
        //  transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
       // transform.position = Vector3.MoveTowards(transform.position,new Vector3( player.transform.position.x,transform.position.y, player.transform.position.z), Time.fixedDeltaTime * speed);
        following = true;
    }

    void Death()
    {
        timer++;
        if ((timer>20) && (playerKilled)) //gives the monster time to recalibrate
        {
            if (this.gameObject == hostage1)
            { Instantiate(hostage1dead, new Vector3(transform.position.x, 0.44f, transform.position.z), Quaternion.identity); }

            else if (this.gameObject == hostage2)
            { Instantiate(hostage2dead, new Vector3(transform.position.x, -0.1120251f, transform.position.z), Quaternion.identity); }

            else if (this.gameObject == hostage3)
            { Instantiate(hostage3dead, new Vector3(transform.position.x, 0.44f, transform.position.z), Quaternion.identity); }

            else if (this.gameObject == hostage4)
            { Instantiate(hostage4dead, new Vector3(transform.position.x, 0.44f, transform.position.z), Quaternion.identity); }

            else if (this.gameObject == hostage4)
            { Instantiate(hostage4dead, new Vector3(transform.position.x, 0.44f, transform.position.z), Quaternion.identity); }

            Destroy(gameObject);
            timer = 0;
        }
        if (!playerKilled)
        
        {   Destroy(gameObject);

            if (this.gameObject == hostage1)
            { Instantiate(hostage1dead, new Vector3(transform.position.x, 0.44f, transform.position.z), Quaternion.identity); }

            else if (this.gameObject == hostage2)
            { Instantiate(hostage2dead, new Vector3(transform.position.x, -0.1120251f, transform.position.z), Quaternion.identity); }

            else if (this.gameObject == hostage3)
            { Instantiate(hostage3dead, new Vector3(transform.position.x, 0.44f, transform.position.z), Quaternion.identity); }

            else if (this.gameObject == hostage4)
            { Instantiate(hostage4dead, new Vector3(transform.position.x, 0.44f, transform.position.z), Quaternion.identity); }

            else if (this.gameObject == hostage4)
            { Instantiate(hostage4dead, new Vector3(transform.position.x, 0.44f, transform.position.z), Quaternion.identity); }


        }
       

    }
}
