using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float timeBtwSpawns;
    public float startTimeBtwSpawns;

	private int timesSpawned;

    public GameObject[] characters;

    private void Start()
    {
        
        timeBtwSpawns = startTimeBtwSpawns;
        
    }

    private void Update()
    {
        
        
        if (timeBtwSpawns <= 0)
        {
            int rand = Random.Range(0, characters.Length);
            Instantiate(characters[rand], transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
            startTimeBtwSpawns = timeBtwSpawns;
			timesSpawned++;
			if(timesSpawned % 3 == 0 && timesSpawned > 0 ) {
				if(startTimeBtwSpawns > 1) {
                    startTimeBtwSpawns -= 0.5f;
                }
                
			}
        }
        else {
            timeBtwSpawns -= Time.deltaTime;
        }
    }

}
