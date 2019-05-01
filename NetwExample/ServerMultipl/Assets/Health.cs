using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
    public bool destroyOnDeath;
    public const int maxHealth = 100;
    public RectTransform healthBar; 
    [SyncVar(hook ="OnChangeHealth")]public int currentHealth = maxHealth;

    private NetworkStartPosition[] spawnPoints;
    private void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    public void TakeDamage(int amount) {
        if (!isServer) { 
            return; }
        currentHealth -= amount;
        if (currentHealth <= 0) {
            if (destroyOnDeath){
                Destroy(gameObject);
            }else{
                currentHealth = maxHealth;
                RpcRespawn();
            }
            //currentHealth = 0;

            Debug.Log("Dead!");
        }
       // healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }
    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }
    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            Vector3 spawnPoint = Vector3.zero;
            if(spawnPoints!=null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }
            //move back to zero location
            transform.position = Vector3.zero;
        }
    }

}
