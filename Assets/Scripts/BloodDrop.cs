using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BloodDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject blood;

    private GameObject player;

    public static int totalBlood = 300;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        { 
            Instantiate(blood, new Vector3(player.transform.position.x, .0000009536743f, player.transform.position.z), Quaternion.identity);
            totalBlood--;
        }

        if (totalBlood == 30)
        {
            MessageShow.displayMessage = true;

            MessageShow.message = "You don't have much blood left.";
        }

        if (totalBlood <= 0) { SceneManager.LoadScene("EndScene"); }
    }
}
