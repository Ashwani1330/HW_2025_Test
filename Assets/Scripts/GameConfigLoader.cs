using UnityEngine;

public class GameConfigLoader : MonoBehaviour
{
    public static GameConfig Config;

    void Awake()
    {
        Load();
    }

    void Load()
    {
        TextAsset json = Resources.Load<TextAsset>("doofus_diary");

        if (json == null)
        {
            Debug.LogError("doofus_diary.json NOT FOUND in Resources!");
            return;
        }

        Config = JsonUtility.FromJson<GameConfig>(json.text);

        if (Config == null)
        {
            Debug.LogError("Failed to parse JSON.");
            return;
        }

        Debug.Log("Config Loaded Successfully");
    }
}
