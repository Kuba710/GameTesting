using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Singleton<ItemSpawner>
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Pickable pickableItemPrefab;

    public void SpawnItem(Item item)
    {
        Pickable pickableItem = Instantiate(pickableItemPrefab, transform);
        pickableItem.transform.position = playerTransform.position + offset;
        pickableItem.Item = item;
        pickableItem.SetupSpriteRenderer();
        pickableItem.gameObject.SetActive(true);
    }
}
