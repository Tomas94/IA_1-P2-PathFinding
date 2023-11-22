using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Node> _allNodes = new List<Node>();
    
    public Node pfEndNode;

    public Transform player;
    public bool playerDetected = false;
    public Vector3 alertPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        AddAllNodes();
    }

    void AddAllNodes()
    {
        Node[] allNodes = FindObjectsOfType<Node>();
        _allNodes.AddRange(allNodes);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(alertPosition != Vector3.zero) Gizmos.DrawSphere(alertPosition, 0.2f);
    }
}
