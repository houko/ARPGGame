using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*!
 *	----------------------------------------------------------------------
 *	@brief	簡易追従カメラ
 *	
*/
public class SimpleFollowCam : MonoBehaviour
{
	[SerializeField] private Transform	m_Target		= null;
	[SerializeField] private float		m_FollowSpeed	= 6.0f;


	/*!
	 *	----------------------------------------------------------------------
	 *	@brief	初期化
	*/
//	private void Start()
//	{
//	}

	/*!
	 *	----------------------------------------------------------------------
	 *	@brief	更新
	*/
	private void FixedUpdate()
	{
		if( null == m_Target ) return;

		float followSpeed = (m_FollowSpeed * Time.deltaTime);
		Vector3 pos = this.transform.position;
		pos = Vector3.Lerp( pos, m_Target.position, followSpeed );
		this.transform.position = pos;
	}


}
