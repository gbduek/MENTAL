using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DesktopMinigame : MonoBehaviour
{
    public GameObject minigamePanel;

    public GameObject filePrefab;
    public RectTransform fileArea;    // Painel onde os arquivos vão ficar
    public GameObject popUpPrefab;
    public Transform popUpArea;

    public int trashFileCount = 2;
    public int normalFileCount = 3;
    public float popUpInterval = 5f;

    private bool isActive = false;
    private float popUpTimer = 0f;

    private List<Vector2> positions = new List<Vector2>();

    string GetExtension(FileType type)
    {
        return type switch
        {
            FileType.PDF => ".pdf",
            FileType.Texto => ".txt",
            FileType.Imagem => ".png",
            FileType.Planilha => ".xlsx",
            _ => ".dat"
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleMinigame();
        }

        if (!isActive || DraggableFile.isDragging) return;


        popUpTimer += Time.unscaledDeltaTime;
        if (popUpTimer >= popUpInterval)
        {
            SpawnPopUp();
            popUpTimer = 0f;
        }
    }

    void ToggleMinigame()
    {
        isActive = !isActive;
        minigamePanel.SetActive(isActive);

        if (isActive)
        {
            Time.timeScale = 0f;
            GenerateFiles();
        }
        else
        {
            Time.timeScale = 1f;
            ClearAll();
        }
    }

    void GenerateFiles()
    {
        foreach (Transform t in fileArea)
            Destroy(t.gameObject);

        float padding = 10f;
        Vector2 areaSize = fileArea.rect.size;

        // Arquivos de Lixo
        for (int i = 0; i < trashFileCount; i++)
        {
            string name = "Lixo_" + i + ".txt";
            SpawnFile(name, FileType.Lixo, areaSize, padding);
        }

        // Arquivos normais com tipos aleatórios (exceto lixo)
        FileType[] validTypes = new FileType[] {
            FileType.PDF, FileType.Texto, FileType.Imagem, FileType.Planilha
        };

        for (int i = 0; i < normalFileCount; i++)
        {
            FileType randomType = validTypes[Random.Range(0, validTypes.Length)];
            string extension = GetExtension(randomType);
            string name = "Arquivo_" + i + extension;
            SpawnFile(name, randomType, areaSize, padding);
        }
    }

    void SpawnFile(string name, FileType type, Vector2 areaSize, float padding)
    {
        var file = Instantiate(filePrefab, fileArea);

        var fd = file.GetComponent<FileData>();
        fd.fileType = type;
        fd.UpdateVisuals();  // atualiza nome e ícone

        file.GetComponentInChildren<TextMeshProUGUI>().text = name;

        RectTransform rt = file.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 1);
        rt.anchorMax = new Vector2(0, 1);
        rt.pivot = new Vector2(0, 1);
        rt.localScale = Vector3.one;

        Vector2 finalPos;
        int attempts = 0;
        do
        {
            float x = Random.Range(padding, areaSize.x - padding - rt.rect.width);
            float y = Random.Range(padding, areaSize.y - padding - rt.rect.height);
            finalPos = new Vector2(x, -y); // negativo no y pois âncora está no topo
            attempts++;
        }
        while (IsTooClose(finalPos) && attempts < 50);

        rt.anchoredPosition = finalPos;

        // Armazena essa posição para evitar overlaps futuros
        positions.Add(finalPos);

        // Salva posição original para retorno se drop for errado
        var drag = file.GetComponent<DraggableFile>();
        if (drag != null)
        {
            drag.originalParent = fileArea;
            drag.originalPosition = finalPos;
        }
    }


    bool IsTooClose(Vector2 pos)
    {
        foreach (Vector2 existing in positions)
        {
            if (Vector2.Distance(existing, pos) < 80f)
                return true;
        }
        return false;
    }

    void SpawnPopUp()
    {
        Instantiate(popUpPrefab, popUpArea);
    }

    void ClearAll()
    {
        foreach (Transform t in fileArea)
            Destroy(t.gameObject);

        foreach (Transform t in popUpArea)
            Destroy(t.gameObject);

        positions.Clear();
    }
}