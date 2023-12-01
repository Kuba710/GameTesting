using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTools : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Transform targetTransform;
    [SerializeField] int numRows = 3;
    [SerializeField] int numColumns = 3;
    [SerializeField] float distanceBetweenSquares = 1.0f;

    [Button]
    void GenerateBoard()
    {
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numColumns; j++)
            {
                float posX = j * distanceBetweenSquares;
                float posZ = i * distanceBetweenSquares;

                Vector3 position = new Vector3(posX, posZ);
                Instantiate(tilePrefab, position, Quaternion.identity, transform);
            }
        }
    }
}