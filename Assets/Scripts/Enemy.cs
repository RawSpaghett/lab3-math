using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;


    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
    }

    void FixedUpdate()
    {
        Vector2 direction = player.transform.position - transform.position; //Step 1: gets direction 

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; //Step 2: Calculate angle in radians via rise-over-run with Atan, and calculate back to degrees for actual transformation, subtract 90 degrees for the sprite

        transform.rotation = Quaternion.Euler(0f, 0f, angle); //Step 3:  Apply transformation

    }
}
