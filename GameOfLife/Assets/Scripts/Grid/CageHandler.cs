using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CageHandler : MonoBehaviour
{
    [SerializeField] private int cubeSide;
    [SerializeField] private GameObject cubePrefab;
    [Range(0.1f, 0.7f)]
    [SerializeField] private float randomValue;
    private Transform transform;
    private int aliveCount;
    private Dictionary<Vector3, Cube> cubes;

    private void Start()
    {
        transform = GetComponent<Transform>();
        cubes = new Dictionary<Vector3, Cube>();
        aliveCount = 0;
        CreateCage();
    }

    private void Print()
    {
        foreach(KeyValuePair<Vector3, Cube> entry in cubes)
        {
            Debug.Log(entry.Key.ToString() + "num:" + entry.Value.numOfNeighbours );
        }
    }
    public void CreateCage()
    {
        //ResizeCellSize();
        for (int x = 0; x < cubeSide; x++)
        {
            for (int y = 0; y < cubeSide; y++)
            {
                for (int z =  0; z < cubeSide; z++)
                {
                    GameObject spawnedCube = Instantiate(cubePrefab, this.transform);
                    Vector3 pos = new Vector3(x, y, z);
                    Cube cube = spawnedCube.GetComponent<Cube>();
                    cube.Init(pos);

                    cubes[pos] = cube;
                }
            }
        }

    }

    public void FillCage()
    {
        ClearCage();
        foreach (KeyValuePair<Vector3, Cube> entry in cubes)
        {
            bool set = Random.Range(0f, 1f) < randomValue ? true : false;
            entry.Value.SetAlive(set);
        }
    }

    public void ClearCage()
    {
        foreach (KeyValuePair<Vector3, Cube> entry in cubes)
        {
            entry.Value.SetAlive(false);
            entry.Value.numOfNeighbours = 0;
        }
    }

    public void Iterate()
    {
        foreach(KeyValuePair<Vector3, Cube> entry in  cubes)
        {

            if (entry.Value.isAlive)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        for (int z = -1; z < 2; z++)
                        {
                            Vector3 temp = new Vector3((float)x, (float)y, (float)z);
                            Vector3 newKey = entry.Key + temp;
                            var cube = GetCubeAtPosition(newKey);
                            if (cube != null) cube.numOfNeighbours++;
                        }
                    }
                }
                entry.Value.numOfNeighbours--;  //because we added itself as 
            }

        }                                   //a neighbour in the loop

        aliveCount = 0;
        foreach (KeyValuePair<Vector3, Cube> entry in cubes)
        {
            var cube = entry.Value;
            if (cube.isAlive && (cube.numOfNeighbours == 2 || cube.numOfNeighbours == 3))
            {
                cube.isAliveNextTurn = true; aliveCount++;
            }
            else if (!cube.isAlive && cube.numOfNeighbours == 3)
            {
                cube.isAliveNextTurn = true; aliveCount++;
            }
        }
        Print();
        ClearCage();
        foreach (KeyValuePair<Vector3, Cube> entry in cubes)
        {
            entry.Value.NextTurn(); //uses the isAliveNextTurn flag
        }
        //ShowAliveCount();
    }

    private Cube GetCubeAtPosition(Vector3 pos)
    {
        if (pos.x < 0)
            pos.x = cubeSide - 1;
        if (pos.x == cubeSide)
            pos.x = 0;
        if (pos.y < 0)
            pos.y = cubeSide - 1;
        if (pos.y == cubeSide)
            pos.y = 0;
        if (pos.z < 0)
            pos.z = cubeSide - 1;
        if (pos.z == cubeSide)
            pos.z = 0;

        if (cubes.TryGetValue(pos, out var Cube)) { return Cube; }
        return null;
    }
}
