using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{

    public static void SavePlayer(MoveMent player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerInformation data = new PlayerInformation(player);
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved at" + path);

    }
    public static PlayerInformation LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerInformation data = formatter.Deserialize(stream) as PlayerInformation;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found at " + path);
            return null;
        }

    }

}