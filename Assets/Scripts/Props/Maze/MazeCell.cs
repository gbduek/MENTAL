using System.Dynamic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField]
    private GameObject backWall;
    [SerializeField]
    private GameObject frontWall;
    [SerializeField]
    private GameObject rightWall;
    [SerializeField]
    private GameObject leftWall;
    [SerializeField]
    private ExitController exitController;

    private GameObject exit;

    [SerializeField]
    private GameObject unvisitedField;

    public bool isVisited {get; private set;}

    public void Visit()
    {
        isVisited = true;
        unvisitedField.SetActive(false);
    }

    public void ClearBackWall()
    {
        backWall.SetActive(false);
    }
    public void ClearFrontWall()
    {
        frontWall.SetActive(false);
    }
    public void ClearRightWall()
    {
        rightWall.SetActive(false);
    }
    public void ClearLeftWall()
    {
        leftWall.SetActive(false);
    }
    public void setBackExit()
    {
        backWall.AddComponent<ExitController>();
        exit = backWall;
    }

    public void setFrontExit()
    {
        frontWall.AddComponent<ExitController>();
        exit = frontWall;
    }

    public void setRightExit()
    {
        rightWall.AddComponent<ExitController>();
        exit = rightWall;
    }

    public void setLeftExit()
    {
        leftWall.AddComponent<ExitController>();
        exit = leftWall;
    }

    public void enableExit()
    {
        Debug.Log("Enabling exit...");
        exit.GetComponent<Collider2D>().isTrigger = true;
        var graphics = exit.GetComponentInChildren<SpriteRenderer>().gameObject;
        graphics.SetActive(false);
    }
}
