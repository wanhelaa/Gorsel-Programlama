using UnityEngine;
using System.Data;
using Mono.Data.Sqlite; // Hata alýrsanýz Mono.Data.SqliteClient olarak güncelleyin
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;

public class DBManager
{
    private IDbConnection _connection;
    private static DBManager _instance;
    private string _dbPath;

    public static DBManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DBManager();
            }
            return _instance;
        }
    }

    private DBManager()
    {
        _dbPath = "URI=file:" + Application.persistentDataPath + "/todo.db";
        ConnectToDatabase();
    }

    private void ConnectToDatabase()
    {
        try
        {
            _connection = new SqliteConnection(_dbPath);
            _connection.Open();
            Debug.Log("Veritabaný baðlantýsý baþarýyla kuruldu.");
            CreateTable();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Veritabaný baðlantý hatasý: " + ex.Message);
        }
    }

    private void CreateTable()
    {
        string sqlQuery = "CREATE TABLE IF NOT EXISTS ToDoItems (id INTEGER PRIMARY KEY AUTOINCREMENT, task TEXT NOT NULL, isComplete INTEGER)";
        using (IDbCommand command = _connection.CreateCommand())
        {
            command.CommandText = sqlQuery;
            command.ExecuteNonQuery();
        }
        Debug.Log("ToDoItems tablosu oluþturuldu veya zaten mevcut.");
    }

    public void SaveTask(ToDoItem task)
    {
        using (IDbCommand command = _connection.CreateCommand())
        {
            string sqlQuery = "INSERT INTO ToDoItems (task, isComplete) VALUES (@task, @isComplete)";
            command.CommandText = sqlQuery;

            var p1 = new SqliteParameter("@task", task.Task);
            var p2 = new SqliteParameter("@isComplete", task.IsComplete ? 1 : 0);
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);

            command.ExecuteNonQuery();
        }
    }

    public List<ToDoItem> GetAllTasks()
    {
        List<ToDoItem> tasks = new List<ToDoItem>();
        using (IDbCommand command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM ToDoItems";
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tasks.Add(new ToDoItem
                    {
                        Id = reader.GetInt32(0),
                        Task = reader.GetString(1),
                        IsComplete = reader.GetInt32(2) == 1
                    });
                }
            }
        }
        return tasks;
    }

    public void UpdateTask(ToDoItem task)
    {
        using (IDbCommand command = _connection.CreateCommand())
        {
            command.CommandText = "UPDATE ToDoItems SET task = @task, isComplete = @isComplete WHERE id = @id";
            command.Parameters.Add(new SqliteParameter("@task", task.Task));
            command.Parameters.Add(new SqliteParameter("@isComplete", task.IsComplete ? 1 : 0));
            command.Parameters.Add(new SqliteParameter("@id", task.Id));
            command.ExecuteNonQuery();
        }
    }

    public void DeleteTask(int id)
    {
        using (IDbCommand command = _connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM ToDoItems WHERE id = @id";
            command.Parameters.Add(new SqliteParameter("@id", id));
            command.ExecuteNonQuery();
        }
    }
}
