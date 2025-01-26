using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal.Converters;
using UnityEngine;

[ExecuteInEditMode]
public class FenceGenerator : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    private List<Transform> _posts;

    private List<GameObject> _g;
    // Start is called before the first frame update
    
    [ContextMenu("Add Posts")]
    public void AddPosts()
    {
        _posts = new List<Transform>();
        _g = new List<GameObject>();
        foreach (var p in GetComponentsInChildren<Transform>())
        {
            _posts.Add(p);
        }

        _posts.RemoveAt(0);

        for (int i = 0; i < _posts.Count; i++)
        {
            _g.Add(Instantiate(fencePrefab, transform));
        }
        
        for(int i = 0; i < _posts.Count; i++) MoveBoard(i);
    }
    
    [ContextMenu("Add Posts")]
    public void UpdatePosts()
    {
        for(int i = 0; i < _posts.Count; i++) MoveBoard(i);
    }

    public void ClearPosts()
    {
        while (_g.Count > 0)
        {
            print(_g[0].name);
            DestroyImmediate(_g[0].gameObject);
            _g.RemoveAt(0);
        }
        _g.Clear();
    }
    void MoveBoard(int index)
    {
        Vector3 v = _posts[(index + 1) % _posts.Count].position - _posts[index].position;
        Vector2 v1 = new Vector2(v.x, v.z);

        _g[index % (_g.Count)].transform.localScale = new Vector3(v1.magnitude, _g[index % (_g.Count)].transform.localScale.y, _g[index %
            (_g.Count)].transform.localScale.z);
        _g[index % (_g.Count)].transform.localPosition = _posts[index].localPosition + (v * 0.5f);
        _g[index % (_g.Count)].transform.localRotation = Quaternion.Euler(0, (CurrectAngle(v1) * -1), 0);
    }
    float CurrectAngle(Vector3 v)
    {
        float angle = Vector2.SignedAngle(Vector2.up, v) - 90;
        if (angle < 0)
        {
            angle = (angle + 360) % 360;
        }
        
        return angle;
    }
}
