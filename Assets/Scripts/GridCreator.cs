using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridCreator : MonoBehaviour
{
    public int gridSize;
    public GameObject squarePrefab;
    public TMP_InputField inputField;
    public Button createButton;
    public ObjectPool objectPool;
    private float cellScale;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        inputField.onValueChanged.AddListener(delegate { SetGridSize(inputField.text); });
        createButton.onClick.AddListener(delegate { CreateGrid(); });
        SetGridSize(inputField.text);
        CreateGrid();
    }
    public void SetGridSize(string text)
    {
        gridSize = int.Parse(text);
    }
    public void CreateGrid()
    {
        CleanGrid();
        CalculateCellSize();
        PlaceCells();
    }

    public void CleanGrid()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            objectPool.ReturnObject(child.gameObject);
        }
    }
    private void CalculateCellSize()
    {
        float screenHeight = 2f * mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;

        cellScale = Mathf.Min(screenWidth / gridSize, screenHeight / gridSize);
    }
    private void PlaceCells()
    {
        float startX = -((gridSize - 1) * cellScale) / 2f;
        float startY = -((gridSize - 1) * cellScale) / 2f;

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Vector3 position = new Vector3(startX + i * cellScale, startY + j * cellScale, 0);
                GameObject obj = objectPool.GetObject();
                obj.transform.position = position;
                obj.transform.SetParent(transform);
                obj.transform.localScale = new Vector3(cellScale, cellScale, 1);
            }
        }
    }
}
