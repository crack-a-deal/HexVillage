//public class HexPathfinder : MonoBehaviour
//{
//    public event Action OnPathFound;
//    [SerializeField] private HexGridLayoutRenderer gridRenderer;
//    PathFinder<Vector2Int> pathFinder = new DijkstraPathFinder<Vector2Int>();

//    public List<Vector2Int> mWayPoints = new List<Vector2Int>();

//    private void Start()
//    {
//        pathFinder.onSuccess = OnSuccessPathFinding;
//        pathFinder.onFailure = OnFailurePathFinding;
//        pathFinder.HeuristicCost = GetManhattanCost;
//        pathFinder.NodeTraversalCost = GetEuclideanCost;
//    }

//    public void SetDestination(Hexagon start, Hexagon end)
//    {
//        if (pathFinder.Status == PathFinderStatus.RUNNING)
//        {
//            Debug.Log("Pathfinder already running. Cannot set destination now");
//            return;
//        }
//        HexNode startNode = new HexNode(gridRenderer, start, start.Coordinate);
//        HexNode endNode = new HexNode(gridRenderer, end, end.Coordinate);

//        pathFinder.Initialize(startNode, endNode);
//        StartCoroutine(Coroutine_FindPathSteps());
//    }

//    public List<Vector2Int> GetPath()
//    {
//        return mWayPoints;
//    }

//    private IEnumerator Coroutine_FindPathSteps()
//    {
//        while (pathFinder.Status == PathFinderStatus.RUNNING)
//        {
//            pathFinder.Step();
//            yield return null;
//        }
//    }

//    private void OnFailurePathFinding()
//    {
//        Debug.Log("Error: Cannot find path");
//    }

//    private void OnSuccessPathFinding()
//    {
//        Debug.Log("Path founded");
//        PathFinder<Vector2Int>.PathFinderNode node = pathFinder.CurrentNode;
//        List<Vector2Int> reverse_indices = new List<Vector2Int>();
//        while (node != null)
//        {
//            reverse_indices.Add(node.Location.Value);
//            node = node.Parent;
//        }
//        for (int i = reverse_indices.Count - 1; i >= 0; i--)
//        {
//            mWayPoints.Add(new Vector2Int(reverse_indices[i].x, reverse_indices[i].y));
//        }

//        OnPathFound?.Invoke();
//    }

//    public float GetManhattanCost(Vector2Int a, Vector2Int b)
//    {
//        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
//    }

//    public float GetEuclideanCost(Vector2Int a, Vector2Int b)
//    {
//        return GetCostBetweenTwoCells(a, b);
//    }

//    public float GetCostBetweenTwoCells(Vector2Int a, Vector2Int b)
//    {
//        return Mathf.Sqrt(
//                (a.x - b.x) * (a.x - b.x) +
//                (a.y - b.y) * (a.y - b.y)
//            );
//    }
//}

//public class HexNode : Node<Vector2Int>
//{
//    private HexGridLayoutRenderer _gridMap;
//    public Hexagon hexagon;
//    public HexNode(HexGridLayoutRenderer gridMap, Hexagon hex, Vector2Int value) : base(value)
//    {
//        _gridMap = gridMap;
//        hexagon = hex;
//    }

//    public override List<Node<Vector2Int>> GetNeighbours()
//    {
//        return _gridMap.GetNeighborsList(this);
//    }
//}
