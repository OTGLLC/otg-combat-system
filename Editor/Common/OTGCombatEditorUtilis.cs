
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

namespace OTG.CombatSystem.Editor
{
    public static class OTGCombatEditorUtilis
    {
        #region Constants
        public const string DATAFILE_OPTIONS_VIEW_LOCATION = "Assets/Submodules/otg-combat-system/Editor/Data";
        public const string DATAFILE_CHARACTER_LIBRARY_LOCATION = "Assets/Submodules/otg-combat-system/Editor/Data";
        public const string TEMPLATE_PATH_MAIN_WINDOW = "Assets/Submodules/otg-combat-system/Editor/MainWindows/OTGCombatSystemEditorTemplate.uxml";
        public const string STYLE_PATH_MAIN_WINDOW = "Assets/Submodules/otg-combat-system/Editor/MainWindows/OTGCombatSystemEditorStyle.uss";
        #endregion

        #region Public API - Data Tools
        public static void PopulateListView<T>(ref ListView _targetListView, ref VisualElement _ownerContainer, List<T> _items, string _listAreaName, bool tailOfPath = false)
        {
            _targetListView = _ownerContainer.Query<ListView>(_listAreaName).First();

            _targetListView.Clear();
            _targetListView.makeItem = () => new Label();

            _targetListView.bindItem = (element, i) =>
            {
                string labelText = _items[i].ToString();
                if (tailOfPath)
                {
                    string[] pathSplit = labelText.Split('/');
                    labelText = pathSplit[pathSplit.Length - 1];
                }
                (element as Label).text = labelText;
            };

            _targetListView.itemsSource = _items;
            _targetListView.itemHeight = 16;
            _targetListView.selectionType = SelectionType.Single;
        }
        public static string GetAssetFolderPath(string _rawPath)
        {
            string val = string.Empty;
            
            string[] pathParts = _rawPath.Split('/');
            int foundIndex = 0;
            bool foundFolder = false;
            for(int i = 0; i < pathParts.Length; i++)
            {
                if (string.Equals(pathParts[i], "Assets"))
                {
                    foundFolder = true;
                    foundIndex = i;
                }
                    
                if(foundFolder)
                {
                    if(i == pathParts.Length-1)
                        val += pathParts[i];
                    else
                        val += pathParts[i] + "/";
                }
            }

            return val;
        }
        public static OTGGlobalCombatConfig InitializeGlobalCombatConfig(string _path, string _projectName)
        {
            OTGGlobalCombatConfig config = null;
            string[] guids = AssetDatabase.FindAssets("t:OTGGlobalCombatConfig");
            if(guids.Length == 0)
            {
                config = ScriptableObject.CreateInstance<OTGGlobalCombatConfig>();
                AssetDatabase.CreateAsset(config, _path + "/" + _projectName + "_GlobalCombatConfig.asset");
                return config;
            }

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            config = AssetDatabase.LoadAssetAtPath<OTGGlobalCombatConfig>(path);
           
            return config;
        }
        public static OTGGlobalCombatConfig GetGlobalCombatConfig()
        {
            OTGGlobalCombatConfig config = null;
            string[] guids = AssetDatabase.FindAssets("t:OTGGlobalCombatConfig");

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            config = AssetDatabase.LoadAssetAtPath<OTGGlobalCombatConfig>(path);
            return config;
        }
        public static CharacterLibrary InitializeCharacterLibrary(string _path)
        {
            CharacterLibrary lib = null;
            string[] guids = AssetDatabase.FindAssets("t:CharacterLibrary");
            if(guids.Length == 0)
            {
                lib = ScriptableObject.CreateInstance<CharacterLibrary>();
                AssetDatabase.CreateAsset(lib, _path + "/CharacterLibrary.asset");
                return lib;
            }
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            lib = AssetDatabase.LoadAssetAtPath<CharacterLibrary>(path);
            return lib;
        }
        public static CharacterLibrary GetCharacterLibrary()
        {
            CharacterLibrary lib = null;
            string[] guids = AssetDatabase.FindAssets("t:CharacterLibrary");
         
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            lib = AssetDatabase.LoadAssetAtPath<CharacterLibrary>(path);
            return lib;
        }
        public static OptionsViewData InitializeOptionsViewData()
        {
            OptionsViewData optionsData = null;
            string[] guids = AssetDatabase.FindAssets("t:OptionsViewData");
            if (guids.Length == 0)
            {
                optionsData = ScriptableObject.CreateInstance<OptionsViewData>();
                AssetDatabase.CreateAsset(optionsData, DATAFILE_OPTIONS_VIEW_LOCATION + "/OptionsViewData.asset");
                return optionsData;
            }
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            optionsData = AssetDatabase.LoadAssetAtPath<OptionsViewData>(path);
            return optionsData;
        }
        public static OptionsViewData GetOptionsViewData()
        {
            OptionsViewData data = null;
            string[] guids = AssetDatabase.FindAssets("t:OptionsViewData");

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            data = AssetDatabase.LoadAssetAtPath<OptionsViewData>(path);
            return data;
        }
        public static void DeleteCharacter(string _characterName)
        {
            OptionsViewData _data = GetOptionsViewData();

            DeleteCharacterDataFolders(_characterName, _data);
        }
        public static void CreateCharacterData(NewCharacterCreationData _data, OptionsViewData _optionsData)
        {
            CharacterLibrary lib = GetCharacterLibrary();
            lib.AddCharacter(_data.CharacterName);
            CreateCharacterDataFolders(_data, _optionsData);
            CreateCharacterSavedGraphFile(_data, _optionsData);
        }
        public static string GetCharacterStateFolder(string _characterName, string _rootFolder)
        {
            return _rootFolder + "/" + _characterName + "/" + "States";
        }
        public static string GetCharacterConfigurationsFolder(string _characterName, string _rootFolder)
        {
            return _rootFolder + "/" + _characterName + "/" + "Configurations/";

        }
        public static string GetCharacterPrefabsFolder(string _characterName, string _rootFolder)
        {
            return _rootFolder + "/" + _characterName + "/" + "Prefabs";


        }
        public static string GetCharacterSavedGraphPath(string _characterName, string _rootFolder)
        {
            return _rootFolder + "/" + _characterName + ".asset";
        }
        public static string GetCombatStateName(string _characterName, string _stateType)
        {
            string nameFormat = "{0}_{1}_CombatState";
            return string.Format(nameFormat, _characterName, _stateType);
        }
        public static string GetCharacterRootFolder(string _characterName, string _rootFolder)
        {
            return _rootFolder + "/" + _characterName;
        }

        #endregion

        #region Data Utili
        private static void CreateCharacterDataFolders(NewCharacterCreationData _data, OptionsViewData _optionsData)
        {
            string rootCharfolder = GetCharacterRootFolder(_data.CharacterName, _optionsData.CharacterDataPath);

            
            AssetDatabase.CreateFolder(_optionsData.CharacterDataPath, _data.CharacterName);
            AssetDatabase.CreateFolder(rootCharfolder, "Configurations");
            AssetDatabase.CreateFolder(rootCharfolder, "Prefabs");
            AssetDatabase.CreateFolder(rootCharfolder, "States");
        }
        private static void DeleteCharacterDataFolders(string _characterName, OptionsViewData _optionsData)
        {
            string rootCharfolder = GetCharacterRootFolder(_characterName, _optionsData.CharacterDataPath);
            AssetDatabase.DeleteAsset(rootCharfolder+ "/States");
            AssetDatabase.DeleteAsset(rootCharfolder + "/Prefabs");
            AssetDatabase.DeleteAsset(rootCharfolder + "/Configurations");
            AssetDatabase.DeleteAsset(_optionsData.CharacterDataPath+"/"+ _characterName);
        }
        private static void CreateCharacterSavedGraphFile(NewCharacterCreationData _data, OptionsViewData _optionsData)
        {
            CharacterSavedGraph savedGraph = ScriptableObject.CreateInstance<CharacterSavedGraph>();
            string path = _optionsData.SavedGraphsPath + "/" + _data.CharacterName.ToString() + ".asset";
            AssetDatabase.CreateAsset(savedGraph, path);
        }
       
        #endregion
    }

}
