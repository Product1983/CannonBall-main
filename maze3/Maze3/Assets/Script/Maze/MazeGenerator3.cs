using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator3 : MonoBehaviour
{
   [SerializeField] private MazeCell3 _mazeCellPrefab; // Префаб ячейки лабиринта
       [SerializeField] private int _mazeWidth; // Ширина лабиринта
       [SerializeField] private int _mazeDepth; // Глубина лабиринта
 private MazeCell3[,] _mazeGrid; // Двумерный массив ячеек лабиринта

    void Start()
    {
        // Инициализация сетки лабиринта
        _mazeGrid = new MazeCell3[_mazeWidth, _mazeDepth];
        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                // Создание ячейки лабиринта и размещение её в мире
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }

        // Запуск алгоритма генерации лабиринта из левого верхнего угла
        GenerateMaze(null, _mazeGrid[0, 0]);
    }

    // Рекурсивный алгоритм генерации лабиринта
    private void GenerateMaze(MazeCell3 previousCell, MazeCell3 currentCell)
    {
        currentCell.Visit(); // Отмечаем текущую ячейку как посещенную
        ClearWalls(previousCell, currentCell); // Убираем стену между текущей и предыдущей ячейками
        MazeCell3 nextCell;
        do
        {
            nextCell = GetNextUnvisitedCell(currentCell); // Получаем случайную непосещенную соседнюю ячейку
            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell); // Рекурсивно вызываем генерацию для следующей ячейки
            }
        } while (nextCell != null); // Повторяем, пока есть непосещенные соседи
    }

    // Находит случайную непосещенную соседнюю ячейку
    private MazeCell3 GetNextUnvisitedCell(MazeCell3 currentCell)
    {
        var unvisitedCells = GetUnvisitedCell(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault(); //Случайно выбираем одну из непосещенных ячеек
    }

    // Возвращает список непосещенных соседних ячеек
    private IEnumerable<MazeCell3> GetUnvisitedCell(MazeCell3 currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;
        // Проверяем наличие непосещенных соседей справа
        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];
            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        // Проверяем наличие непосещенных соседей слева
        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];
            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        // Проверяем наличие непосещенных соседей спереди
        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];
            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        // Проверяем наличие непосещенных соседей сзади
        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];
            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    // Убирает стену между двумя ячейками
    private void ClearWalls(MazeCell3 previousCell, MazeCell3 currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        // Определяем, где находится предыдущая ячейка относительно текущей, и убираем соответствующие стены
        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }
}