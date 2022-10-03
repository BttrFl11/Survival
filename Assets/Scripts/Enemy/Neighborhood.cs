using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighborhood : MonoBehaviour
{
    private List<Neighborhood> neighbors = new();

    // Returns the neighbors average position, if neighbors count equal zero retruns Vector3.zero
    public Vector3 AVGPosition
    {
        get
        {
            Vector3 avgPos = Vector3.zero;

            if (neighbors.Count == 0)
                return avgPos;

            for (int i = 0; i < neighbors.Count; i++)
                avgPos += neighbors[i].transform.position;

            avgPos /= neighbors.Count;

            return avgPos;
        }
    }

    // Adds the neighbor in list
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Neighborhood neghbor))
            neighbors.Add(neghbor);
    }

    // Removes the neighbor from list
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Neighborhood neighbor))
            neighbors.Remove(neighbor);
    }
}
