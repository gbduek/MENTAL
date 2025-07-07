using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;  // IMPORTANTE para Image e Sprite

public class DropZone : MonoBehaviour, IDropHandler
{
    public FileType acceptedType;
    public Image iconImage;             // arraste aqui no Inspector o Image do ícone da pasta
    public Sprite folderEmptyIcon;      // sprite da pasta vazia (opcional)
    public Sprite folderFilledIcon;     // sprite da pasta cheia (opcional)

    private bool isFirstCorrectDrop = true;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        FileData data = dropped.GetComponent<FileData>();
        if (data != null && data.fileType == acceptedType)
        {
            Destroy(dropped);
            Debug.Log("Arquivo correto entregue!");

            if (isFirstCorrectDrop && iconImage != null && folderFilledIcon != null)
            {
                iconImage.sprite = folderFilledIcon;
                isFirstCorrectDrop = false;
            }
        }
        else
        {
            Debug.Log("Arquivo errado! Voltando para a posição original.");
            DraggableFile draggable = dropped.GetComponent<DraggableFile>();
            if (draggable != null)
            {
                draggable.ReturnToOriginalPosition();
            }
        }
    }
}
