using UnityEngine;
using System.Collections;

public class DisplayFPS : MonoBehaviour
{
	float deltaTime = 0.0f;
	// variable de type float 
	void Update()
	{// calcule de la variable delta time qui prend en compte le temps entre deux frame(Time.unscaled...) moins le précedent delta time sauvegarder
		// au qu'elle on fait un facteur de lissage 
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
	{
		// multi declaration de variable int
		int w = Screen.width, h = Screen.height;

		//creation d'une variale style baser sur le GUIStyle qui contient une nouvelle fonction appeler GUYStyle 
		GUIStyle style = new GUIStyle();
		// creation d'une variable Rect = qui contient un nouveau rectangle 
		Rect rect = new Rect(10, 10, w, h * 2 / 100);
		// on vas chercher le composent style et modifer certain des parametre 
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 4 / 100;
		style.normal.textColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
		// on vas calculer le nombre de milliseconde dans cette variable a avec la variable deltaTime calculer plus haut * 1000
		float msec = deltaTime * 1000.0f;
		//transformation du nombre de frame en fps 
		float fps = 1.0f / deltaTime;
		//concatennation des information calculer plus haut 
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		//affichage sur le GUI
		GUI.Label(rect, text, style);
	}
}