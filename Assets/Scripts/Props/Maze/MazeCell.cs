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
}
