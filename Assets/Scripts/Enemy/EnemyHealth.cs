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
    private GenerateRooms generator;
    
    void Start()
    {
        PlayerController playerInScene = GameObject.Find("Player").GetComponent<PlayerController>();
        if(playerInScene != null) {
            player = playerInScene;
        }
        
        GenerateRooms generatorInScene = GameObject.Find("Generation").GetComponent<GenerateRooms>();
        if(generatorInScene != null) {
            generator = generatorInScene;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // destroy if health is zero
        if (health < 1)
        {
            generator.enemies.Remove(transform.parent.gameObject);
            Destroy(transform.parent.gameObject);
        }


        timer += Time.deltaTime;
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange && timer > 1/attacksPerSecond)
        {
            // take away player health
            player.health -= 1;
            // set display of player health
            if (player.health > 0) player.healthString.text = "Health: " + player.health;
            else player.healthString.text = "Health: dead";
            
            timer = 0;
        }
    }
}
