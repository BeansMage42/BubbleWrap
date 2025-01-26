// using UnityEditor;
// using UnityEngine;
//
// [CustomEditor(typeof(FenceGenerator))]
// public class FenceEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector(); // Keep default fields
//
//         FenceGenerator fence = (FenceGenerator)target;
//         if (GUILayout.Button("Add Posts"))
//         {
//             fence.AddPosts();
//         }
//         if (GUILayout.Button("Update Posts"))
//         {
//             fence.UpdatePosts();
//         }
//         if (GUILayout.Button("Clear Posts"))
//         {
//             fence.ClearPosts();
//         }
//     }
// }