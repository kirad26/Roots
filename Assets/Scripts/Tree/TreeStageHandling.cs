using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStageHandling : MonoBehaviour
{
    [Header("Player Refernce")]
    [SerializeField] private string playerTreeTag = "Player";

    [SerializeField] private int currentTreeStage = 0;
    [SerializeField] private float maxSunEnergyForStage = 30;
    [SerializeField] private float maxWaterForStage = 30;

    [SerializeField] private float limitAngleForStage = 5;
    [SerializeField] private List<GameObject> playerPrefabsList;
    //public delegate void TreeAction();
    //public static event TreeAction OnTreeNewStage;
    private Transform playerTransform;
    private TreeRotation treeRotation;

    private void Start()
    {
        playerTransform = transform;
        SpawnPlayerTree();
    }

    [ContextMenu("New stage")]
    public void NewTreeStage()
    {
        //Index must be smaller than list count
        if (currentTreeStage == playerPrefabsList.Count - 1)
        {
            return;
        }

        currentTreeStage++;
        GameManager.Instance.IncreaseMaxResources(maxSunEnergyForStage, maxWaterForStage);
        if(treeRotation != null)
        {
            treeRotation.LimitTreeTilting(limitAngleForStage);
        }
        SpawnPlayerTree();
    }
    private void SpawnPlayerTree()
    {
        //Delete old
        foreach (var obj in GameObject.FindGameObjectsWithTag(playerTreeTag))
        {
            Destroy(obj);
        }
        Instantiate(playerPrefabsList[currentTreeStage], playerTransform);
        treeRotation = gameObject.GetComponentInChildren<TreeRotation>();
    }
}