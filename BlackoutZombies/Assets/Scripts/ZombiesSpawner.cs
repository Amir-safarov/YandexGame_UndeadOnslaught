using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombiesSpawner : MonoBehaviour
{
    public int ZombieSpawnCounter
    {
        get
        {
            if (zombieSpawnCounter >= _zombiesList.Count)
                zombieSpawnCounter = 0;
            return zombieSpawnCounter;
        }
        set { zombieSpawnCounter = value; }
    }

    [SerializeField] private GameObject _zombiePrefabType1;
    [SerializeField] private GameObject _zombiePrefabType2;
    [SerializeField] private Transform _zombiesOnScene;
    [SerializeField] private List<GameObject> _zombiesList;
    [SerializeField] private List<Transform> _spawnPointList;
    [SerializeField] private float _spawnInterval;

    int zombieSpawnCounter = 0;

    private void Awake()
    {
        EventManager.PlayerDeathEvent.AddListener(DestroyZombiesList);
    }

    private void Start()
    {
        for (int i = 0; i < _spawnPointList.Count; i++)
            _spawnPointList[i] = transform.GetChild(i);
        for (int i = 0; i < _zombiesList.Count; i++)
            _zombiesList[i] = _zombiesOnScene.GetChild(i).gameObject;
    }

    public void DestroyZombiesList()
    {
        foreach (var zombie in _zombiesList)
            zombie.SetActive(false);
        StopAllCoroutines();
    }

    public void StartZombiesSpawn() =>
        StartCoroutine(FirstWaveSpawn());

    private IEnumerator FirstWaveSpawn()
    {
        while (true)
        {
            if (_zombiesList[ZombieSpawnCounter].activeSelf)
                yield return null;
            else
            {
                SpawnZombies(_zombiesList[ZombieSpawnCounter]);
                ZombieSpawnCounter++;
                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }

    private void SpawnZombies(GameObject _zombie)
    {
        int spawnPointIndex = Random.Range(0, _spawnPointList.Count);
        _zombie.transform.position = _spawnPointList[spawnPointIndex].position;
        _zombie.gameObject.SetActive(true);
    }
}
