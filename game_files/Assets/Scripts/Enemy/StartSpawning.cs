using UnityEngine;
using Random = UnityEngine.Random;

public class StartSpawning : MonoBehaviour
{
    [SerializeField]
    private bool _startSpawning = false;
    [SerializeField]
    private GameObject[] _positions;
    [SerializeField] 
    private GameObject[] _spawnableObjects;

    private float _lastSpawn = 0;
    [SerializeField]
    private float _spawnRate = 1;

    // Update is called once per frame
    void Update()
    {
        if (_startSpawning)
        {
            if (Time.time - _lastSpawn >= _spawnRate)
            {
                foreach (GameObject pos in _positions)
                {
                    int ranNum = Random.Range(0, _spawnableObjects.Length);
                    Instantiate(_spawnableObjects[ranNum], pos.transform.position, pos.transform.rotation);
                }

                _lastSpawn = Time.time;
            }
        }
    }

    public void ToggleStartSpawningValue()
    {
        if (_startSpawning)
            _startSpawning = false;

        else if (!_startSpawning)
            _startSpawning = true;
    }

    public bool GetStartSpawningProjectiles()
    {
        return _startSpawning;
    }

    public void ChangeSpawnRate(float value)
    {
        _spawnRate = value;
    }

    public bool GetStartSpawningValue()
    {
        return _startSpawning;
    }
}
