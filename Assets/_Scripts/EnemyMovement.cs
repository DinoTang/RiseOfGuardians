using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : DinoMonoBehaviour
{
    [SerializeField] protected Rigidbody2D rgb;
    [SerializeField] protected int speed = 2;
    [SerializeField] protected string type;
    [SerializeField] protected Transform currentPathNode;
    [SerializeField] protected List<Transform> oldPathNodes;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRigidbody();
        this.LoadCurrPathNode();
    }
    protected override void Start()
    {
        this.RandomPath();
    }
    protected void Update()
    {
        this.FindCurrentPathNode();
    }
    protected void FixedUpdate()
    {
        this.Moving();
    }
    protected void LoadRigidbody()
    {
        if (this.rgb != null) return;
        this.rgb = GetComponent<Rigidbody2D>();
        Debug.Log(this.transform.name + ": LoadRigidbody");
    }
    protected void LoadCurrPathNode()
    {
        if (this.currentPathNode != null) return;
        this.currentPathNode = GameObject.Find("StartNode").transform;
        Debug.Log(this.transform.name + ": LoadCurrPathNode");
    }
    protected void RandomPath()
    {
        int rand = Random.Range(1, 3);
        if (rand == 1) this.type = "PathNode1";
        else this.type = "PathNode2";
    }

    protected void FindCurrentPathNode()
    {
        if (Vector2.Distance(this.transform.position, this.currentPathNode.position) > 0.1f) return;
        foreach (PathNode pathNode in PathNodeManager.Instance.PathNodes)
        {
            if (pathNode.type != this.type && pathNode.type != "MergePathNode") continue;
            if (this.CheckHavedInOldPN(pathNode.trans.name)) continue;
            this.currentPathNode = pathNode.trans;
            this.oldPathNodes.Add(pathNode.trans);
            return;
        }
    }
    protected bool CheckHavedInOldPN(string pathNodeName)
    {
        bool haved = this.oldPathNodes.Any(p => p.name == pathNodeName);
        return haved;
    }
    protected void Moving()
    {
        Transform target = this.currentPathNode;
        Vector2 direct = (target.position - this.transform.position).normalized;
        this.rgb.velocity = direct * this.speed;
    }
}
