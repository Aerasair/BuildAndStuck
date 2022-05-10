using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get;private set; }
    public static MovingCube LastCube { get;private set; }

    [SerializeField] private float _moveSpeed = 1f;

    private Color _colorObject;


    private void OnEnable()
    {
        if (LastCube == null)
            LastCube = GameObject.Find("StartCube").GetComponent<MovingCube>(); 

        CurrentCube = this;
        _colorObject = GetRandomColor();
        GetComponent<Renderer>().material.color = _colorObject;

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    public void Stop()
    {
      

        _moveSpeed = 0f;
        float hangover = transform.position.z - LastCube.transform.position.z;

        if(Mathf.Abs(hangover) >= LastCube.transform.localScale.z)
        {
            SceneManager.LoadScene(0);
        }

        float direction = hangover > 0 ? 1f : -1f; 
        SplitCubeOnZ(hangover, direction);

        LastCube = this;
    }

    private void SplitCubeOnZ(float hangover, float direction)
    {
        float newSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newSize;

        float newZPosition = LastCube.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);


        float CubeEdge = transform.position.z + (newSize / 2f * direction);
        float fallingBlockZPosition = CubeEdge + fallingBlockSize / 2 * direction;


        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y,fallingBlockSize);
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = _colorObject;

        Destroy(cube.gameObject, 1f);
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _moveSpeed;
    }
}
