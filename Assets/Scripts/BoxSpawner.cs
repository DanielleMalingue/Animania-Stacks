using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab;

    public void SpawnBox()
    {
        GameObject Duck_1 = Instantiate(boxPrefab);
        Duck_1.transform.position = transform.position;


    }


}
