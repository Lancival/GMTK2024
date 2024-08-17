using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bryant {
public class Decoration : MonoBehaviour {
    [field: SerializeField] public int Name { get; private set; }
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public DecoType Type { get; private set; }

    [field: SerializeField] public List<DecoType> ValidAttachToTypes { get; private set; }

    [SerializeField] Transform snapPoint;

    public bool Place(Decoration attachToDeco) {
        if (!ValidAttachToTypes.Contains(attachToDeco.Type)) return false;

        return true;
    }

    // startPoint is a point outside the collider of the object to place the decoration on
    public Vector2 GetSnapPoint(Vector2 startPoint) {
        RaycastHit2D hit = Physics2D.Raycast(startPoint, Vector2.down);
        return hit.collider != null ? new Vector2(hit.point.x, hit.point.y - snapPoint.localPosition.y) : startPoint;
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
}