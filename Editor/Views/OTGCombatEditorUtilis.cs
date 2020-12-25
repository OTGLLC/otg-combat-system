
using System.Collections.Generic;
using UnityEngine.UIElements;


namespace OTG.CombatSystem.Editor
{
    public static class OTGCombatEditorUtilis
    {
        #region Data manip
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
                    val += pathParts[i] + "/";
                }
            }

            return val;
        }
        #endregion
    }

}
