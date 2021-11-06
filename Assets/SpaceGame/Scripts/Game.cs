using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform startPoint;
    void Start()
    {
        Instantiate(playerPrefab, startPoint.position, Quaternion.identity);
    }

    
}
