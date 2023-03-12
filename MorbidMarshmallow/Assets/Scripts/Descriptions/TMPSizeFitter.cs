using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[AddComponentMenu("Layout/TMPSizeFitter")]
public class TMPSizeFitter : MonoBehaviour
{
	public GameObject backGround;
	public GameObject pivotPoint;
	public GameObject description;
	public float finalPrefferedHeight;
	[SerializeField]
	private TMPro.TextMeshProUGUI m_TextMeshPro;
	public TMPro.TextMeshProUGUI TextMeshPro
	{
		get { 
			if(m_TextMeshPro == null && transform.GetComponentInChildren<TMPro.TextMeshProUGUI>())
			{
				m_TextMeshPro = backGround.GetComponent<TMPro.TextMeshProUGUI>();
				m_TMPrectTransform = m_TextMeshPro.rectTransform;
			}
				return m_TextMeshPro;
		}
	}

	private RectTransform m_RectTransform;
	public RectTransform rectTransform { 
		get 
		{ 
			if(m_RectTransform == null)
			{
				m_RectTransform =  backGround.GetComponent<RectTransform>();
			}
			return m_RectTransform;
		}
	}

	private RectTransform m_TMPrectTransform;

	public RectTransform TMPrectTransform { get { return m_TMPrectTransform; } }

	private float m_PreferredHeight;

	public float PreferredHeight { get { return m_PreferredHeight; } }

	private void SetHeight()
	{
		if(TextMeshPro == null)
			return;
		m_PreferredHeight = TextMeshPro.preferredHeight;
		rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, m_PreferredHeight + 50);
	}

	private void OnEnable()
	{
		SetHeight();
	}

	private void Start()
	{
		SetHeight();
	}

	private void Update()
	{
		if(PreferredHeight != TextMeshPro.preferredHeight)
		{
			SetHeight();
		}
	}
}
