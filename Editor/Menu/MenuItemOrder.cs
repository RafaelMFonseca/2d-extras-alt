using UnityEngine;

namespace UnityEditor.Tilemaps
{
    internal enum ETilesMenuItemOrder
    {
        RuleTileBitmasking = 300
    }

    static internal partial class AssetCreation
    {
        [MenuItem("Assets/Create/2D/Tiles/Alternatives/Rule Tile Bitmasking", priority = (int)ETilesMenuItemOrder.RuleTileBitmasking)]
        static void CreateRuleTileBitmasking()
        {
            ProjectWindowUtil.CreateAsset(ScriptableObject.CreateInstance<RuleTileBitmasking>(), "New Rule Tile Bitmasking.asset");
        }
    }
}
