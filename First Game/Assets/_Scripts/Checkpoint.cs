using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour

{
    public HealthManager theHealthman;
    // Start is called before the first frame update
    void Start()
    {
        theHealthman = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            theHealthman.SetSpawnPoint(transform.position);
        }
    }
}
