using TMPro;
using UnityEngine;

public class DialogViewPanel : MonoBehaviour
{
    [SerializeField]
    TMP_Text autor;
    [SerializeField]
    TMP_Text replic;

    private TaskGame taskView;
    private int viewRemark = 0;

    private float defaultPosY;
    private RectTransform rectTransform;
    public void OpenDialog(TaskGame task) {
        if (task != taskView)
        {
            taskView = task;
            viewRemark = 0;
            rectTransform.position = new Vector3(rectTransform.position.x, defaultPosY, rectTransform.position.z);
            autor.text = taskView.startDialog[0].Speaking == TypeD.Boss ? "Power system dispatcher" : "Hydroelectric Power Plant Dispatcher";
            replic.text = taskView.startDialog[0].remark;
        }
    }
    private void CloseDialog() {
        if (taskView != null) 
            taskView.CompletedDialog = true;
        rectTransform.position = new Vector3(rectTransform.position.x, 2 * -defaultPosY, rectTransform.position.z);
    }
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultPosY = rectTransform.position.y;
        CloseDialog();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (taskView != null && !taskView.CompletedDialog)
            {
                viewRemark++;
                if (viewRemark == taskView.startDialog.Length)
                {
                    CloseDialog();
                    return;
                }
                autor.text = taskView.startDialog[viewRemark].Speaking == TypeD.Boss ? "Power system dispatcher" : "Hydroelectric Power Plant Dispatcher";
                replic.text = taskView.startDialog[viewRemark].remark;
            }
        }
    }
}
