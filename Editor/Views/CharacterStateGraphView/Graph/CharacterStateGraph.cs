
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;

namespace OTG.CombatSystem.Editor
{
    public class CharacterStateGraph : GraphView
    {
        private Port m_selectedPort;
        private List<CharacterStateNode> m_nodesInGraph;

        #region Public API
        public void OnViewFocused()
        {
            Initialize();
            AddManipulators();
           // ConstructGridBackground();
          
        }
        public void OnCharacterSelected(SelectedCharacterData _data)
        {
            //CreateStartingNode(_data);
            for(int i = 0; i < _data.AvailableStates.Count; i++)
            {
                if (_data.AvailableStates[i].IsRepeat)
                    continue;

                CharacterStateNode n = GenerateNode(_data.AvailableStates[i]);
            }
        }
        public void Cleanup()
        {
            CleanupNodeList();
            Clear();
        }
        public void OnViewLostFocus()
        {
            Clear();
        }
        public void AddNode(SerializedObject _stateOwner)
        {
            //CharacterStateNode newNode = GenerateNode(_stateOwner);
        }
        #endregion


        #region Utility
        private void Initialize()
        {
            m_nodesInGraph = new List<CharacterStateNode>();
        }
       private void CleanupNodeList()
        {
            for(int i = 0; i < m_nodesInGraph.Count; i++)
            {
                CharacterStateNode n = m_nodesInGraph[i];
                Remove(n);
            }
            m_nodesInGraph = null;
        }
       
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> combatablePorts = new List<Port>();
          
           
            
            ports.ForEach(port => 
            {
                if(startPort != port && startPort.node != port.node)
                {
                    
                    combatablePorts.Add(port);
                }
            });

            return combatablePorts;
        }
        
        
        private void AddManipulators()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ContentZoomer());
            
           
        }
        private void ConstructGridBackground()
        {
            GridBackground grid = new GridBackground();
            grid.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Submodules/otg-combat-system/Editor/Views/CharacterStateGraphView/Graph/CharacterStateGraphStyle.uss"));

            Insert(0, grid);
            grid.StretchToParentSize();
        }

        private CharacterStateNode GenerateNode(AvailableStateData _stateData)
        {
            
            CharacterStateNode n = new CharacterStateNode(_stateData.StateSOBj);
            AddElement(n);

            //Vector2 position = m_charViewData.CharacterStateGraph.GetNodePosition(_nodeData);
            //AddStateNodeToStack(n);

            Rect parentPosition = new Rect(_stateData.WidthLevl * 500, _stateData.HeightLevel * 200, 150, 150);
            n.SetPosition(parentPosition);

            return n;
        }

        #endregion
    }
}
