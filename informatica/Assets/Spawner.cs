using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject spawnee;
    [SerializeField] float sizeX = 1f;
    [SerializeField] float sizeY = 1f;
    [SerializeField] AnimationCurve spawnCurve;
    
    private float spawnCooldown;
    private float elapsedTime = 0f;

    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        
        spawnTime = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        spawnCooldown = (int)spawnCurve.Evaluate(elapsedTime);
        //Debug.Log(spawnCooldown);

        if (spawnTime > 0) spawnTime -= Time.deltaTime;

        if (spawnTime <= 0)
        {
            Spawn();
            spawnTime = spawnCooldown;
        }
    }

    void Spawn()
    {
        float xPos = (Random.value - 0.5f) * 2 * sizeX;
        float yPos = (Random.value - 0.5f) * 2 * sizeY;

        var spawn = Instantiate(spawnee);

        spawn.transform.position = new Vector3(xPos, yPos, 0);
    }
}
