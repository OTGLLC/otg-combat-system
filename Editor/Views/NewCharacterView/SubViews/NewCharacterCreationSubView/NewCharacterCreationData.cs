
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace OTG.CombatSystem.Editor
{
    public class NewCharacterCreationData
    {
        #region Properties
        public string CharacterName { get; private set; }
        public e_CombatantType CharacterType { get; private set; }
        public Object CharacterModel { get; private set; }
        public Avatar CharacterAvatar { get; private set; }
        public string SelectedModelPath { get; set; }
        public  List<string> AvailableModelsPaths { get; private set; }
        public List<string> AvailableAvatarsPaths { get; private set; }
       
        #endregion

        #region Fields
        private OptionsViewData m_optionsViewData;
        private GameObject m_characterGameObject;
        private SerializedObject m_combatSMC;
        #endregion

        #region Public API

        public NewCharacterCreationData()
        {
            InstantiateOptionsViewData();
            FindAvailableModels();
            FindAvatars();
        }
        public void Refresh()
        {
            InstantiateOptionsViewData();
            FindAvailableModels();
            FindAvatars();
        }
        public void CreateCharacter()
        {
            OTGCombatEditorUtilis.CreateCharacterData(this, m_optionsViewData);
            CreateCharacterGameObject();
            AttachCombatController();
            FocusOnAddedCharacter();
            CreateAndAttachHandlerDataGroup();
            LinkGlobalCombatConfig();
            CreateInitialState();
            ApplyCharacterModel();
            ApplyCharacterType();
        }
        public void Cleanup()
        {
            CleanupOptionsViewData();
            AvailableModelsPaths = null;
            AvailableAvatarsPaths = null;
        }
        public void OnCharacterModelSelected(Object _charmodel)
        {
            CharacterModel = _charmodel;
           
        }
        public void OnCharacterTypeSelected(e_CombatantType _type)
        {
            CharacterType = _type;
        }
        public void OnCharacterName(string _val)
        {
            CharacterName = _val;
        }
        #endregion

        #region Utility
        private void InstantiateOptionsViewData()
        {
            string[] guids = AssetDatabase.FindAssets("t:OptionsViewData");
            for(int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                m_optionsViewData = AssetDatabase.LoadAssetAtPath<OptionsViewData>(path);
            }
        }
        private void CleanupOptionsViewData()
        {
            m_optionsViewData = null;
        }
        private void FindAvailableModels()
        {
            string[] guids = AssetDatabase.FindAssets(" model t:Model");
            AvailableModelsPaths = new List<string>();
            for(int i = 0; i < guids.Length; i++)
            {
                AvailableModelsPaths.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
            }
            
        }
        private void FindAvatars()
        {
            string[] guids = AssetDatabase.FindAssets("Avatar t:Avatar");
            AvailableAvatarsPaths = new List<string>();
            for (int i = 0; i < guids.Length; i++)
            {
                AvailableAvatarsPaths.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
            }
        }
        #endregion

        #region Creation Factory
        private void CreateCharacterGameObject()
        {
            m_characterGameObject = new GameObject(CharacterName);
            m_characterGameObject.transform.position = Vector3.zero;


        }
        private void AttachCombatController()
        {
            
            m_characterGameObject.AddComponent<OTGCombatSMC>();
            m_combatSMC = new SerializedObject(m_characterGameObject.GetComponent<OTGCombatSMC>());
        }
        private void FocusOnAddedCharacter()
        {
            Selection.activeObject = m_characterGameObject;
            SceneView.FrameLastActiveSceneViewWithLock();
        }
        private void CreateAndAttachHandlerDataGroup()
        {
            HandlerDataGroup dataGrp = ScriptableObject.CreateInstance<HandlerDataGroup>();
            dataGrp.name = CharacterName + "_HanderDataGroup";

            string path = OTGCombatEditorUtilis.GetCharacterConfigurationsFolder(CharacterName, m_optionsViewData.CharacterDataPath);
            AssetDatabase.CreateAsset(dataGrp, path + dataGrp.name + ".asset");

            m_combatSMC.FindProperty("m_handlerDataGroup").objectReferenceValue = dataGrp;
            m_combatSMC.ApplyModifiedProperties();
        }
        private void LinkGlobalCombatConfig()
        {
            m_combatSMC.FindProperty("m_globalConfig").objectReferenceValue = m_optionsViewData.GlobalCombatConfig;
            m_combatSMC.ApplyModifiedProperties();
        }
        private void CreateInitialState()
        {
            OTGCombatState initialState = ScriptableObject.CreateInstance<OTGCombatState>();
            initialState.name = OTGCombatEditorUtilis.GetCombatStateName(CharacterName, "Inititial");

            string stateFolder = OTGCombatEditorUtilis.GetCharacterStateFolder(CharacterName, m_optionsViewData.CharacterDataPath);
            string initialStateGUID = AssetDatabase.CreateFolder(stateFolder, "InitialState");
            string initialStatePath = AssetDatabase.GUIDToAssetPath(initialStateGUID);
            AssetDatabase.CreateAsset(initialState, initialStatePath + "/" + initialState.name + ".asset");

            m_combatSMC.FindProperty("m_startingState").objectReferenceValue = initialState;
            m_combatSMC.ApplyModifiedProperties();

        }
        private void ApplyCharacterType()
        {
            m_combatSMC.FindProperty("m_combatantType").enumValueIndex = (int)CharacterType;
            m_combatSMC.ApplyModifiedProperties();
        }
        private void ApplyCharacterModel()
        {
            GameObject characterModel = (GameObject)PrefabUtility.InstantiatePrefab(CharacterModel);

            Animator anim = characterModel.GetComponent<Animator>();
            if (anim != null)
                GameObject.DestroyImmediate(anim);

            characterModel.transform.SetParent(m_characterGameObject.transform);
            characterModel.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        #endregion

        public static string GetDisplayName(string _path)
        {
            string[] pathParts = _path.Split('/');
            string modelPath = pathParts[pathParts.Length - 1];
            return modelPath;
        }
    }

}
