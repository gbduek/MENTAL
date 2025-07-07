using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Coloque o enum fora da classe para que outros scripts possam usar
public enum FileType
{
    PDF,
    Texto,
    Imagem,
    Planilha,
    Apresentacao,
    Lixo
}

public class FileData : MonoBehaviour
{
    public FileType fileType;

    public TextMeshProUGUI fileNameText;
    public Image iconImage;

    public Sprite pdfIcon;
    public Sprite txtIcon;
    public Sprite imgIcon;
    public Sprite xlsIcon;
    public Sprite pptIcon;
    public Sprite trashIcon;

    private void Start()
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        switch (fileType)
        {
            case FileType.PDF:
                fileNameText.text = "relatorio.pdf";
                iconImage.sprite = pdfIcon;
                break;
            case FileType.Texto:
                fileNameText.text = "anotacao.txt";
                iconImage.sprite = txtIcon;
                break;
            case FileType.Imagem:
                fileNameText.text = "imagem.png";
                iconImage.sprite = imgIcon;
                break;
            case FileType.Planilha:
                fileNameText.text = "planilha.xlsx";
                iconImage.sprite = xlsIcon;
                break;
            case FileType.Apresentacao:
                fileNameText.text = "slides.pptx";
                iconImage.sprite = pptIcon;
                break;
            case FileType.Lixo:
                fileNameText.text = "aviso_antigo.txt";
                iconImage.sprite = trashIcon;
                break;
        }
    }
}
