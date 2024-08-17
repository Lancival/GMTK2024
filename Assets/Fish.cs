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

    // How fast the fish will swim
    public float speed;

    // How likely the fish will turn before reaching the wall (0 - 100)
    public float turn_probability;

    // The distance between the actual wall collider and where the fish will turn. A value of 0 means the fish will turn after colliding with the wall 
    public float wall_buffer;

    // The layer where the fish tank is in
    public LayerMask tank_layer;

#endregion

    Vector2 PickDirection()
    {
        Vector2[] directions = { Vector2.left, Vector2.right };
        return directions[Random.Range(0, directions.Length)];
    }

    void Swim()
    {
        // TODO: support non-unit vector directions
        // Calculate distance to tank boundary
        float currentDistance=0;
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(tank_layer);
        var results = new List<RaycastHit2D>();
        int hits = Physics2D.Raycast(transform.position, m_Direction, filter, results, Mathf.Infinity);
        Assert.IsTrue(hits == 1);
        foreach (RaycastHit2D hit in results)
        {
            currentDistance = hit.distance;
        }

        if  (currentDistance < wall_buffer)
        {   
            m_Direction *= -1;
            return;
        }
        else
        {
            // Flip directions if probability is met
            var probability = Mathf.Clamp(turn_probability, 0, 100);
            var roll = Random.Range(0, 100);
            if (probability >= roll)
            {
                m_Direction *= -1; 
            }
        }
    }

    void Start()
    {
        // Assign initial direction where the fish will swim
        m_Direction = PickDirection();
    }

    void FixedUpdate()
    {
        Swim();
    }
}
