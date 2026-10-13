using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    [SerializeField]
    GameObject cube;

    [SerializeField]
    Transform spawnPoint;

    public void Spawn()
    {
        Instantiate(cube, spawnPoint);
    }
}
