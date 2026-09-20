using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [Header("Orbit Settings")]
    [SerializeField] private float radius = 5f;
    [SerializeField] private float orbitSpeed = 2f;

    private float orbitAngle;

    void Start()
    {
        Init();
    }

    void FixedUpdate()
    {
        LookAt();
        Circle();
    }

    private void LookAt()
    {
        Vector2 direction = player.transform.position - transform.position; //Step 1: gets direction 

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; //Step 2: Calculate angle in radians via rise-over-run with Atan, and calculate back to degrees for actual transformation, subtract 90 degrees for the sprite

        transform.rotation = Quaternion.Euler(0f, 0f, angle); //Step 3:  Apply transformation
    }

    private void Circle()
    {
        // calculate the distance between the enemy and the player
        Vector3 distance = transform.position - player.transform.position;
        float distanceSquared = distance.sqrMagnitude;

        // adjust orbit speed based on distance
        float currentOrbitSpeed = orbitSpeed + distanceSquared * 0.1f;

        //increases the angle over time
        orbitAngle += currentOrbitSpeed * Time.deltaTime;
        
        //finding the x and y position every call, cosine tells us the x position just by having the angle, and sin does the same for y
        float x = Mathf.Cos(orbitAngle) * radius;
        float y = Mathf.Sin(orbitAngle) * radius;

        Vector3 newPosition = new Vector3(x,y,0);

        transform.position = newPosition;
    }

    private void Init()
    {
        player = GameObject.FindWithTag("Player"); 

        //calculate enemy distance relative to player
        Vector3 enemyPos = transform.position - player.transform.position;

        //takes the enemyPosition offset and returns a radian that tell us the starting angle that the enemy will begin to rotate around us from
        orbitAngle = Mathf.Atan2(enemyPos.y,enemyPos.x);

        radius = Random.Range(2f,6f);
        orbitSpeed = Random.Range(0.5f,1f);
    }
}
