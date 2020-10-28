using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;

    static PlayerCharacterController player = null;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = Instantiate(playerPrefab, transform.position, transform.rotation).GetComponent<PlayerCharacterController>();
        }
    }
}
