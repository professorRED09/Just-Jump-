using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlatform : MonoBehaviour
{
    public static RespawnPlatform Instance;

    [SerializeField] GameObject platform;   

    private void Awake()
    {
        Instance = this;
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnPlatform(2));
    }
    private IEnumerator SpawnPlatform(float waitTime)
    {
        platform.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        platform.SetActive(true);        
    }
}
