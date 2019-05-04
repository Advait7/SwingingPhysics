using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour {

    public GameObject m_ropeShooter;

    private SpringJoint2D m_rope;
    public int m_maximumRopeFramecount;
    private int m_ropeFramecount;

    public LineRenderer m_lineRenderer;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
	}

    void LateUpdate()
    {
        if(m_rope != null)
        {
            m_lineRenderer.enabled = true;
            m_lineRenderer.SetVertexCount(2);
            m_lineRenderer.SetPosition(0, m_ropeShooter.transform.position);
            m_lineRenderer.SetPosition(0, m_rope.connectedAnchor);
        }
        else
        {
            m_lineRenderer.enabled = false;
        }
    }

    void Fire()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 originPosition = m_ropeShooter.transform.position;
        Vector3 direction = mousePosition - originPosition;

        RaycastHit2D hit = Physics2D.Raycast(originPosition, direction, Mathf.Infinity);

        if(hit.collider != null)
        {
            SpringJoint2D newRope = m_ropeShooter.AddComponent<SpringJoint2D>();
            newRope.enableCollision = false;
            newRope.frequency = 0.2f;
            newRope.connectedAnchor = hit.point;
            newRope.enabled = true;

            GameObject.DestroyImmediate(m_rope);
            m_rope = newRope;
            m_ropeFramecount = 0;
        }
    }
}
