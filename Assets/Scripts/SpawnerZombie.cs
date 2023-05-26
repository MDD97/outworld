using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerZombie : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
               float rng = Random.Range(0f, 5f);
                 go = Instantiate(zombie, new Vector3(zombie.transform.position.x, zombie.transform.position.y, zombie.transform.position.z), Quaternion.identity);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
