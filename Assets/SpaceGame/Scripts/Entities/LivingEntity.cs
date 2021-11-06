using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] EntityModel modelPrefab;
    EntityModel model;
    Transform modelContainer;


    public EntityModel Model { get { return model; } }
    public event EventHandler ModelSpawned;

    void Awake()
    {
        BuildHierarchy();
    }

    void Start()
    {
        SpawnModel();
    }

    void BuildHierarchy()
    {
        modelContainer = new GameObject("ModelContainer").transform;
        modelContainer.SetParent(transform, false);
    }

    void SpawnModel()
    {
        if (!modelPrefab)
        {
            Debug.LogError("SpawnModel called but no model set");
            return;
        }

        model = GameObject.Instantiate(modelPrefab, Vector3.zero, Quaternion.identity);
        model.transform.SetParent(modelContainer, false);
        ModelSpawned?.Invoke(this, null );

    }

}
