
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
            OTGCombatEditorUtilis.CreateCharacter(this, m_optionsViewData);
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
        
        #endregion

        public static string GetDisplayName(string _path)
        {
            string[] pathParts = _path.Split('/');
            string modelPath = pathParts[pathParts.Length - 1];
            return modelPath;
        }
    }

}
