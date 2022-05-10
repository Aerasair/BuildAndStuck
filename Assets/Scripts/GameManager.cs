using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] CubeSpawner[] _cubeSpawner;

    public event UnityAction IsCubeSpawned;

    private int _spawnerIndex;
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();

            _spawnerIndex = _spawnerIndex == 0 ? 1 : 0;
            _cubeSpawner[_spawnerIndex].SpawnCube();
            IsCubeSpawned.Invoke();
        }
    }
}
