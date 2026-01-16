using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using TMPro;

public class FileLoader : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName);

    public MainMenuManager menuManager;

    public void OpenFileBrowser()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        UploadFile(gameObject.name, "OnFileUpload");
#endif
    }

    public void OnFileUpload(string fileContent)
    {
        menuManager.ProcessLoadedFile(fileContent);
    }
}