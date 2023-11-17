using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Respawn1 : MonoBehaviour
{
    public Transform initialSpawnPoint; // The initial spawn point of the player
    private Transform currentCheckpoint; // The current checkpoint the player will respawn at
    private float playerHeightOffset = 1.0f;
    private PlayerMovement pm;
    public Canvas StartScreen;

    void Start()
    {
        currentCheckpoint = initialSpawnPoint;
        StartScreen.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            StartScreen.gameObject.SetActive(false);
            GameObject player = GameObject.FindGameObjectWithTag("Player2");
            player.transform.position = initialSpawnPoint.position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("checkPoint"))
        {
            SetCheckpoint(collision.transform);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Respawnground"))
        {
            RespawnPlayer();
        }
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public void RespawnPlayer()
    {
        if (currentCheckpoint != null)
        {
            // Reset the player's position to the current checkpoint
            GameObject player = GameObject.FindGameObjectWithTag("Player2");
            float playerYOffset = player.GetComponent<Collider>().bounds.extents.y + playerHeightOffset;
            Vector3 respawnPosition = currentCheckpoint.position + Vector3.up * playerYOffset;

            player.transform.position = respawnPosition;

            // Additional logic for respawning (e.g., reset health, etc.) can be added here
        }
        else
        {
            Debug.LogWarning("No checkpoint set. Respawning at initial spawn point.");
            // If no checkpoint is set, respawn at the initial spawn point
            if (initialSpawnPoint != null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player2");
                player.transform.position = initialSpawnPoint.position;
            }
            else
            {
                Debug.LogError("Initial spawn point not assigned!");
            }
        }
    }
}
