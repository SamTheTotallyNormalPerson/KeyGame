using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WumpaPickUp : MonoBehaviour {

    public int value;
    //public AudioSource wumpapickyupper;
    public float destoryTime;
    public GameObject wumpaMesh;
    public float lookRadius = 10f;
    public GameObject pickupEffect;

   public Transform target;
    public float speed;
	// Use this for initialization
	void Start () {

        
       
	}
	
	// Update is called once per frame
	void LateUpdate () {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //wumpapickyupper.Play();
            FindObjectOfType<GameManager>().AddGold(value);
            Destroy(gameObject);
            Instantiate(pickupEffect, transform.position, transform.rotation);
            
        }
    }

   void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
