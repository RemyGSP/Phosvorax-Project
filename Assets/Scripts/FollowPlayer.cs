using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private GameObject player;
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            this.transform.position = player.transform.position;
        }
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
