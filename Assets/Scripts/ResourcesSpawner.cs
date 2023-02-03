using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    // for debug :
    public string Name;

    [SerializeField] public GameObject Prefab;
    [Range(0f, 100f)] public float Chance = 100f;
    [HideInInspector] public double _weight;
}
public class ResourcesSpawner : MonoBehaviour
{
    [SerializeField] private Item[] items;
    [SerializeField] private GameObject itemAreaSpawn;

    private double accumulatedWeights;
    private System.Random rand = new System.Random();


    private void Awake()
    {
        CalculateWeights();
    }


    private void Start()
    {
        for (int i = 0; i < 10; i++)
            SpawnRandomItem(new Vector2(Random.Range(-30f, 30f), Random.Range(-20f, 0f)));
    }

    private void SpawnRandomItem(Vector2 position)
    {
        Item randomItem = items[GetRandomItemIndex()];

        Instantiate(randomItem.Prefab, position, Quaternion.identity, transform);

        //Debug.Log("<color=" + randomItem.Name + ">●</color> Chance: <b>" + randomItem.Chance + "</b>%");
    }

    private int GetRandomItemIndex()
    {
        double r = rand.NextDouble() * accumulatedWeights;

        for (int i = 0; i < items.Length; i++)
            if (items[i]._weight >= r)
                return i;

        return 0;
    }

    private void CalculateWeights()
    {
        accumulatedWeights = 0f;
        foreach (Item enemy in items)
        {
            accumulatedWeights += enemy.Chance;
            enemy._weight = accumulatedWeights;
        }
    }
}
