using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerWorldTraveller player = collision.gameObject.GetComponent<PlayerWorldTraveller>();
            player.SpawnLocation = tag;
            SceneManager.LoadScene(tag);
        }
    }
}
