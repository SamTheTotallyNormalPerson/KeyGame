using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    public PlayerController thePlayer;

    public float invincabiltiyLength;
    public float invincabilityCounter;

    public Renderer playerRendeer;
    public float flashCounter;
    public float flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;

    public GameObject deathEffect;

    public Image deathFade;
    private bool isFadeTo;
    private bool isFadeFrom;
    public float fadeSpeed;
    public float waitForFade;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        thePlayer = FindObjectOfType<PlayerController>();

        respawnPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincabilityCounter > 0)
        {
            invincabilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter > 0)
            {
                playerRendeer.enabled = !playerRendeer.enabled;
                flashCounter = flashLength;
            }

            if (invincabilityCounter <= 0)
            {
                playerRendeer.enabled = true;
            }
        }

        if (isFadeTo)
        {
            deathFade.color = new Color(deathFade.color.r, deathFade.color.g, deathFade.color.b, Mathf.MoveTowards(deathFade.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (deathFade.color.a == 1f)
            {
                isFadeTo = false;
            }
        }

        if (isFadeFrom)
        {
            deathFade.color = new Color(deathFade.color.r, deathFade.color.g, deathFade.color.b, Mathf.MoveTowards(deathFade.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (deathFade.color.a == 00f)
            {
                isFadeFrom = false;
            }
        }
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        if (invincabilityCounter <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Respawn();
            }
            else
            {

                thePlayer.KnockBack(direction);

                invincabilityCounter = invincabiltiyLength;

                playerRendeer.enabled = false;

                flashCounter = flashLength;
            }
        }
    }


    public void Respawn()
    {
        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
    }
    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);
        Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(respawnLength);

        isFadeTo = true;

        yield return new WaitForSeconds(waitForFade);

        isFadeTo = false;
        isFadeFrom = true;

        isRespawning = false;

        thePlayer.gameObject.SetActive(true);

        GameObject player = GameObject.Find("Player");
        CharacterController charController = thePlayer.GetComponent<CharacterController>();
        charController.enabled = false;
        thePlayer.transform.position = respawnPoint;
        charController.enabled = true;

        currentHealth = maxHealth;

        invincabilityCounter = invincabiltiyLength;
        playerRendeer.enabled = false;
        flashCounter = flashLength;
    }


    public void HealPlayer(int healthAmount)
    {
        currentHealth += healthAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
}

