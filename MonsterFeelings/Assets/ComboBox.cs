using System;
using UnityEngine;

public class ComboBox
{
		private static bool forceToUnShow = false; 
		private static int useControlID = -1;
		private bool isClickedComboButton = false;
		private int selectedItemIndex = 0;
		public int SelectedItemIndex {
				get;
				set;
		}		


		private Rect rect;
		private GUIContent buttonContent;
		private GUIContent[] listContent;
		private string buttonStyle;
		private string boxStyle;
		private GUIStyle listStyle;
			
		public ComboBox (Rect rect, GUIContent _buttonContent, GUIContent[] _listContent, GUIStyle listStyle)
		{
				this.rect = rect;
				this.buttonContent = _buttonContent;
				this.listContent = _listContent;
				this.buttonStyle = "button";
				this.boxStyle = "box";
				this.listStyle = listStyle;
		}
			
		public ComboBox (Rect rect, GUIContent _buttonContent, GUIContent[] _listContent, string _buttonStyle, string _boxStyle, GUIStyle _listStyle)
		{
				this.rect = rect;
				this.buttonContent = _buttonContent;
				this.listContent = _listContent;
				this.buttonStyle = _buttonStyle;
				this.boxStyle = _boxStyle;
				this.listStyle = _listStyle;
		}
			
		public int Show ()
		{
				if (forceToUnShow) {
						forceToUnShow = false;
						isClickedComboButton = false;
				}
				
				bool done = false;
				int controlID = GUIUtility.GetControlID (FocusType.Passive);       
				
				
				switch (Event.current.GetTypeForControl (controlID)) {
				case EventType.mouseUp:
						{
								if (isClickedComboButton) {
										done = true;
								}
						}
						break;
				}  
			
				
				if (GUI.Button (rect, buttonContent, buttonStyle)) {
						if (GUI.Button (new Rect (200, 100, 100, 20), buttonContent, buttonStyle)) {
						}
						if (useControlID == -1) {
								useControlID = controlID;
								isClickedComboButton = false;
						}
					
						if (useControlID != controlID) {
								forceToUnShow = true;
								useControlID = controlID;
						}
						isClickedComboButton = true;
				}
				
				if (isClickedComboButton) {
						Rect listRect = new Rect (rect.x, rect.y + listStyle.CalcHeight (listContent [0], 1.0f),
					                         rect.width, listStyle.CalcHeight (listContent [0], 1.0f) * listContent.Length);
					
						GUI.Box (listRect, "", boxStyle);
						int newSelectedItemIndex = GUI.SelectionGrid (listRect, selectedItemIndex, listContent, 1, listStyle);
						if (newSelectedItemIndex != selectedItemIndex) {
								selectedItemIndex = newSelectedItemIndex;
								buttonContent = listContent [selectedItemIndex];
						}
				}
				
				if (done)
						isClickedComboButton = false;
				
				return selectedItemIndex;
		}
		
}

