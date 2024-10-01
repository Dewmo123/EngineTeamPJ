using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    private SpringJoint2D _jointCompo;
    private Rigidbody2D _rbCompo;
    private RaycastHit2D _ray;
    private Vector2 _point;
    private void Awake()
    {
        _jointCompo = GetComponent<SpringJoint2D>();
        _rbCompo = GetComponent<Rigidbody2D>();
        _jointCompo.enabled = false;
    }
    private void Update()
    {
        if (UnityEngine.Input.GetAxisRaw("Horizontal")==1)
        {
            _jointCompo.distance -= 0.01f;
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            _ray = Physics2D.Raycast(transform.position, (Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition)-transform.position).normalized,100,_groundLayer);
            _point = _ray.point;
            _jointCompo.enabled = true;
            _jointCompo.connectedAnchor = _point;
            _jointCompo.distance *= 0.7f;
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.P))
        {
            _rbCompo.AddForce(Vector2.up*5, ForceMode2D.Impulse);
        }
    }
}
