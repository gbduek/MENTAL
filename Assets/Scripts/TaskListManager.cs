using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TaskListManager : MonoBehaviour
{
    public GameObject taskListPanel; // Painel que mostra a lista
    public TextMeshProUGUI taskListText;

    private List<string> tasks = new List<string>();
    private bool isOpen = false;

    void Start()
    {
        taskListPanel.SetActive(false); // Esconde no início

        AddTask("Ache o relógio.");
        AddTask("Fale com o Charizard.");
        AddTask("Pressione E para fechar essa lista.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            taskListPanel.SetActive(isOpen);
        }
    }

    public void AddTask(string task)
    {
        tasks.Add(task);


        UpdateTaskListUI();


    }

    public void RemoveTask(string task)
    {
        tasks.Remove(task);
        UpdateTaskListUI();
    }

    private void UpdateTaskListUI()
    {
        taskListText.text = "";

        for (int i = 0; i < tasks.Count; i++)
        {
            taskListText.text += (i + 1) + ". " + tasks[i] + "\n";
        }
    }
}
