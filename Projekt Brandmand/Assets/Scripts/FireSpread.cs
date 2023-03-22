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

        //Check for collision
        //KODE HER

        //Spread fire
        switch (dir)
        {
            case Dir.N:
                Instantiate(prefab, new Vector3(gameObject.transform.position.x + distance, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
                break;
            case Dir.E:
                Instantiate(prefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + distance), Quaternion.identity);
                break;
            case Dir.S:
                Instantiate(prefab, new Vector3(gameObject.transform.position.x - distance, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
                break;
            case Dir.W:
                Instantiate(prefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - distance), Quaternion.identity);
                break;
            default:
                Debug.Log("No Direction was chosen");
                break;
        }

        //Fire has spread and is no longer spreading
        fireSpreading = false;
        yield return null;
    }
}
