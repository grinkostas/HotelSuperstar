using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

public class GuestSpawner : MonoBehaviour
{
    [SerializeField] private List<Guest> _guestsPrefabs;
    [SerializeField] private float _uniqueGuestSpawnTime;
    [SerializeField] private List<Guest> _uniqueGuests;


    [Inject] private DiContainer _container;
    [Inject] private UpgradesController _upgradesController;
    
    public UnityAction<Guest> GuestSpawned;
    public int TotalSpawnedGuests { get; private set; } = 0;
    private float ArrivalDelay => _upgradesController.GetValue(UpgradeIds.GuestArrivalDelay);
    
    private bool _isSpawningEnabled = true;
    private float _spawnTime = 0.0f;

    private void Update()
    {
        _spawnTime += Time.deltaTime;
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnGuests());
    }

    private IEnumerator SpawnGuests()
    {
        while (_isSpawningEnabled)
        {
            SpawnRandomGuest();
            yield return new WaitForSeconds(ArrivalDelay);
        }
    }

    public Guest SpawnRandomGuest()
    {
        var guestToSpawn = GetGuestToSpawn();
        return CreateGuest(guestToSpawn);
    }

    private Guest GetGuestToSpawn()
    {
        List<Guest> guestList;
        int index;

        if (_spawnTime >= _uniqueGuestSpawnTime)
        {
            _spawnTime = 0.0f;
            index = Random.Range(0, _uniqueGuests.Count);
            guestList = _uniqueGuests;
        }
        else
        {
            index = Random.Range(0, _guestsPrefabs.Count);
            guestList = _guestsPrefabs;
        }
        
        return guestList[index];
    }
    
    private Guest CreateGuest(Guest prefab)
    {
        var guestObject = _container.InstantiatePrefab(prefab, transform);
        Guest guest = guestObject.GetComponent<Guest>();
        TotalSpawnedGuests++;
        GuestSpawned?.Invoke(guest);
        return guest;
    }

}
