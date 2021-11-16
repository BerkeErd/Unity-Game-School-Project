using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{ 
    public static void SavePlayerSkills(SaveData saveData)
    {
        Debug.Log("savesystem'e girdi");
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.stats";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(saveData);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayerSkills()
    {
        string path = Application.persistentDataPath + "/player.stats";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save dosyası bu adreste bulunamadı : " + path);
            return null;
        }
    }

}
