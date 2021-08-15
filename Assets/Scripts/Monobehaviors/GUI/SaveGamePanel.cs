using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGamePanel : MonoBehaviour
{
    private int currentPadding = -70;
    private DateTime now;

    public GameObject saveFilesHolder;
    public GameObject saveFileButtonPrefab;
    public GameObject newSaveFile;
    public List<SaveFileButton> saveFiles;

    void OnEnable()
    {
        now = DateTime.Now;
        string saveDirectoryPath = Application.persistentDataPath + "/saves";
        DirectoryInfo info = new DirectoryInfo(saveDirectoryPath);
        FileInfo[] fileInfo = info.GetFiles("*.dat");
        foreach(var file in fileInfo)
        {
            
            //Vector3 pos = new Vector3(newSaveFile.transform.position.x, newSaveFile.transform.position.y + currentPadding, newSaveFile.transform.position.z);
            SaveFileButton saveFileButton = Instantiate(saveFileButtonPrefab, newSaveFile.transform.position, Quaternion.identity, saveFilesHolder.transform).GetComponent<SaveFileButton>();
            //saveFileButton.GetComponent<RectTransform>().localPosition.y = new Vector3(0, 0);
            //Debug.Log(saveFileButton.transform.position);
            saveFileButton.DisplayText(file.Name);
            saveFiles.Add(saveFileButton);
        }
    }
    public void onSaveGameSubmit(string saveName)
    {

    }

    public void onSaveGameSubmit()
    {

    }
}
