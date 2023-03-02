using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attacksPerSecond;
    
    public int health;
    private float timer = 0;
    private PlayerController player;
    
    void Start()
    {
        PlayerController playerInScene = GameObject.Find("Player").GetComponent<PlayerController>();
        if(playerInScene != null) {
            player = playerInScene;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // destroy if health is zero
        if (health < 1) Destroy(gameObject);


        timer += Time.deltaTime;
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange && timer > 1/attacksPerSecond)
        {
            // take away player health
            player.health -= 1;
            // set display of player health
            if (player.health > 0) player.healthString.text = "Health: " + player.health;
            else player.healthString.text = "Health: dead";
            
            // Do death display if necessary
            if (player.health < 1) player.Kill();
            
            timer = 0;
        }
    }
}
