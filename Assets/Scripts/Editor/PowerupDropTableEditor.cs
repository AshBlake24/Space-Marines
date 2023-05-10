using System.Linq;
using Roguelike.StaticData.Loot.Powerups;
using UnityEditor;
using UnityEngine;

namespace Roguelike.Editor
{
    [CustomEditor(typeof(PowerupDropTable))]
    public class PowerupDropTableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            PowerupDropTable dropTable = (PowerupDropTable)target;
            
            if (GUILayout.Button("Sort Table"))
                dropTable.PowerupConfigs = dropTable.PowerupConfigs.OrderByDescending(x => x.Weight).ToList();
                
            base.OnInspectorGUI();
        }
    }
}