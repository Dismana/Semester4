using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpread : MonoBehaviour
{
    //Prefab for fire
    public GameObject prefab;
    
    //Fire spreading and directions
    private enum Dir {N, E, S, W};
    private Dir dir;
    private bool fireSpreading;

    //Variables to modify the fire behavior
    public Vector3 minBoundaries;
    public Vector3 maxBoundaries;
    public int minTime;
    public int maxTime;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        //Get random direction
        dir = chooseRandomDirection(dir);

        //Spread Fire if not already
        if (!fireSpreading)
        {
            StartCoroutine(spreadFire());
        }
    }

    //Choose a random direction
    Dir chooseRandomDirection(Dir dir)
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                dir = Dir.N;
                break;
            case 1:
                dir = Dir.E;
                break;
            case 2:
                dir = Dir.S;
                break;
            case 3:
                dir = Dir.W;
                break;
            default:
                break;
        }
        
        return dir;
    }

    IEnumerator spreadFire()
    {
        //Fire is spreading
        fireSpreading = true;

        //Get random spreadtime
        int rand = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(rand);

        //Spread fire
        switch (dir)
        {
            case Dir.N:
                 SpawnFire(prefab, new Vector3(gameObject.transform.position.x + distance, gameObject.transform.position.y, gameObject.transform.position.z));
                 break;
            case Dir.E:
                SpawnFire(prefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + distance));
                break;
            case Dir.S:
                SpawnFire(prefab, new Vector3(gameObject.transform.position.x - distance, gameObject.transform.position.y, gameObject.transform.position.z));
                break;
            case Dir.W:
                SpawnFire(prefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - distance));
                break;
            default:
                Debug.Log("No Direction was chosen");
                break;
        }

        //Fire has spread and is no longer spreading
        fireSpreading = false;
        yield return null;
    }

    void SpawnFire(GameObject prefab, Vector3 spawnPoint)
    {
        //Checks if fire is within boundaries
        if (minBoundaries.x > spawnPoint.x || maxBoundaries.x < spawnPoint.x || minBoundaries.y > spawnPoint.y || maxBoundaries.y < spawnPoint.y || minBoundaries.z > spawnPoint.z || maxBoundaries.z < spawnPoint.z)
        {
            Debug.Log("Spawn failed, Out of Bounds");
        } else {
            //Checks for colliders at the spawnpoint
            var hitColliders = Physics.OverlapSphere(spawnPoint, 0.01f);

            //Checks for collision, if none is present then spawns fire
            if (hitColliders.Length > 0.01)
            {
                Debug.Log("Spawn " + spawnPoint.x + ", " + spawnPoint.y + ", " + spawnPoint.z + " Occupied.");
            }
            else
            {
                Debug.Log("Fire Spawned at " + spawnPoint.x + ", " + spawnPoint.y + ", " + spawnPoint.z);
                Instantiate(prefab, spawnPoint, Quaternion.identity);
            }
        }
    }
}
