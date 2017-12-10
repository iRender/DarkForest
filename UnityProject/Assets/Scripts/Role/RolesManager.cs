using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RolesManager : MonoSingleton<RolesManager>
{
	public Dictionary<int, Role> m_rolesDic;
	public List<Role> m_rolesList;
	// Use this for initialization
	void Awake ()
	{
		m_rolesDic = new Dictionary<int, Role> ();
		m_rolesList = new List<Role> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_rolesList.Sort ((x, y) => {
			if (x != null && y != null)
			{
			return y.PosYValue.CompareTo (x.PosYValue);
			}
			return 0;
		});
		int depth = 0;
		foreach (Role role in m_rolesList) {
			role.SetDepth (depth);
			depth++;
		}
	}

	public void AddRole (Role role)
	{
		m_rolesDic.Add (role.m_id, role);
		m_rolesList.Add (role);
	}
}
