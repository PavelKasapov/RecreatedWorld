using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileButton : MonoBehaviour
{
    public Text saveName;

    public void DisplayText(string fileName)
    {
        saveName.text = Path.GetFileNameWithoutExtension(fileName);
    }
}
