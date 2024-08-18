using System;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour {
    [field: SerializeField] public int Name { get; private set; }
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public DecoType Type { get; private set; }

    [field: SerializeField] public List<DecoType> ValidAttachToTypes { get; private set; }

    [SerializeField] Transform snapPoint;
    [SerializeField] LayerMask decoLayer;
    
    [field: SerializeField] public float Space { get; private set; }
    [field: SerializeField] public float Cleanliness { get; private set; }

    public bool CanPlace(Decoration attachToDeco) {
        return ValidAttachToTypes.Contains(attachToDeco.Type);
    }

    // startPoint is a point outside the collider of the object to place the decoration on
    /// <summary>
    /// Returns world point of decoration's snap position
    /// </summary>
    /// <param name="startPoint">A point outside the collider of the object to place the decoration on</param>
    public Vector2 GetSnapPoint(Vector2 startPoint) {
        RaycastHit2D hit = Physics2D.Raycast(startPoint, Vector2.down, 100f, decoLayer);
        return hit.collider != null ? new Vector2(startPoint.x, hit.point.y - snapPoint.position.y) : startPoint;
    }
}

public enum DecoType {
    None = 0,
    Rock = 1,
    Sand = 2,
    Gravel = 3,
    Wood = 4,
    Organic = 5,
    Machine = 6,
}
