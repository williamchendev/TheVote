using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebGLStreamingFile {

    private string filePath;
    private string result;

    public WebGLStreamingFile(string path) {
        string[] new_path = path.Split('/');
        for (int i = 0; i < new_path.Length; i++){
            filePath = System.IO.Path.Combine(Application.streamingAssetsPath, new_path[i]);
        }
        getText();
    }

    public string resulttext() {
        return result;
    }

	IEnumerator getText() {
        if (filePath.Contains("://") || filePath.Contains(":///")) {
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(filePath);
            yield return www.SendWebRequest();
            result = www.downloadHandler.text;
        } else
            result = System.IO.File.ReadAllText(filePath);
    }

}
