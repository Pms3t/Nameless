using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    [SerializeField]
    private int _killGoal = 0;
    [SerializeField] 
    private int _currentKills = 0;
    [SerializeField]
    int _loopCount = 0;
    [SerializeField]
    private int _waitTime = 10;
    [SerializeField]
    private bool _triggered = false;

    // for testing
    public bool addone = false;

    private float tempTime;

    // components
    private ManageDoors _MD;
    private Counter _killCounter;
    private SpawnEnemies _spawnEnemies;

    // Update is called once per frame
    void Start()
    {
        _MD = GetComponent<ManageDoors>();
        _killCounter = GetComponent<Counter>();
        _spawnEnemies = GetComponent<SpawnEnemies>();
    }

    void Update()
    {
        if(_triggered)
            Room02Event();
    }

    public void Room02Event()
    {
        float time = Time.time;

        // After player have triggered a trigger start closing doors and reveal kill count ui
        if (_killCounter.ReturnCounter() == 0)
            _MD.CloseDoors();

        if (_killGoal == 0)
            _killGoal = 5;

        // that should be displayed on the door as "hologram" (if time).
        // After this spawn enemies.
        if (time >= _waitTime)
            _spawnEnemies.SpawnEnemy(_loopCount - 1, _killGoal);
           // _spawnEnemies._spawningOn = true;

        // When player has killed enough enemies -> open the door. Repeat for the amount of doors.
        if (_killCounter.ReturnCounter() == _killGoal)
        {
            _MD.OpenSingleDoor(_MD.GetDoor(_loopCount - 1));
            _spawnEnemies.ChangeSelectedSpawnArea(_loopCount - 1);
            _loopCount++;

            if(_killGoal < 20)
                IncreaseKillGoal(_killGoal*2);

            else if (_killGoal <= 20)
                _spawnEnemies.SpawnEnemy(_loopCount - 1, _killGoal - _currentKills);

            else
                return;
        }

        // Only for testing
        if (addone)
        {
            _killCounter.AddToCounter(1);
            addone = false;
        }

        // Whenever enemy is destroyed add + 1 to counter.

    }

    public void SetTriggered(bool value)
    {
        _triggered = value;
    }

    public void IncreaseCurrentKills()
    {
        _currentKills++;
    }

    public void IncreaseKillGoal(int increaseTo)
    {
        _killGoal = increaseTo;
    }
}
