using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject slime;
    [SerializeField] GameObject slimegreen;
    [SerializeField] GameObject slimeboss;
    [SerializeField] float sizeX = 1f;
    [SerializeField] float sizeY = 1f;
    [SerializeField] AnimationCurve spawnCurve;
    
    private float spawnCooldown;
    private float bossCooldown;
    private float elapsedTime = 0f;

    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 1f;
        bossCooldown = 10;
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        bossCooldown -= Time.deltaTime;
        spawnCooldown = (int)spawnCurve.Evaluate(elapsedTime);
        //Debug.Log(spawnCooldown);

        if (spawnTime > 0) spawnTime -= Time.deltaTime;

        if(bossCooldown <=0){
            GameObject boss = Instantiate(slimeboss);
            boss.transform.position = new Vector3(0, 0, 0);  // Set position to (0, 0, 0)
            bossCooldown = 100000000000f;
        }

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

        GameObject selectedSlime = Random.Range(0, 2) == 0 ? slime : slimegreen;
        var spawn = Instantiate(selectedSlime);
        

        spawn.transform.position = new Vector3(xPos, yPos, 0);
    }
}
