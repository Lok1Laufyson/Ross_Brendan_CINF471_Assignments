using UnityEngine;

public class GeneratorTEST : MonoBehaviour
{
    public int gridWidth = 20;
    public int gridHeight = 20;
    public float cellSize = 0.5f;
    public GameObject cellPrefab;

    private Cell[,] grid;
    public float updateInterval = 0.1f;
    private float timer;

    public enum GridPlacement { Floor, Ceiling, Frontwall, Backwall, Leftwall, Rightwall }
    public GridPlacement placement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeGrid();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            UpdateGrid();  // Now correctly references UpdateGrid()
            timer = 0;
        }
    }

    void InitializeGrid()
    {
        grid = new Cell[gridWidth, gridHeight];

        // Calculate the starting offset to center the grid on the floor
        float startX = -(gridWidth * cellSize) / 2f;  // Center the grid along the X-axis
        float startZ = -(gridHeight * cellSize) / 2f; // Center the grid along the Z-axis

        // Ensure the cell is placed right on the floor level
        float floorOffset = cellPrefab.transform.localScale.y / 2f; // Calculate half the height of the cellPrefab

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = Vector3.zero;
                Quaternion rotation = Quaternion.identity;

                switch (placement)
                {
                    case GridPlacement.Floor:
                        // Adjust the position to ensure cells are on the floor
                        position = new Vector3(startX + x * cellSize, floorOffset, startZ + y * cellSize);
                        rotation = Quaternion.Euler(90, 0, 0); // Rotate to lay flat on the floor
                        break;

                    case GridPlacement.Ceiling:
                        position = new Vector3(startX + x * cellSize, 116, startZ + y * cellSize);
                        rotation = Quaternion.Euler(270, 0, 0); // Rotate to face downwards
                        break;

                    case GridPlacement.Frontwall:
                        position = new Vector3(startX + x * cellSize, 0, 73.7f + y * cellSize);
                        rotation = Quaternion.Euler(0, 0, 0);
                        break;

                    case GridPlacement.Backwall:
                        position = new Vector3(startX + x * cellSize, 0, -25.5f + y * cellSize);
                        rotation = Quaternion.Euler(0, 180, 0);
                        break;

                    case GridPlacement.Leftwall:
                        position = new Vector3(-10.6f, y * cellSize, startZ + x * cellSize);
                        rotation = Quaternion.Euler(0, 90, 0);
                        break;

                    case GridPlacement.Rightwall:
                        position = new Vector3(89.5f, y * cellSize, startZ + x * cellSize);
                        rotation = Quaternion.Euler(0, -90, 0);
                        break;
                }

                GameObject cellObj = Instantiate(cellPrefab, position, rotation);
                cellObj.transform.parent = transform;

                grid[x, y] = cellObj.GetComponent<Cell>();
                grid[x, y].isAlive = Random.value > 0.5f;
                grid[x, y].UpdateColor();
            }
        }
    }

    void UpdateGrid()
    {
        bool[,] nextState = new bool[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                int aliveNeighbors = CountAliveNeighbors(x, y);
                bool isAlive = grid[x, y].isAlive;

                if (isAlive && (aliveNeighbors < 1 || aliveNeighbors > 3))
                    nextState[x, y] = false;
                else if (!isAlive && (aliveNeighbors == 3 || aliveNeighbors == 2))
                    nextState[x, y] = true;
                else
                    nextState[x, y] = isAlive;
            }
        }

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                grid[x, y].isAlive = nextState[x, y];
                grid[x, y].UpdateColor();
            }
        }
    }

    int CountAliveNeighbors(int x, int y)
    {
        int count = 0;
        int[] dx = { -1, 0, 1, -1, 1, -1, 0, 1 };
        int[] dy = { -1, -1, -1, 0, 0, 1, 1, 1 };

        for (int i = 0; i < 8; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];

            if (nx >= 0 && nx < gridWidth && ny >= 0 && ny < gridHeight && grid[nx, ny].isAlive)
            {
                count++;
            }
        }

        return count;
    }
}
