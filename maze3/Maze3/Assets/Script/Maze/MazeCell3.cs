using UnityEngine;

public class MazeCell3 : MonoBehaviour
{
    [SerializeField] private GameObject _leftWall;   // Ссылка на GameObject левой стены
    [SerializeField] private GameObject _rightWall;  // Ссылка на GameObject правой стены
    [SerializeField] private GameObject _frontWall;  // Ссылка на GameObject передней стены
    [SerializeField] private GameObject _backWall;   // Ссылка на GameObject задней стены
    [SerializeField] private GameObject _unvisitedBlock; // Ссылка на GameObject блока, показывающего, что ячейка не посещена
    public bool IsVisited { get; private set; }      // Свойство, указывающее, была ли посещена ячейка

    // Метод для отметки ячейки как посещенной
    public void Visit()
    {
        IsVisited = true;
        if (_unvisitedBlock != null) // Проверка на null, чтобы избежать ошибок
        {
            _unvisitedBlock.SetActive(false); // Отключаем блок, показывающий, что ячейка не посещена
        }
    }

    // Методы для удаления стен (отключения GameObject)
    public void ClearLeftWall()
    {
        if (_leftWall != null) _leftWall.SetActive(false);
    }

    public void ClearRightWall()
    {
        if (_rightWall != null) _rightWall.SetActive(false);
    }

    public void ClearFrontWall()
    {
        if (_frontWall != null) _frontWall.SetActive(false);
    }

    public void ClearBackWall()
    {
        if (_backWall != null) _backWall.SetActive(false);
    }
}