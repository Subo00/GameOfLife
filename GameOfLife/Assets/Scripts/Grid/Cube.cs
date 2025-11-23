using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Material deadMaterial;
    [SerializeField] private Material aliveMaterial;
    private Renderer mesh;

    //private CageHandler _cage;
    public bool isAlive;
    public bool isAliveNextTurn = false;
    public int numOfNeighbours;


    public void Init(Vector3 pos)
    {
        numOfNeighbours = 0;
        mesh = GetComponent<Renderer>();
        SetAlive(false);
        GetComponent<Transform>().position = pos;
    }

    public void SetAlive(bool isAlive)
    {
        this.isAlive = isAlive;
        mesh.material = isAlive ? aliveMaterial : deadMaterial;
    }

    public void NextTurn()
    {
        SetAlive(isAliveNextTurn);
        isAliveNextTurn = false;
    }

    public void ToggleAlive()
    {
        isAlive = !isAlive;
        SetAlive(isAlive);
    }
}
