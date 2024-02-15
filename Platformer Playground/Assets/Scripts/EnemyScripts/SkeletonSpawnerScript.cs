using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject skeleton;
    [SerializeField] private bool spawnReady = true;
    [SerializeField] private int spawnAmount = 3;
    // Start is called before the first frame update

    public void ResetSpawner()
    {
        spawnAmount--;
        spawnReady = true;
    }

    private void OnBecameVisible()
    {
        if (spawnReady && spawnAmount > 0)
        {
            Instantiate(skeleton, transform);
            spawnReady = false;
        }
    }
}
