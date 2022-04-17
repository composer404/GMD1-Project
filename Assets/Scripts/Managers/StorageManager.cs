using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    #region Singleton

    private static StorageManager instance;

    private void Awake() {
        instance = this;
    }

    public static StorageManager GetInstance() {
        return instance;
    }

    #endregion

    private string resultPath = "/ResultData.json";

    public void SaveResult(float result) {
        Results results = ReadResults();

        if(results == null) {
            results = new Results();
        }

        string username = PlayerPrefs.GetString("USERNAME");

        ResultData timeToSave = new ResultData(result, username);
        results.results.Add(timeToSave);

        System.IO.File.WriteAllText(Application.persistentDataPath + resultPath, JsonUtility.ToJson(results));
    }

    public Results ReadResults() {
        try{
            string response = System.IO.File.ReadAllText(Application.persistentDataPath + resultPath);
            return JsonUtility.FromJson<Results>(response);
        } catch {
            return null;
        }

    }

    public Results SortResults(Results results) {
        if (results == null) {
            return null;
        }

        List<ResultData> sordeted = results.results.OrderByDescending((element) => element.result).ToList();
        results.results = sordeted;

        print("Sorteet" + sordeted);
        return results;
    } 

    public float GetBestResult() {
        Results readResults = ReadResults();
        Results sortedResults = SortResults(readResults);

        if (readResults == null || sortedResults == null || sortedResults.results[0] == null) {
            return 0;
        }

        return sortedResults.results[0].result;
    }
}