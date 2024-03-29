﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace MANA3DGames
{
	public class UIMenu : GOGroup
    {
		#region [Constructors]

		public UIMenu( GameObject root ) : base( root )
        {
        }
			
		#endregion

		#region [Listeners Functions]

		public bool RemoveAllListenerOnButton( string name, bool inner = false  )
		{
			GameObject go = Get( name );
			if ( go )
			{
				Button btn = inner ? go.GetComponentInChildren<Button>() : go.GetComponent<Button>();
				if ( btn )
				{
					btn.onClick.RemoveAllListeners();
					return true;
				}
			}

			return false;
		}


		public bool AddListenerToButton( string name, UnityEngine.Events.UnityAction action, bool inner = false )
        {
            GameObject go = Get( name );
            if ( go )
            {
				Button btn = inner ? go.GetComponentInChildren<Button>() : go.GetComponent<Button>();
                if ( btn )
                {
                    btn.onClick.AddListener( action );
                    return true;
                }
            }

            return false;
        }

		public bool AddListenerToRepeatButton( string name, UnityEngine.Events.UnityAction downAction, UnityEngine.Events.UnityAction upAction )
		{
			AddEventTrigger( name, EventTriggerType.PointerDown, ( act )=> { downAction.Invoke(); } );
			return AddEventTrigger( name, EventTriggerType.PointerUp, ( act )=> { upAction.Invoke(); } );
		}


		public bool AddEventTrigger( string name, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> action )
		{
			GameObject go = Get( name );
			if ( go )
				return AddEventTrigger( go, type, action );

			return false;
		}

		public static bool AddEventTrigger( GameObject go, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> action )
		{
			EventTrigger trigger = go.GetComponent<EventTrigger>();
			if ( !trigger )
				trigger = go.AddComponent<EventTrigger>();

			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = type;
			entry.callback.AddListener( action );
			//trigger.delegates.Add( entry );
			trigger.triggers.Add( entry );
			return true;
		}


		public bool AddListenerToSliderValueChanged( string name, UnityEngine.Events.UnityAction<float> action, bool inner = false )
		{
			GameObject go = Get( name );
			if ( go )
			{
				Slider slider = inner ? go.GetComponentInChildren<Slider>() : go.GetComponent<Slider>();
				if ( slider )
				{
					slider.onValueChanged.AddListener( action );
					return true;
				}
			}

			return false;
		}

		#endregion

		#region [Set Functions]

		public bool SetText( string name, string text, bool internalTxt = false )
		{
			GameObject go = Get( name );
			if ( go )
			{
				TextMeshProUGUI txt = internalTxt ? go.GetComponentInChildren<TextMeshProUGUI>() : go.GetComponent<TextMeshProUGUI>();
				if ( txt )
				{
					txt.text = text;
					return true;
				}
			}

			return false;
		}


		public bool SetAlpha( string name, float alpha, bool internalImg = false, bool show = true )
		{
			GameObject go = Get( name );
			if ( go )
			{
				if ( internalImg )
				{
					Graphic[] graphics = go.GetComponentsInChildren<Graphic>();

					if ( graphics != null )
					{
                        for (int i = 0; i < graphics.Length; i++)
                        {
                            if (graphics[i].gameObject == go  )
								continue;
							var color = graphics[i].color;
                            graphics[i].color = new Color( color.r, color.g, color.b, alpha );
							go.SetActive( show );
						}
						return true;
					}
				}
				else
				{
					Graphic graphic = go.GetComponent<Graphic>();

					if ( graphic )
					{
						var color = graphic.color;
						graphic.color = new Color( color.r, color.g, color.b, alpha );
						go.SetActive( show );
						return true;
					}
				}
			}

			return false;
		}

		public bool SetAlphaChildren( string name, float alpha, bool show = true )
		{
			GameObject go = Get( name );
			if ( go )
			{
				Graphic[] graphics = go.GetComponentsInChildren<Graphic>();
				if ( graphics != null )
				{
					for ( int i = 0; i < graphics.Length; i++ ) 
					{
						if (graphics[i].gameObject == go  )
							continue;
						var color = graphics[i].color;
                        graphics[i].color = new Color( color.r, color.g, color.b, alpha );
						go.SetActive( show );
					}
					return true;
				}
			}

			return false;
		}

        public bool SetColor( string name, Color color, bool internalTxt = false )
        {
            GameObject go = Get( name );
			if ( go )
            {
				if ( internalTxt )
				{
					Graphic[] graphics = go.GetComponentsInChildren<Graphic>();

					if ( graphics != null )
					{
                        for (int i = 0; i < graphics.Length; i++)
						{
							if (graphics[i].gameObject == go  )
								continue;
                            graphics[i].color = color;
						}
						return true;
					}
				}
				else
				{
					Graphic graphic = go.GetComponent<Graphic>();

					if ( graphic )
					{
						graphic.color = color;
						return true;
					}
				}

            }

            return false;
        }

        public bool SetInputFiledText( string name, string text, bool internalTxt = false )
        {
            GameObject go = Get( name );
			if ( go )
            {
                TMP_InputField txt = internalTxt ? go.GetComponentInChildren<TMP_InputField>() : go.GetComponent<TMP_InputField>();
                if ( txt )
                {
                    txt.text = text;
                    return true;
                }
            }

            return false;
        }

		public bool SetImageSprite( string name, Sprite sprite, bool internalImg = false, bool activate = true, bool setNative = false )
		{
			GameObject go = Get( name );
			if ( go )
			{
				Image img = go.GetComponent<Image>();
				if ( internalImg )
				{
					if ( go.transform.childCount > 0 )
						img = go.transform.GetChild(0).GetComponent<Image>();
				}

				if ( img )
				{
					img.sprite = sprite;

					if ( setNative )
						img.SetNativeSize();

					if ( activate )
						go.gameObject.SetActive( true );
					return true;
				}
			}

			return false;
		}

		public void SetImageNaitve( string name, bool internalImg = false )
		{
			GameObject go = Get( name );
			if ( go )
			{
				Image img = go.GetComponent<Image>();
				if ( internalImg )
				{
					if ( go.transform.childCount > 0 )
						img = go.transform.GetChild(0).GetComponent<Image>();
				}

				if ( img )
					img.SetNativeSize();
			}
		}


		public void SetButtonInteractable( string name, bool val )
		{
			GameObject go = Get( name );
			if ( go )
				go.GetComponent<Button>().interactable = val;
			else
				Debug.Log( name + " couldn't be found in " + root.name );
		}

		public void SetAllButtonsInteractable( bool val )
		{
			foreach ( var item in gameObjects )
			{
				var btn = item.Value.GetComponent<Button>();
				if ( btn )
					btn.interactable = val;
			}
		}
			

		public void SetImageFillVertical( string name, Image.OriginVertical origin )
		{
            GameObject go = Get( name );
			if ( go )
            {
                Image img = go.GetComponent<Image>();
                img.type = Image.Type.Filled;
                img.fillMethod = Image.FillMethod.Vertical;
                img.fillOrigin = (int)origin;
            }
		}
		public void SetImageFillHorizontal( string name, Image.OriginHorizontal origin )
		{
            GameObject go = Get( name );
			if ( go )
            {
                Image img = go.GetComponent<Image>();
                img.type = Image.Type.Filled;
                img.fillMethod = Image.FillMethod.Horizontal;
                img.fillOrigin = (int)origin;
            }
		}

		public void SetImageFillValue( string name, float val )
		{
            GameObject go = Get( name );
			if ( go )
			    go.GetComponent<Image>().fillAmount = val;
		}

		public void SetSliderValue( string name, float val )
		{
            GameObject go = Get( name );
			if ( go )
			    go.GetComponent<Slider>().value = val;
		}


		public void SetRectScale( string name, Vector2 scale, bool show = true )
		{
			GameObject go = Get( name );
			if ( go )
			{
				go.GetComponent<RectTransform>().localScale = scale;
				go.SetActive( show );
			}
			else
				Debug.Log( name + " couldn't be found in " + root.name );
		}

		public void SetRectScaleX( string name, float val, bool show = true )
		{
			GameObject go = Get( name );
			if ( go )
			{
				var scale = go.GetComponent<RectTransform>().localScale;
                go.GetComponent<RectTransform>().localScale = new Vector3( val, scale.y, scale.z );
                go.SetActive( show );
			}
			else
				Debug.Log( name + " couldn't be found in " + root.name );
		}

		public void SetRectScaleY( string name, float val, bool show = true )
		{
			GameObject go = Get( name );
			if ( go )
			{
				var scale = go.GetComponent<RectTransform>().localScale;
                go.GetComponent<RectTransform>().localScale = new Vector3( scale.x, val, scale.z );
                go.SetActive( show );
			}
			else
				Debug.Log( name + " couldn't be found in " + root.name );
		}



		public void SetAnchoredPosition( string name, Vector2 pos, bool show = false )
		{
			GameObject go = Get( name );
			if ( go )
			{
                go.GetComponent<RectTransform>().anchoredPosition = pos;
                go.SetActive( show );
			}
			else
				Debug.Log( name + " couldn't be found in " + root.name );
		}

		public void SetAnchoredPositionX( string name, float x, bool show = false )
		{
			GameObject go = Get( name );
			if ( go )
			{
				var y = go.GetComponent<RectTransform> ().anchoredPosition.y;
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2( x, y );
                go.SetActive( show );
			}
			else
				Debug.Log( name + " couldn't be found in " + root.name );
		}

		public void SetAnchoredPositionY( string name, float y, bool show = false )
		{
			GameObject go = Get( name );
			if ( go )
			{
				var x = go.GetComponent<RectTransform> ().anchoredPosition.x;
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2( x, y );
                go.SetActive( show );
			}
			else
				Debug.Log( name + " couldn't be found in " + root.name );
		}


		public void SetItemChildrenActiveState( string name, bool active )
		{
			var children = GetChildren( name );
			for ( int i = 0; i < children.Length; i++ )
                children[i].SetActive( active );
		}


        public void SetDropDownOption( string name, List<string> optionData )
        {
            GameObject go = Get( name );
            if ( go )
            {
                TMP_Dropdown drop = go.GetComponent<TMP_Dropdown>();
                drop.ClearOptions();
                drop.AddOptions( optionData );
            }
        }
        public void SetDropDownOption( string name, List<TMP_Dropdown.OptionData> optionData )
        {
            GameObject go = Get( name );
            if ( go )
            {
                TMP_Dropdown drop = go.GetComponent<TMP_Dropdown>();
                drop.ClearOptions();
                drop.AddOptions( optionData );
            }
        }
        public void SetDropDownOption( string name, List<Sprite> optionData )
        {
            GameObject go = Get( name );
            if ( go )
            {
                TMP_Dropdown drop = go.GetComponent<TMP_Dropdown>();
                drop.ClearOptions();
                drop.AddOptions( optionData );
            }
        }
        public void SetDropDownSelected( string name, int value )
        {
            GameObject go = Get( name );
            if ( go )
            {
                TMP_Dropdown drop = go.GetComponent<TMP_Dropdown>();
                drop.value = value;
            }
        }

		#endregion

		#region [Get Functions]

        public string GetText( string name, bool internalTxt = false )
        {
            GameObject go = Get( name );
			if ( go )
            {
				TextMeshProUGUI txt = internalTxt ? go.GetComponentInChildren<TextMeshProUGUI>() : go.GetComponent<TextMeshProUGUI>();
                if ( txt )
                    return txt.text;
            }

            return string.Empty;
        }

		public string GetInputFieldValue( string name, bool internalTxt = false )
		{
			GameObject go = Get( name );
			if ( go )
			{
				TMP_InputField txt = internalTxt ? go.GetComponentInChildren<TMP_InputField>() : go.GetComponent<TMP_InputField>();
				if ( txt )
                    return txt.text;
            }

			return string.Empty;
		}

		public float GetSliderValue( string name, bool internalTxt = false )
		{
			GameObject go = Get( name );
			if ( go )
			{
				Slider slider = internalTxt ? go.GetComponentInChildren<Slider>() : go.GetComponent<Slider>();
				if ( slider )
                    return slider.value;
            }

			return 0.0f;
		}

        public Image GetUIImage( string name, bool internalImg = false )
        {
            GameObject go = Get( name );
			if ( go )
            {
                if ( internalImg )
                {
                    if ( go.transform.childCount > 0 )
                        return go.transform.GetChild( 0 ).GetComponent<Image>();
                }
                else
                    return go.GetComponent<Image>();
            }

            return null;
        }

        public Sprite GetImageSprite( string name, bool internalImg = false )
		{
            Image img = GetUIImage( name, internalImg );
            if ( img )
                return img.sprite;
			return null;
		}

        public float GetImageFillAmount( string name, bool internalImg = false )
        {
            Image img = GetUIImage( name, internalImg );
            if ( img )
                return img.fillAmount;

            return -1.0f;
        }

        public TextMeshProUGUI GetTextMeshProUGUI( string name, bool internalTxt = false )
        {
            GameObject go = Get( name );
			if ( go )
            {
                TextMeshProUGUI txt = internalTxt ? go.GetComponentInChildren<TextMeshProUGUI>() : go.GetComponent<TextMeshProUGUI>();
                return txt;
            }

            return null;
        }


		public RectTransform GetRootRect()
		{
			return root.GetComponent<RectTransform>();
		}
		public float GetRootRectPositionX()
		{
			return GetRootRect().anchoredPosition.x;
		}
		public float GetRootRectPositionY()
		{
			return GetRootRect().anchoredPosition.y;
		}


		public RectTransform GetRect( string name )
		{
			GameObject go = Get( name );
			if ( go )
			    return go.GetComponent<RectTransform>();
            else
                return null;
		}
		public float GetRectPositionX( string name )
		{
			GameObject go = Get( name );
			if ( go )
			    return go.GetComponent<RectTransform>().anchoredPosition.x;
            else
                return 0.0f;
		}
		public float GetRectPositionY( string name )
		{
			GameObject go = Get( name );
			if ( go )
			    return go.GetComponent<RectTransform>().anchoredPosition.y;
            else
                return 0.0f;
		}


		public float GetRectScaleX( string name )
		{
			GameObject go = Get( name );
			if ( go )
			{
				return go.GetComponent<RectTransform>().localScale.x;
			}
			else
				Debug.Log( name + " couldn't be found in " + root.name );

			return 0.0f;
		}
		public float GetRectScaleY( string name )
		{
			GameObject go = Get( name );
			if ( go )
			{
				return go.GetComponent<RectTransform>().localScale.y;
			}
			else
				Debug.Log( name + " couldn't be found in " + root.name );

			return 0.0f;
		}


		public Graphic GetGraphic( string name )
		{
			GameObject go = Get( name );
			if ( go )
				return go.GetComponent<Graphic>();

			return null;
		}

		public Color GetGraphicColor( string name )
		{
			GameObject go = Get( name );
			if ( go )
				return go.GetComponent<Graphic>().color;

			return Color.white;
		}


        public string GetDropDownTextValue( string name )
        {
            GameObject go = Get( name );
            if ( go )
            {
                TMP_Dropdown drop = go.GetComponent<TMP_Dropdown>();
                return drop.options[drop.value].text;
            }
            else
                return string.Empty;
        }

        public List<TMP_Dropdown.OptionData> GetDropDownOption( string name )
        {
            GameObject go = Get( name );
            if ( go )
                return go.GetComponent<TMP_Dropdown>().options;
            else
                return null;
        }

		#endregion


        public void SetSelected( string name )
        {
            EventSystem.current.SetSelectedGameObject( Get( name ) );
        }

        public static void SetSelected( GameObject uiGO )
        {
            EventSystem.current.SetSelectedGameObject( uiGO );
        }
    }
}