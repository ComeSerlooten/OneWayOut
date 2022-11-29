using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opendoors : MonoBehaviour
{


	public GameObject Pivot, Porte;
	public int Angle = 130;
	private int CurAngle;
	public bool Ouverture = false;
	private bool keyE = false;

	

	

	void Update()
	{
		if (Ouverture && Input.GetKeyDown(KeyCode.E))   //Si on est dans le trigger et qu'on appuie sur E
			keyE = true;                                    //On déclenche l'ouverture

		if (keyE)
		{                                       //Si l'ouverture est déclenchée on ouvre
			if (CurAngle < Angle)
			{
				CurAngle += 1;
				Porte.transform.RotateAround(Pivot.transform.position, -Vector3.up, CurAngle * Time.deltaTime);
			}
		}

		if (!Ouverture)
		{                                   //Si on sort du trigger
			if (CurAngle != 0)
			{
				CurAngle -= 1;
				Porte.transform.RotateAround(Pivot.transform.position, Vector3.up, CurAngle * Time.deltaTime);
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
	{
			
			Ouverture = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		Ouverture = false;
		keyE = false;                   //Remettre aussi le déclencheur à false pour réinitialiser
	}
}