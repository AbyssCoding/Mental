using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveEntity(GeneralData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + data.serialNumber + ".hahalol";
        FileStream stream = new FileStream(path, FileMode.Create);

        EntityData genData = new EntityData(data);

        formatter.Serialize(stream, genData);
        stream.Close();
    }

    public static EntityData LoadData(GeneralData data)
    {
        string path = Application.persistentDataPath + "/" + data.serialNumber + ".hahalol";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EntityData stuff = formatter.Deserialize(stream) as EntityData;
            stream.Close();

            return stuff;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

    }
}
