using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public Node2 startNode;
    [HideInInspector] public Node2 endNode;
    Pathfinding2 _pf;
    [SerializeField] public Grid2 currentGraph;

    public static GameManager2 instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        _pf = new Pathfinding2();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeStartingNode(GetNodeOnCursor());
        }
        if (Input.GetMouseButtonDown(1))
        {
            ChangeEndNode(GetNodeOnCursor());
        }
        if (Input.GetMouseButtonDown(2))
        {
            Node2 node = GetNodeOnCursor();
            node?.SetBlock(!node.isBlocked);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentGraph.PaintNodesWhite(startNode, endNode);
            //if (_startNode != null) _pf.BFS(_startNode);
           // if (startNode != null) StartCoroutine(_pf.PaintAStar(startNode, endNode));
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Node2 node = GetNodeOnCursor();
            node?.SetCost(node.Cost == 1 ? 5 : node.Cost + 5);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            Node2 node = GetNodeOnCursor();
            node?.SetCost(node.Cost - 5);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    void ChangeStartingNode(Node2 node)
    {
        if (node == null) return;
        startNode?.ChangeColor(Color.white);
        startNode = node;
        startNode.ChangeColor(Color.red);
    }

    void ChangeEndNode(Node2 node)
    {
        if (node == null) return;
        endNode?.ChangeColor(Color.white);
        endNode = node;
        endNode.ChangeColor(Color.green);
    }

    public GameObject GetOnCursorObject()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
        {
            return hit.collider.gameObject;
        }

        return default;

        /* return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity) ? hit.collider.gameObject : default;*/
    }

    Node2 GetNodeOnCursor()
    {
        return GetOnCursorObject()?.GetComponent<Node2>();
    }
    

    public void ChangeObjectColor(GameObject go, Color color)
    {
        if (go == null) return;
        go.GetComponent<Renderer>().material.color = color;
    }

}
