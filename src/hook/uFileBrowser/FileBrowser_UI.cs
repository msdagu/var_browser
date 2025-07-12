﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
//using MVR.FileManagement;
//using MVR.FileManagementSecure;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
namespace var_browser
{
    public partial class FileBrowser : MonoBehaviour
    {
		//public UIDynamicPopup CreateFilterablePopup(JSONStorableStringChooser jsc, int yOffset)
		//{
		//	UIDynamicPopup uIDynamicPopup = null;

		//	var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();

		//	if (manager != null && manager.configurableFilterablePopupPrefab != null && jsc.popup == null)
		//	{
		//		Transform transform = CreateUIElement(manager.configurableFilterablePopupPrefab.transform, yOffset);
		//		if (transform != null)
		//		{
		//			uIDynamicPopup = transform.GetComponent<UIDynamicPopup>();
		//			if (uIDynamicPopup != null)
		//			{
		//				uIDynamicPopup.label = jsc.name;
		//				jsc.popup = uIDynamicPopup.popup;
		//			}
		//		}
		//	}
		//	return uIDynamicPopup;
		//}
        protected RectTransform CreateUIContainer(int xOffset, int yOffset, int width, int height)
        {
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableScrollablePopupPrefab != null)
            {
				RectTransform backgroundTransform = manager.configurableScrollablePopupPrefab.transform.Find("Background") as RectTransform;
				RectTransform rectTransform = UnityEngine.Object.Instantiate(backgroundTransform, this.window.transform);

				//RectTransform rectTransform = transform.GetComponent<RectTransform>();
				rectTransform.localRotation = Quaternion.identity;
				rectTransform.localPosition = new Vector3(-1725, 850, 0);
				rectTransform.anchorMin = new Vector2(0, 1);
				rectTransform.anchorMax = new Vector2(0, 1);
				rectTransform.pivot = new Vector2(0, 1);
				//yOffset往负值增长
				rectTransform.anchoredPosition = new Vector2(xOffset, yOffset);
				rectTransform.sizeDelta = new Vector2(width, height);//大小
				rectTransform.localScale = Vector3.one;

				var layout=	rectTransform.gameObject.AddComponent<VerticalLayoutGroup>();
				layout.padding = new RectOffset(2, 2, 2, 2);
				layout.spacing = 2;
				layout.childAlignment = TextAnchor.UpperLeft;
				layout.childControlHeight = true;
				layout.childControlWidth = true;
				layout.childForceExpandHeight = false;
				layout.childForceExpandWidth = false;
				return rectTransform;
			}
			return null;
        }

		RectTransform CreateLabel(RectTransform parent, string v,Color color,bool bold=false)
        {
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableSpacerPrefab != null)
			{
				RectTransform transform = UnityEngine.Object.Instantiate(manager.configurableSpacerPrefab, parent) as RectTransform;
				transform.gameObject.SetActive(true);

                UIDynamic header = transform.GetComponent<UIDynamic>();
                header.height = 40;
                var text = header.gameObject.AddComponent<Text>();
                text.text = v;
                text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                text.fontSize = 30;
                text.fontStyle = bold ? FontStyle.Bold : FontStyle.Normal;
                text.color = color;
                return transform;
            }
			return null;
		}

		UIDynamicPopup CreateFilterablePopup(RectTransform parent, JSONStorableStringChooser jsc)
        {
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableFilterablePopupPrefab != null)
			{
				RectTransform transform = UnityEngine.Object.Instantiate(manager.configurableFilterablePopupPrefab, parent) as RectTransform;
				transform.gameObject.SetActive(true);
				var uIDynamicPopup = transform.GetComponent<UIDynamicPopup>();
				if (uIDynamicPopup != null)
				{
					uIDynamicPopup.label = jsc.name;
					jsc.popup = uIDynamicPopup.popup;
				}
				var popup = transform.GetComponent<UIPopup>();
				if (popup != null)
				{
					FieldInfo fieldInfo = typeof(UIPopup).GetField("maxNumber", BindingFlags.NonPublic | BindingFlags.Instance);
					fieldInfo.SetValue(popup, 999);
				}
				//var uIDynamicToggle = transform.GetComponent<UIDynamicToggle>();
				//if (uIDynamicToggle != null)
				//{
				//	//toggleToJSONStorableBool.Add(uIDynamicToggle, jsb);
				//	uIDynamicToggle.label = jsb.name;
				//	jsb.toggle = uIDynamicToggle.toggle;
				//}
				return uIDynamicPopup;
			}
			return null;
		}

		UIDynamicToggle CreateToggle(RectTransform parent, JSONStorableBool jsb)
        {
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableTogglePrefab != null)
            {
				RectTransform transform = UnityEngine.Object.Instantiate(manager.configurableTogglePrefab, parent) as RectTransform;
				transform.gameObject.SetActive(true);
				var uIDynamicToggle = transform.GetComponent<UIDynamicToggle>();
				if (uIDynamicToggle != null)
				{
					//toggleToJSONStorableBool.Add(uIDynamicToggle, jsb);
					uIDynamicToggle.label = jsb.name;
					jsb.toggle = uIDynamicToggle.toggle;
				}
				return uIDynamicToggle;
			}
			return null;
		}


        protected Transform CreateUIElement(Transform prefab, int yOffset)
		{
			Transform transform = null;
			if (prefab != null)
			{
				transform = UnityEngine.Object.Instantiate(prefab);
				transform.SetParent(this.window.transform);
				transform.gameObject.SetActive(true);

				RectTransform rectTransform = transform.GetComponent<RectTransform>();
				rectTransform.localRotation = Quaternion.identity;
				rectTransform.localPosition = new Vector3(-1725, 850, 0);
				rectTransform.anchorMin = new Vector2(0, 1);
				rectTransform.anchorMax = new Vector2(0, 1);
				rectTransform.pivot = new Vector2(0, 1);
				//yOffset往负值增长
				rectTransform.anchoredPosition = new Vector2(-500, yOffset);
				rectTransform.sizeDelta = new Vector2(500, 120);
				rectTransform.localScale = Vector3.one;
			}
			return transform;
		}

		public UIDynamicButton CreateRightButton(string label, int yOffset)
		{
			UIDynamicButton uIDynamicButton = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableButtonPrefab != null)
			{
				Transform transform = CreateRightUIElement(manager.configurableButtonPrefab.transform, yOffset);
				if (transform != null)
				{
					uIDynamicButton = transform.GetComponent<UIDynamicButton>();
					if (uIDynamicButton != null)
					{
						uIDynamicButton.label = label;
					}
				}
			}
			return uIDynamicButton;
		}
		protected Transform CreateRightUIElement(Transform prefab, int yOffset)
		{
			Transform transform = null;
			if (prefab != null)
			{
				transform = UnityEngine.Object.Instantiate(prefab);
				transform.SetParent(this.window.transform);
				transform.gameObject.SetActive(true);

				RectTransform rectTransform = transform.GetComponent<RectTransform>();
				rectTransform.localRotation = Quaternion.identity;
				rectTransform.localPosition = new Vector3(1225, 850, 0);
				rectTransform.anchorMin = new Vector2(1, 1);
				rectTransform.anchorMax = new Vector2(1, 1);
				rectTransform.pivot = new Vector2(0, 1);
				//yOffset往负值增长
				rectTransform.anchoredPosition = new Vector2(0, yOffset);
				rectTransform.sizeDelta = new Vector2(200, 50);
				rectTransform.localScale = Vector3.one;

			}
			return transform;
		}

		public UIDynamic CreateRightSpacer(int yOffset)
		{
			UIDynamic result = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableSpacerPrefab != null)
			{
				Transform transform = CreateRightUIElement(manager.configurableSpacerPrefab.transform, yOffset);
				if (transform != null)
				{
					result = transform.GetComponent<UIDynamic>();
				}
			}
			return result;
		}
		public UIDynamic CreateLeftSpacer(int yOffset)
		{
			UIDynamic result = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableSpacerPrefab != null)
			{
				Transform transform = CreateRightUIElement(manager.configurableSpacerPrefab.transform, yOffset);
				LogUtil.Log("CreateLeftSpacer "+transform.localScale);
				if (transform != null)
				{
					result = transform.GetComponent<UIDynamic>();
				}
			}
			return result;
		}
		void CreateRightHeader(string v, int yOffset, Color color)
		{
			var header = CreateRightSpacer(yOffset);
			header.height = 40;
			var text = header.gameObject.AddComponent<Text>();
			text.text = v;
			text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
			text.fontSize = 30;
			text.fontStyle = FontStyle.Bold;
			text.color = color;
		}
		UIDynamicSlider CreateRightSlider(JSONStorableFloat jsf,int yOffset)
		{
			UIDynamicSlider uIDynamicSlider = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableSliderPrefab != null)
			{
				Transform transform = CreateRightUIElement(manager.configurableSliderPrefab.transform, yOffset);
				if (transform != null)
				{
					RectTransform rectTransform = transform.GetComponent<RectTransform>();
					rectTransform.sizeDelta = new Vector2(300, 100);
					uIDynamicSlider = transform.GetComponent<UIDynamicSlider>();
					if (uIDynamicSlider != null)
					{
						uIDynamicSlider.Configure(jsf.name, jsf.min, jsf.max, jsf.val, jsf.constrained, "F2", showQuickButtons: false, !jsf.constrained);
						jsf.slider = uIDynamicSlider.slider;
					}
				}
			}
			return uIDynamicSlider;
		}
		void CreateLeftHeader(string v, int yOffset, Color color)
		{
			var header = CreateLeftSpacer(yOffset);
			header.height = 40;
			var text = header.gameObject.AddComponent<Text>();
			text.text = v;
			text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
			text.fontSize = 30;
			text.fontStyle = FontStyle.Bold;
			text.color = color;
		}
		public UIDynamicButton CreateInstallButton(int xOffset, int yOffset)
		{
			UIDynamicButton uIDynamicButton = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableButtonPrefab != null)
			{
				Transform transform = CreateUIElement(manager.configurableButtonPrefab.transform, yOffset);
				if (transform != null)
				{
					RectTransform rectTransform = transform.GetComponent<RectTransform>();
					rectTransform.anchoredPosition = new Vector2(xOffset, yOffset);
					rectTransform.sizeDelta = new Vector2(150, 60);
					
					uIDynamicButton = transform.GetComponent<UIDynamicButton>();
					if (uIDynamicButton != null)
					{
						uIDynamicButton.label = "Install Selected";
						// Set button color to green
						var button = uIDynamicButton.button;
						var colors = button.colors;
						colors.normalColor = new Color(0.2f, 0.8f, 0.2f, 1f); // Green
						colors.highlightedColor = new Color(0.3f, 0.9f, 0.3f, 1f);
						colors.pressedColor = new Color(0.1f, 0.7f, 0.1f, 1f);
						button.colors = colors;
					}
				}
			}
			return uIDynamicButton;
		}

		public UIDynamicButton CreateClearButton(int xOffset, int yOffset)
		{
			UIDynamicButton uIDynamicButton = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableButtonPrefab != null)
			{
				Transform transform = CreateUIElement(manager.configurableButtonPrefab.transform, yOffset);
				if (transform != null)
				{
					RectTransform rectTransform = transform.GetComponent<RectTransform>();
					rectTransform.anchoredPosition = new Vector2(xOffset, yOffset);
					rectTransform.sizeDelta = new Vector2(150, 60);
					
					uIDynamicButton = transform.GetComponent<UIDynamicButton>();
					if (uIDynamicButton != null)
					{
						uIDynamicButton.label = "Clear Selection";
						// Set button color to red
						var button = uIDynamicButton.button;
						var colors = button.colors;
						colors.normalColor = new Color(0.8f, 0.2f, 0.2f, 1f); // Red
						colors.highlightedColor = new Color(0.9f, 0.3f, 0.3f, 1f);
						colors.pressedColor = new Color(0.7f, 0.1f, 0.1f, 1f);
						button.colors = colors;
					}
				}
			}
			return uIDynamicButton;
		}

		public UIDynamicButton CreateLoadButton(int xOffset, int yOffset)
		{
			UIDynamicButton uIDynamicButton = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableButtonPrefab != null)
			{
				Transform transform = CreateUIElement(manager.configurableButtonPrefab.transform, yOffset);
				if (transform != null)
				{
					RectTransform rectTransform = transform.GetComponent<RectTransform>();
					rectTransform.anchoredPosition = new Vector2(xOffset, yOffset);
					rectTransform.sizeDelta = new Vector2(150, 60);
					
					uIDynamicButton = transform.GetComponent<UIDynamicButton>();
					if (uIDynamicButton != null)
					{
						uIDynamicButton.label = "Load";
						// Set button color to blue
						var button = uIDynamicButton.button;
						var colors = button.colors;
						colors.normalColor = new Color(0.2f, 0.5f, 0.8f, 1f); // Blue
						colors.highlightedColor = new Color(0.3f, 0.6f, 0.9f, 1f);
						colors.pressedColor = new Color(0.1f, 0.4f, 0.7f, 1f);
						button.colors = colors;
					}
				}
			}
			return uIDynamicButton;
		}

		public UIDynamicButton CreateSetFavoriteButton(int xOffset, int yOffset)
		{
			UIDynamicButton uIDynamicButton = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableButtonPrefab != null)
			{
				Transform transform = CreateUIElement(manager.configurableButtonPrefab.transform, yOffset);
				if (transform != null)
				{
					RectTransform rectTransform = transform.GetComponent<RectTransform>();
					rectTransform.anchoredPosition = new Vector2(xOffset, yOffset);
					rectTransform.sizeDelta = new Vector2(150, 60);
					
					uIDynamicButton = transform.GetComponent<UIDynamicButton>();
					if (uIDynamicButton != null)
					{
						uIDynamicButton.label = "Favorite";
						// Set button color to pink/magenta
						var button = uIDynamicButton.button;
						var colors = button.colors;
						colors.normalColor = new Color(0.8f, 0.2f, 0.8f, 1f); // Magenta
						colors.highlightedColor = new Color(0.9f, 0.3f, 0.9f, 1f);
						colors.pressedColor = new Color(0.7f, 0.1f, 0.7f, 1f);
						button.colors = colors;
					}
				}
			}
			return uIDynamicButton;
		}

		public UIDynamicButton CreateSetAutoInstallButton(int xOffset, int yOffset)
		{
			UIDynamicButton uIDynamicButton = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableButtonPrefab != null)
			{
				Transform transform = CreateUIElement(manager.configurableButtonPrefab.transform, yOffset);
				if (transform != null)
				{
					RectTransform rectTransform = transform.GetComponent<RectTransform>();
					rectTransform.anchoredPosition = new Vector2(xOffset, yOffset);
					rectTransform.sizeDelta = new Vector2(150, 60);
					
					uIDynamicButton = transform.GetComponent<UIDynamicButton>();
					if (uIDynamicButton != null)
					{
						uIDynamicButton.label = "Auto Install";
						// Set button color to orange
						var button = uIDynamicButton.button;
						var colors = button.colors;
						colors.normalColor = new Color(1f, 0.6f, 0.2f, 1f); // Orange
						colors.highlightedColor = new Color(1f, 0.7f, 0.3f, 1f);
						colors.pressedColor = new Color(0.9f, 0.5f, 0.1f, 1f);
						button.colors = colors;
					}
				}
			}
			return uIDynamicButton;
		}

		// Right-side function buttons
		public UIDynamicButton CreateRightInstallButton(int yOffset)
		{
			UIDynamicButton uIDynamicButton = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableButtonPrefab != null)
			{
				Transform transform = CreateRightUIElement(manager.configurableButtonPrefab.transform, yOffset);
				if (transform != null)
				{
					uIDynamicButton = transform.GetComponent<UIDynamicButton>();
					if (uIDynamicButton != null)
					{
						uIDynamicButton.label = "Install";
						// Set button color to green
						var button = uIDynamicButton.button;
						var colors = button.colors;
						colors.normalColor = new Color(0.2f, 0.8f, 0.2f, 1f); // Green
						colors.highlightedColor = new Color(0.3f, 0.9f, 0.3f, 1f);
						colors.pressedColor = new Color(0.1f, 0.7f, 0.1f, 1f);
						button.colors = colors;
					}
				}
			}
			return uIDynamicButton;
		}

		public UIDynamicButton CreateRightLoadButton(int yOffset)
		{
			UIDynamicButton uIDynamicButton = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableButtonPrefab != null)
			{
				Transform transform = CreateRightUIElement(manager.configurableButtonPrefab.transform, yOffset);
				if (transform != null)
				{
					uIDynamicButton = transform.GetComponent<UIDynamicButton>();
					if (uIDynamicButton != null)
					{
						uIDynamicButton.label = "Load";
						// Set button color to blue
						var button = uIDynamicButton.button;
						var colors = button.colors;
						colors.normalColor = new Color(0.2f, 0.5f, 0.8f, 1f); // Blue
						colors.highlightedColor = new Color(0.3f, 0.6f, 0.9f, 1f);
						colors.pressedColor = new Color(0.1f, 0.4f, 0.7f, 1f);
						button.colors = colors;
					}
				}
			}
			return uIDynamicButton;
		}

		public UIDynamicButton CreateRightClearButton(int yOffset)
		{
			UIDynamicButton uIDynamicButton = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableButtonPrefab != null)
			{
				Transform transform = CreateRightUIElement(manager.configurableButtonPrefab.transform, yOffset);
				if (transform != null)
				{
					uIDynamicButton = transform.GetComponent<UIDynamicButton>();
					if (uIDynamicButton != null)
					{
						uIDynamicButton.label = "Clear Selection";
						// Set button color to red
						var button = uIDynamicButton.button;
						var colors = button.colors;
						colors.normalColor = new Color(0.8f, 0.2f, 0.2f, 1f); // Red
						colors.highlightedColor = new Color(0.9f, 0.3f, 0.3f, 1f);
						colors.pressedColor = new Color(0.7f, 0.1f, 0.1f, 1f);
						button.colors = colors;
					}
				}
			}
			return uIDynamicButton;
		}

		public UIDynamicButton CreateRightSetFavoriteButton(int yOffset)
		{
			UIDynamicButton uIDynamicButton = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableButtonPrefab != null)
			{
				Transform transform = CreateRightUIElement(manager.configurableButtonPrefab.transform, yOffset);
				if (transform != null)
				{
					uIDynamicButton = transform.GetComponent<UIDynamicButton>();
					if (uIDynamicButton != null)
					{
						uIDynamicButton.label = "Favorite";
						// Set button color to pink/magenta
						var button = uIDynamicButton.button;
						var colors = button.colors;
						colors.normalColor = new Color(0.8f, 0.2f, 0.8f, 1f); // Magenta
						colors.highlightedColor = new Color(0.9f, 0.3f, 0.9f, 1f);
						colors.pressedColor = new Color(0.7f, 0.1f, 0.7f, 1f);
						button.colors = colors;
					}
				}
			}
			return uIDynamicButton;
		}

		public UIDynamicButton CreateRightSetAutoInstallButton(int yOffset)
		{
			UIDynamicButton uIDynamicButton = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableButtonPrefab != null)
			{
				Transform transform = CreateRightUIElement(manager.configurableButtonPrefab.transform, yOffset);
				if (transform != null)
				{
					uIDynamicButton = transform.GetComponent<UIDynamicButton>();
					if (uIDynamicButton != null)
					{
						uIDynamicButton.label = "Auto Install";
						// Set button color to orange
						var button = uIDynamicButton.button;
						var colors = button.colors;
						colors.normalColor = new Color(1f, 0.6f, 0.2f, 1f); // Orange
						colors.highlightedColor = new Color(1f, 0.7f, 0.3f, 1f);
						colors.pressedColor = new Color(0.9f, 0.5f, 0.1f, 1f);
						button.colors = colors;
					}
				}
			}
			return uIDynamicButton;
		}

		public UIDynamicButton CreateRightSetHiddenButton(int yOffset)
		{
			UIDynamicButton uIDynamicButton = null;
			var manager = SuperController.singleton.transform.Find("ScenePluginManager").GetComponent<MVRPluginManager>();
			if (manager != null && manager.configurableButtonPrefab != null)
			{
				Transform transform = CreateRightUIElement(manager.configurableButtonPrefab.transform, yOffset);
				if (transform != null)
				{
					uIDynamicButton = transform.GetComponent<UIDynamicButton>();
					if (uIDynamicButton != null)
					{
						uIDynamicButton.label = "Hide";
						// Set button color to dark gray
						var button = uIDynamicButton.button;
						var colors = button.colors;
						colors.normalColor = new Color(0.4f, 0.4f, 0.4f, 1f); // Dark gray
						colors.highlightedColor = new Color(0.5f, 0.5f, 0.5f, 1f);
						colors.pressedColor = new Color(0.3f, 0.3f, 0.3f, 1f);
						button.colors = colors;
					}
				}
			}
			return uIDynamicButton;
		}

		// ...existing code...
	}
}
