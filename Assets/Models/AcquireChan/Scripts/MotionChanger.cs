using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*!
 *	----------------------------------------------------------------------
 *	@brief	モーション切り替えスクリプト
 *	
 *	@note	←→キーかOnGUIのボタンでモーションを切り替える
 *	
*/
public class MotionChanger : MonoBehaviour
{
	[SerializeField] private Animator	m_Animator;

	private int					m_AnimationIndex = 0;
	private int					m_AnimationMax = 0;
	private AnimatorStateInfo	m_PrevState;
	private bool				m_ChangingMotion = false;




	/*!
	 *	----------------------------------------------------------------------
	 *	@brief	初期化
	*/
	private void Start()
	{
		AnimationClip[] AnimationClips = m_Animator.runtimeAnimatorController.animationClips;

		m_AnimationIndex = 0;
		m_AnimationMax = AnimationClips.Length;
		m_PrevState = m_Animator.GetCurrentAnimatorStateInfo(0);

		// index
		for( int i=0; i < m_AnimationMax; ++i )
		{
			if( m_PrevState.IsName( AnimationClips[i].name ) )
			{
				m_AnimationIndex = i;
				break;
			}
		}
	}

	/*!
	 *	----------------------------------------------------------------------
	 *	@brief	更新
	*/
	private void Update()
	{

		// モーション遷移中
		AnimatorStateInfo animState = m_Animator.GetCurrentAnimatorStateInfo(0);
		if( animState.fullPathHash != m_PrevState.fullPathHash )
		{
			// 遷移完了
			m_Animator.SetBool( "prev", false );
			m_Animator.SetBool( "next", false );

			m_ChangingMotion = false;
			m_PrevState = m_Animator.GetCurrentAnimatorStateInfo(0);
		}
		else
		{
			// モーション変更
			if( Input.GetKeyDown( KeyCode.LeftArrow ) )
			{
				PrevAnimation();
			}
			else if( Input.GetKeyDown( KeyCode.RightArrow ) )
			{
				NextAnimation();
			}
		}

		// 終了
		if( Input.GetKeyDown( KeyCode.Escape ) ) Application.Quit();
	}

	/*!
	 *	----------------------------------------------------------------------
	 *	@brief	アニメーション次へ
	*/
	private void NextAnimation()
	{
		if( m_ChangingMotion ) return;

		m_AnimationIndex = ((m_AnimationIndex + 1) % m_AnimationMax);
		m_PrevState = m_Animator.GetCurrentAnimatorStateInfo(0);
		m_Animator.SetBool( "next", true );
		m_ChangingMotion = true;
	}

	/*!
	 *	----------------------------------------------------------------------
	 *	@brief	アニメーション前へ
	*/
	private void PrevAnimation()
	{
		if( m_ChangingMotion ) return;

		m_AnimationIndex = ((m_AnimationIndex - 1 + m_AnimationMax) % m_AnimationMax);
		m_PrevState = m_Animator.GetCurrentAnimatorStateInfo(0);
		m_Animator.SetBool( "prev", true );
		m_ChangingMotion = true;
	}






	/*!
	 *	----------------------------------------------------------------------
	 *	@brief	ボタン表示
	*/
	private void OnGUI()
	{
		GUIStyle tempStyle = GUI.skin.box;
		tempStyle.fontSize = 24;

		Vector2 boxSize = new Vector2( 350f, 100f );
		Vector2 pos = new Vector2( (Screen.width - boxSize.x - 50f), (Screen.height - boxSize.y - 50f) );

		// モーション名
		string animName = m_Animator.runtimeAnimatorController.animationClips[m_AnimationIndex].name;
		string text = string.Format( "{0} [{1}/{2}]", animName, (m_AnimationIndex + 1), m_AnimationMax );
		GUI.Box( new Rect( pos, boxSize ), text, tempStyle );

		// ボタン
		pos.x += 60f;
		pos.y += 50f;
		if( GUI.Button( new Rect( pos, new Vector2(100f, 40f) ), "<<", tempStyle ) )
		{
			PrevAnimation();
		}
		pos.x += (100f + 30f);
		if( GUI.Button( new Rect( pos, new Vector2(100f, 40f) ), ">>", tempStyle ) )
		{
			NextAnimation();
		}
	}


}
