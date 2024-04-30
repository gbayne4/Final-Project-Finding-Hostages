using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Linq;

public class MonsterMove : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private GameObject hostage1, hostage2, hostage3, hostage4, hostage5, hostage6, hostage7, hostage8, hostage9, hostage10, hostage11, hostage12, hostage13, hostage14, hostage15, hostage16;

    NavMeshAgent agent;
    private Vector3 closestVector;

    private int hostNum = 16;

    [SerializeField]
    LayerMask groundLayer, playerLayer;

    //patrol
    Vector3 destination;
    private bool walkPoint;

    [SerializeField]
    private float rangeNeg = 0;

    [SerializeField]
    private float rangePos = 143;

    [SerializeField]
    private float almostThere;

    private int timer, limbs = 0;
    private int distance = 20, playerDistance = 400, monsterTimer = 0;
    // Start is called before the first frame update

    public List<GameObject> hostages = new List<GameObject>();
    void Start()
    {

        hostages.Add(hostage1);
        hostages.Add(hostage2);
        hostages.Add(hostage3);
        hostages.Add(hostage4);
        hostages.Add(hostage5);
        hostages.Add(hostage6);
        hostages.Add(hostage7);
        hostages.Add(hostage8);
        hostages.Add(hostage9);
        hostages.Add(hostage10);
        hostages.Add(hostage11);
        hostages.Add(hostage12);
        hostages.Add(hostage13);
        hostages.Add(hostage14);
        hostages.Add(hostage15);
        hostages.Add(hostage16);


        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");


    }

    private void OnCollisionEnter(Collision collision)
    {

        //if you hit an obstacle
        
        if (collision.gameObject.tag == "Player")
        {
            MessageShow.displayMessage = true;

            MessageShow.message = "You're going to die!";

            monsterTimer++;
            if (monsterTimer > 150)
            {
                if (Random.Range(0, 100) % 9 == 0) { SceneManager.LoadScene("EndScene"); ScoreManager.score -= 500; } // if the moster gets too close, theres a random chance it might kill you
                monsterTimer = 0;
            }
            if (Input.GetKey(KeyCode.Space)) // if you're able to kill the monster
            {
                transform.position = new Vector3(Random.Range(-rangeNeg, rangePos), .87f, Random.Range(-rangeNeg, rangePos));

                transform.GetChild(limbs).gameObject.SetActive(false); //hides limbs

                ScoreManager.score += 200;
                limbs += 1;

                MessageShow.displayMessage = true;

                MessageShow.message = "It's weakened.";
            }

        }
        if (collision.gameObject.tag == "Hostage")
        {
            //Debug.Log("hit hostage");

            //put it double bc I kept getting errors and this stopped it
            if (hostages.Any())
            {
                if (hostages[0] == null) //making sure it still exists
                {
                    hostNum -= 1;
                    hostages.Remove(hostages[0]);
                }
            }
        }
    }

            // Update is called once per frame
            void Update()
    {
        if (hostages.Any())
        {
            if (hostages[0] == null) //making sure it still exists
            {
                hostNum -= 1;
                hostages.Remove(hostages[0]);
            }
        }
        //put it double bc I kept getting errors and this stopped it
        if (hostages.Any())
        {
            for (int i = 1; i < hostNum; i++)
            { //for loop to check
                if (hostages[i] == null) //making sure it still exists
                {
                    hostNum -= 1;
                    hostages.Remove(hostages[i]);
                }
            }
            //finding the nearest hostage
            hostages = hostages.OrderBy(x => Vector3.Distance(x.transform.position, this.transform.position)).ToList();
            closestVector = hostages[0].transform.position;
        }





            //once it runs out of limbs, the player wins
            if (limbs == 5) { SceneManager.LoadScene("EndScene"); }

        //goes to player if closer
        if (ToggleFlashlight.flashOn == false) //only find if flash is off
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) < playerDistance)
            {
                Debug.Log("Coming for you");
                //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.fixedDeltaTime * 15);
                agent.SetDestination(player.transform.position);
                transform.LookAt(player.transform.position);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
            }
        }
        //brings them to the nearest hostage
        else if (Vector3.Distance(closestVector, this.transform.position) < distance)
        {
            //Debug.Log("hostage here");
            agent.SetDestination(closestVector);
            transform.LookAt(closestVector);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        else
        {
            timer++;

            Patrol();

            if (agent.speed == 0) walkPoint = false; //me trying to fix the issue of some of my fish not swimming 


            //every so often it will just randomly teleport
            if (timer % 5000 == 0)
            {
                transform.position = new Vector3(Random.Range(-rangeNeg, rangePos), .87f, Random.Range(-rangeNeg, rangePos));
            }
        }

    }

    void Patrol()
    {
        //have it first pick a location
        if (!walkPoint) SearchForDest();

        //if walkpoint is in a viable location, it will walk towards thta direction
        if (walkPoint) agent.SetDestination(destination);
       // Debug.Log(destination);

        //once it gets close to the end, changes destination
        if (Vector3.Distance(transform.position, destination) < almostThere) walkPoint = false;
    }

    void SearchForDest()
    {
        //sets a random location
        float x = Random.Range(-rangeNeg, rangePos);
        float z = Random.Range(-rangeNeg, rangePos);


        //gives it a new location to patrol / swim to
        destination = new Vector3(x, .87f, z);

        walkPoint = true;
    }
}