using UnityEngine;
using System.Collections.Generic;

public class ToDoUIController : MonoBehaviour
{
    public GameObject toDoItemPrefab;
    public Transform taskListContent;

    private DBManager _dbManager;

    void Start()
    {
        _dbManager = DBManager.Instance;
        RefreshTaskList();
    }

    public void AddNewTask(string taskName)
    {
        if (string.IsNullOrEmpty(taskName)) return;

        ToDoItem newTask = new ToDoItem { Task = taskName, IsComplete = false };
        _dbManager.SaveTask(newTask);
        RefreshTaskList();
    }

    public void RefreshTaskList()
    {
        // Eski listeyi temizle
        foreach (Transform child in taskListContent)
        {
            Destroy(child.gameObject);
        }

        // Veritabanýndan çek ve listele
        List<ToDoItem> allTasks = _dbManager.GetAllTasks();

        foreach (ToDoItem task in allTasks)
        {
            GameObject taskObject = Instantiate(toDoItemPrefab, taskListContent);
            ToDoItemDisplay display = taskObject.GetComponent<ToDoItemDisplay>();

            display.taskText.text = task.Task;
            display.taskToggle.isOn = task.IsComplete;

            // Olaylarý baðla
            int taskId = task.Id;
            display.taskToggle.onValueChanged.AddListener((isComplete) => OnToggleTask(taskId, isComplete));
            display.deleteButton.onClick.AddListener(() => OnDeleteTask(taskId));
        }
    }

    private void OnToggleTask(int taskId, bool isComplete)
    {
        // Güncelleme mantýðý buraya gelecek
        Debug.Log($"Görev {taskId} durumu deðiþti: {isComplete}");
    }

    private void OnDeleteTask(int taskId)
    {
        _dbManager.DeleteTask(taskId);
        RefreshTaskList();
    }
}