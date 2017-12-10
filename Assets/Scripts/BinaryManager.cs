using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class BinaryManager : MonoBehaviour{

	private const string file_name = "/playerInfo.dat";

	private static int levelUnlocked = 1;

	void Awake(){
		Load();
		Destroy(gameObject);
	}

	public static int GetLevelUnlocked(){
		return levelUnlocked;
	}

	public static void UnlockLevel(int level){
		if (level <= levelUnlocked) return;
		levelUnlocked = level;
		Save();
	}

	public static void Load(){
		if(File.Exists(Application.persistentDataPath + file_name)){
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + file_name, FileMode.Open);
			PlayerData data = (PlayerData)binaryFormatter.Deserialize(file);
			file.Close();
			levelUnlocked = data.levelUnlocked;
		} else {
			Save();
		}
	}

	public static void Save(){
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + file_name);
		PlayerData data = new PlayerData();
		data.levelUnlocked = levelUnlocked;
		binaryFormatter.Serialize(file, data);
		file.Close();
	}

	public static void Reset(){
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + file_name);
		PlayerData data = new PlayerData();
		data.levelUnlocked = 1;
		binaryFormatter.Serialize(file, data);
		file.Close();
	}
}

[Serializable]
class PlayerData{
	public int levelUnlocked;
}
