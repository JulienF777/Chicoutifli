using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ennemyPrefab;
    [SerializeField] private int _spawnNumber = 1;
    [SerializeField] private float _ennemyOffset = 1.0f;
    [SerializeField] private GameObject pickUpItem;

    public int ennemyNumber = 0;
    private List<Vector3> _positions;

    private bool listOfEnnemyIsSet = false;
    // Start is called before the first frame update
    void Start()
    {
        spawnEnnemies();
        pickUpItem.SetActive(false);
    }

    private void Update()
    {
        if(ennemyNumber == 0 && listOfEnnemyIsSet)
        {
            pickUpItem.SetActive(true);
        }
    }

    private void spawnEnnemies()
    {
        _positions = new List<Vector3>();
        Vector3 scale = transform.localScale;
        Vector3 position = transform.position;

        //spawn ennemies
        for (int i = 0; i < _spawnNumber; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition(scale, position);
            _positions.Add(spawnPosition); // Add the new position to the list of occupied positions
            // Instantiate the ennemy at the spawn position
            GameObject ennemy = Instantiate(_ennemyPrefab, spawnPosition, Quaternion.identity);
            ennemy.transform.GetChild(0).GetComponent<AIController>().setSpawner(gameObject);
            ennemyNumber++;
        }
        listOfEnnemyIsSet = true;
    }

    private Vector3 GetValidSpawnPosition(Vector3 scale, Vector3 position)
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-scale.x / 2, scale.x / 2), 0, Random.Range(-scale.z / 2, scale.z / 2));

            // Check if the position is already taken
            bool positionIsValid = true;
            foreach (Vector3 occupiedPosition in _positions)
            {
                // Check if the distance between the new position and the occupied position is greater than the sum of their offsets
                if (Vector3.Distance(spawnPosition, occupiedPosition) < _ennemyOffset * 2)
                {
                    positionIsValid = false;
                    break;
                }
            }

            if (positionIsValid)
                return spawnPosition;
        }
    }

}
