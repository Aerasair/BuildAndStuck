using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private MovingCube _cubePrefab;
    [SerializeField] private MoveDirection _moveDirection;

    public void SpawnCube()
    {
        var cube = Instantiate(_cubePrefab);
        cube.MoveDirection = _moveDirection;


        if (MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameObject.Find("StartCube"))
        {
            float x = _moveDirection == MoveDirection.X ? transform.position.x : MovingCube.LastCube.transform.position.x;
            float z = _moveDirection == MoveDirection.Z ? transform.position.z : MovingCube.LastCube.transform.position.z;
           
            cube.transform.position = new Vector3(x,
                MovingCube.LastCube.transform.position.y + _cubePrefab.transform.localScale.y,
                z);
        }
        else
        {
            cube.transform.position = transform.position;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _cubePrefab.transform.localScale);
    }

}

public enum MoveDirection
{
    Z,
    X
}
