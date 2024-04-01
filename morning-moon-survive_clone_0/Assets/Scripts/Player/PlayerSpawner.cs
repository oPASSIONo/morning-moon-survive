using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Reference to the player prefab

    public void SpawnPlayer(Vector3 position)
    {
        // Instantiate the player prefab
        GameObject playerObject = Instantiate(playerPrefab, position, Quaternion. identity);

        // Set the position of the player prefab
        playerObject.transform.position = position;
    }
}