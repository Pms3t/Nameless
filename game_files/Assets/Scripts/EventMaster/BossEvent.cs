using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    [SerializeField] private GameObject _theBoss;
    [SerializeField] private GameObject _platforms;
    [SerializeField] private bool _movePlatformsUp = false;
    [SerializeField] private Vector3 _platformPosition;
    private Animator _animator;

    [SerializeField] private GameObject _player;
    [SerializeField] private bool _letItRainOn = false;
    [SerializeField] private GameObject _attackModels;

    // cooldown checks
    private float _lastSpawn;

    // Spawning rate
    [SerializeField] [Range(0.1f, 5)] private float _spawningRate = 1;

    // temps
    public int _guardCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _platformPosition = _platforms.transform.position;
        _platforms.transform.position = new Vector3(194, -60, 423);
        _animator = _theBoss.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_movePlatformsUp)
            MovePlatformsUp(_platformPosition);
        else if (!_movePlatformsUp)
            MovePlatformsDown(_platformPosition);

        if (_letItRainOn)
        {
            if (Time.time - _lastSpawn >= _spawningRate)
            {
                SpawnObjectAboveSomeone(_player);
                _lastSpawn = Time.time;
            }
        }

        // If all guards have been killed activate boss
        if (_guardCount == 2)
            _theBoss.GetComponent<BossInteraction>().ActivateBoss();
    }

    // Move platforms up...
    private void MovePlatformsUp(Vector3 moveHere)
    {
        if (_platforms.transform.position.y < moveHere.y)
            _platforms.transform.position = MoveUp(2);
    }

    // ...and down
    private void MovePlatformsDown(Vector3 moveHere)
    {
        if (_platforms.transform.position.y > moveHere.y - 10)
            _platforms.transform.position = MoveDown(3.5f);
    }

    private Vector3 MoveUp(float speed)
    {
        return _platforms.transform.position = new Vector3(_platforms.transform.position.x,
            _platforms.transform.position.y + speed * Time.deltaTime, _platforms.transform.position.z);
    }

    private Vector3 MoveDown(float speed)
    {
        return _platforms.transform.position = new Vector3(_platforms.transform.position.x,
            _platforms.transform.position.y - speed * Time.deltaTime, _platforms.transform.position.z);
    }

    // Fall down objects to player's position
    private void SpawnObjectAboveSomeone(GameObject target)
    {
        Vector3 abovePlayer = new Vector3(target.gameObject.transform.position.x,
            17, target.gameObject.transform.position.z);
        SpawnObject(_attackModels, abovePlayer);
    }

    // Spawn an object
    private void SpawnObject(GameObject spawnableObject, Vector3 position)
    {
        Instantiate(spawnableObject, position, Quaternion.identity);
    }

    public void ToggleLetItRain()
    {
        if (_letItRainOn)
            _letItRainOn = false;

        else if (!_letItRainOn)
            _letItRainOn = true;
    }

    public bool GetLetItRainValue()
    {
        return _letItRainOn;
    }

    public void TogglePlatformMovement()
    {
        if (!_movePlatformsUp)
            _movePlatformsUp = true;
        else
            _movePlatformsUp = false;
    }

    public bool GetPlatformBoolValue()
    {
        return _movePlatformsUp;
    }

    public void ChangeSpawningRate(float value)
    {
        _spawningRate = value;
    }
}
