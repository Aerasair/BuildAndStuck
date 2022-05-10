using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CubeSpawner _cubeSpawner;
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();

            _cubeSpawner.SpawnCube();
        }
    }
}
