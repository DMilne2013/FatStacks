﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public Transform boxGrid;
    public Box[] pool;
    public float maxInterval;
    public float minInterval;
    public bool fixedRate;
    public bool startActive;
    private bool on;

    private Coroutine coroutine;

    // Start is called before the first frame update
    void Start()
    {
        TurnSpawnerOn(startActive);
    }

    public void TurnSpawnerOn(bool _on)
    {
        if(on != _on)
        {
            on = _on;
            if (on)
            {
                coroutine = StartCoroutine("SpawnBox");
            }
        }
    }

    IEnumerator SpawnBox()
    {
        System.Random random = new System.Random();
        while (on)
        {
            GameObject obj = Instantiate(pool[random.Next(pool.Length)].gameObject, transform.position, Quaternion.identity, boxGrid);
            obj.SetActive(true);
            if (fixedRate)
                yield return new WaitForSeconds(minInterval);
            else
                yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        }
        StopCoroutine(coroutine);
    }
}