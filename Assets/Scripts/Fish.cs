using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class Fish : MonoBehaviour
{
#region components

    Rigidbody2D m_RigidBody2D;
    Vector2 m_Direction;

#endregion

#region parameters
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string FlavorText { get; private set; }
    
    [field: SerializeField] public float Space { get; private set; }
    [field: SerializeField] public float Cleanliness { get; private set; }
        
    [field: SerializeField] public IdealRange SpaceRange { get; private set; }
    [field: SerializeField] public IdealRange CleanlinessRange { get; private set; }

    // How fast the fish will swim
    public float speed;

    // How likely the fish will turn before reaching the wall (0 - 100)
    public float turn_probability;

    // How frequent an impulse force is applied to the fish
    public float impulse_probability;

    // How long a fish's burst swim lasts
    public float burst_duration;

    // How many seconds until we check if the fish will swim
    public float swim_frequency;

    // The distance between the actual wall collider and where the fish will turn. A value of 0 means the fish will turn after colliding with the wall 
    public float wall_buffer;

    // The layer where the fish tank is in
    public LayerMask tank_layer;

#endregion

#region members
    float elapsed_time;
    bool is_swimming;

#endregion

    void Start()
    {
        m_RigidBody2D = GetComponent<Rigidbody2D>();

        // Assign initial direction where the fish will swim
        m_Direction = PickDirection();
        elapsed_time = 0;
        is_swimming = false;
    }

    public void Init(StatsDatabase.StatItem statItem) {
        Name = statItem.name;
        Sprite = statItem.sprite;
        Space = int.Parse(statItem.space);
        Cleanliness = int.Parse(statItem.waterQuality);
        // TODO: Flavortext

        GetComponent<SpriteRenderer>().sprite = Sprite;
    }

    void FixedUpdate()
    {
        CheckCollisions();
        
        elapsed_time+= Time.fixedDeltaTime;
        if ( elapsed_time > swim_frequency )
        {
            SwimCheck();
            elapsed_time = 0;
        }
    }

    Vector2 PickDirection()
    {
        Vector2[] directions = { Vector2.left, Vector2.right };
        return directions[Random.Range(0, directions.Length)];
    }

    void TurnCheck()
    {
        // Flip directions if probability is met
        var probability = Mathf.Clamp(turn_probability, 0, 100);
        var roll = Random.Range(1, 100);
        if (probability >= roll)
        {
            m_Direction *= -1; 
        }
    }

    void SwimCheck()
    {
        var swim_roll = Random.Range(1, 100);
        var swim_probability = Mathf.Clamp(impulse_probability, 0, 100);
        if (swim_probability >= swim_roll)
        {
            if (!is_swimming)
            {
                StartCoroutine(Swim());
            }
        }
    }

    void CheckCollisions()
    {
        // TODO: support non-unit vector directions
        // Calculate distance to tank boundary
        float currentDistance=0;
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(tank_layer);
        var results = new List<RaycastHit2D>();
        int hits = Physics2D.Raycast(transform.position, m_Direction, filter, results, Mathf.Infinity);
        foreach (RaycastHit2D hit in results)
        {
            currentDistance = hit.distance;
        }

        if (currentDistance <= wall_buffer)
        {
            m_RigidBody2D.velocity = new Vector2(0f,0f);
            m_Direction *= -1;
        }
    }

    IEnumerator Swim()
    {
        TurnCheck();
        
        float elapsedTime = 0f;
        while (elapsedTime < burst_duration)
        {
            is_swimming = true;

            // Apply force in the current direction
            m_RigidBody2D.AddForce(speed * m_Direction);

            // Wait for the next physics frame
            yield return new WaitForFixedUpdate();

            // Accumulate elapsed time
            elapsedTime += Time.fixedDeltaTime;
        }

        is_swimming = false;
    }
}
