
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;

namespace OTG.CombatSystem.Editor
{
    public static class OTGCombatEditorUtilis
    {
        static List<OTGCombatAction> AvailableActions;
        #region Constants
        public const string DATAFILE_OPTIONS_VIEW_LOCATION = "Assets/Submodules/otg-combat-system/Editor/Data";
        public const string DATAFILE_CHARACTER_LIBRARY_LOCATION = "Assets/Submodules/otg-combat-system/Editor/Data";
        public const string TEMPLATE_PATH_MAIN_WINDOW = "Assets/Submodules/otg-combat-system/Editor/MainWindows/OTGCombatSystemEditorTemplate.uxml";
        public const string STYLE_PATH_MAIN_WINDOW = "Assets/Submodules/otg-combat-system/Editor/MainWindows/OTGCombatSystemEditorStyle.uss";
        #endregion

        #region Event Calls
        public static void OnRequestActions()
        {
            var targetName = "OTG.Game";
            Assembly asm = Assembly.Load(targetName);

            GetAllActionsInGameAssembly(asm);
            GetAllTransitionsInAssembly(asm);
        }
        #endregion
        #region Public API - Data Tools
        public static void PopulateListView<T>(ref ListView _targetListView, ref VisualElement _ownerContainer, List<T> _items, string _listAreaName, bool tailOfPath = false)
        {
            _targetListView = _ownerContainer.Query<ListView>(_listAreaName).First();

            _targetListView.Clear();

           
            _targetListView.makeItem = () => new Label();

            //if (_items.Count == 0)
                //return;

            _targetListView.bindItem = (element, i) =>
            {
                string labelText = string.Empty;

                if (_items.Count > 0)
                {
                    labelText = _items[i].ToString();
                }
                    

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
        public static OTGCombatState CreateNewState(string _characterName,string _stateName)
        {
            OptionsViewData optionsData = GetOptionsViewData();
            OTGCombatState state = ScriptableObject.CreateInstance<OTGCombatState>();
            state.name = GetCombatStateName(_characterName, _stateName);

            string stateFolder = GetCharacterStateFolder(_characterName, optionsData.CharacterDataPath);
            AssetDatabase.CreateAsset(state, stateFolder + "/" + state.name + ".asset");

            return state;
        }
        public static List<string> GetAllActionsLocations()
        {
            List<string> paths = new List<string>();
            string[] guids = AssetDatabase.FindAssets("t:OTGCombatAction");
            for(int i = 0; i < guids.Length; i++)
            {
                paths.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
            }
            return paths;
        }
        public static OTGCombatAction GetCombatActionFromPath(string _path)
        {
            return AssetDatabase.LoadAssetAtPath<OTGCombatAction>(_path);
        }
        public static List<string> GetAllStates(string _characterName)
        {
            OptionsViewData optionsView = GetOptionsViewData();

            string dataFolder = GetCharacterStateFolder(_characterName, optionsView.CharacterDataPath);
            
            List<string> paths = new List<string>();
            string[] guids = AssetDatabase.FindAssets("t:OTGCombatState", new[] { dataFolder });
            for (int i = 0; i < guids.Length; i++)
            {
                paths.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
            }
            return paths;
        }
        public static OTGCombatState GetCombatStateFromPath(string _path)
        {
            return AssetDatabase.LoadAssetAtPath<OTGCombatState>(_path);
        }
        public static List<string> GetAllTransitionsLocations()
        {
            
            List<string> paths = new List<string>();
            string[] guids = AssetDatabase.FindAssets("t:OTGTransitionDecision");
            for (int i = 0; i < guids.Length; i++)
            {
                paths.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
            }
            return paths;
        }
        public static OTGTransitionDecision GetTransitionDecisionFromPath(string _path)
        {
            return AssetDatabase.LoadAssetAtPath<OTGTransitionDecision>(_path);
        }
        public static List<string> GetAllAnimationsLocations()
        {

            List<string> paths = new List<string>();
            string[] guids = AssetDatabase.FindAssets("t:AnimationClip");
            for (int i = 0; i < guids.Length; i++)
            {
                paths.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
            }
            return paths;
        }
        public static AnimationClip GetAnimationClipFromPath(string _path)
        {
            return AssetDatabase.LoadAssetAtPath<AnimationClip>(_path);
        }
        public static string GetActionOrTransitionNameFromPath(string _path)
        {
            string retVal = string.Empty;
            string[] parts = _path.Split('/');
            string assetName = parts[parts.Length - 1];
            string[] assetNameParts = assetName.Split('.');
            retVal = assetNameParts[0];

            return retVal;
        }
        public static void DeleteCharacter(string _characterName)
        {
            OptionsViewData _data = GetOptionsViewData();

            DeleteCharacterDataFolders(_characterName, _data);
            DeleteCharacterFromScene(_characterName);
        }
        public static void CreateCharacterData(NewCharacterCreationData _data, OptionsViewData _optionsData)
        {
            CharacterLibrary lib = GetCharacterLibrary();
            lib.AddCharacter(_data.CharacterName);
            CreateCharacterDataFolders(_data, _optionsData);
            CreateCharacterSavedGraphFile(_data, _optionsData);
        }
        public static OTGCombatSMC GetCharacterFromScene(string _characterName)
        {
            OTGCombatSMC target = null;
            OTGCombatSMC[] smcs = GameObject.FindObjectsOfType<OTGCombatSMC>();
            for (int i = 0; i < smcs.Length; i++)
            {
                if (string.Equals(smcs[i].gameObject.name, _characterName, System.StringComparison.Ordinal))
                {
                    target = smcs[i];
                }
            }
            return target;
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
        private static void GetAllActionsInGameAssembly(Assembly _asm)
        {
            OptionsViewData data = GetOptionsViewData();
            var actions2 = _asm.GetTypes();

            string[] existing = AssetDatabase.FindAssets("t:OTGCombatAction");
            List<string> paths = new List<string>();

            for(int i = 0; i < existing.Length; i++)
            {
                paths.Add(AssetDatabase.GUIDToAssetPath(existing[i]));
            }

            foreach(Type ty in actions2)
            {
                if (string.Equals(ty.BaseType.Name, "OTGCombatAction", StringComparison.OrdinalIgnoreCase))
                {
                    var match = paths.Where(x => x.Contains(ty.Name));
                    if (match.Count() > 0)
                        continue;

                    var act = ScriptableObject.CreateInstance(ty.Name);
                    AssetDatabase.CreateAsset(act, data.ActionsPath +"/" +ty.Name + ".asset");
                }
            }
        }
        private static void GetAllTransitionsInAssembly(Assembly _asm)
        {
            
            OptionsViewData data = GetOptionsViewData();
            var transitions2 = _asm.GetTypes();

            string[] existing = AssetDatabase.FindAssets("t:OTGTransitionDecision");
            List<string> paths = new List<string>();

            for (int i = 0; i < existing.Length; i++)
            {
                paths.Add(AssetDatabase.GUIDToAssetPath(existing[i]));
            }

            foreach (Type ty in transitions2)
            {
                if (string.Equals(ty.BaseType.Name, "OTGTransitionDecision", StringComparison.OrdinalIgnoreCase))
                {
                    var match = paths.Where(x => x.Contains(ty.Name));
                    if (match.Count() > 0)
                        continue;

                    var act = ScriptableObject.CreateInstance(ty.Name);
                    AssetDatabase.CreateAsset(act, data.TransitionsPath + "/" + ty.Name + ".asset");
                }
            }
        }
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
            AssetDatabase.DeleteAsset(GetCharacterSavedGraphPath(_characterName, rootCharfolder));
            GetCharacterLibrary().RemoveCharacter(_characterName);
            AssetDatabase.DeleteAsset(_optionsData.CharacterDataPath+"/"+ _characterName);
        }
        private static void DeleteCharacterFromScene(string _characterName)
        {
            OTGCombatSMC[] smcs = GameObject.FindObjectsOfType<OTGCombatSMC>();
            for(int i = 0; i < smcs.Length; i++)
            {
                if(string.Equals(smcs[i].gameObject.name,_characterName,System.StringComparison.Ordinal))
                {
                    GameObject.DestroyImmediate(smcs[i].gameObject);
                }
            }
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
