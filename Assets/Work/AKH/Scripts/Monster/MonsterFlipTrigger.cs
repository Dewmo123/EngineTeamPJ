using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFlipTrigger : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _monsterLayer;
    private void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.right, _distance, _monsterLayer);
        if (!ray) return;
        ray.collider.GetComponent<Monster>().Flip();
        gameObject.SetActive(false);
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * _distance);
    }
#endif
}
