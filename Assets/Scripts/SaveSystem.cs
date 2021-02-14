//Author: Mokhirbek Salimboev
//Student ID: 1919019

using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public class SaveSystem
{
    private static string FilePath//taking filpath
    {
        get { return Application.persistentDataPath + "/playerInfo.dat"; }//apps persistent data path + name of data
    }

    /// <summary>
    /// function to save data
    /// </summary>
    /// <param name="game"></param>
    public static void Save(GameManager game)
    {
        
        BinaryFormatter formatter = new BinaryFormatter();//making new binary format
        
        FileStream fileStream = new FileStream(FilePath, FileMode.Create);//creating file

        Data data = new Data(game);//saving data
        

        formatter.Serialize(fileStream, data);//serializing this data
        fileStream.Close();//closing file
    }
    /// <summary>
    /// load data
    /// </summary>
    /// <returns></returns>
    public static Data Load()
    {
        if (File.Exists(FilePath))//check file
        {
            BinaryFormatter formatter = new BinaryFormatter();//make binary formatter
            FileStream stream = new FileStream(FilePath, FileMode.Open);//open file

            Data data = formatter.Deserialize(stream) as Data;//deserialize
            //GameManager.Instance.coin = data.coin;
            stream.Close();//close file
            return data;
        }
        else
        {
            return null;
        }
    }

}

[System.Serializable]
public class Data
{
    public int coin;
    public Data(GameManager game)
    {
        coin = game.coin;
    }
}
