using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP{
	public class LogDebugItem : MonoBehaviour {
		#region IInputClickHandler implementation
        /*
		public void OnInputClicked (MrPP.Helper.InputModule.InputClickedEventData eventData)
		{
			LogMessage.Instance.show (item_.message);
		}
        */
		#endregion

		public struct Item
		{
			public Color32 color;
			public string text;
			public string message;
			public int no;
		};


		private Item item_;
		public Item item{
			get{
				return item_;
			}

		}

		[SerializeField]
		private UnityEngine.UI.Text _text = null;
		private static string ToText (LogDebugItem.Item item){
			//;
			string ret = "<color=#"+item.color.r.ToString("x2") + item.color.g.ToString("x2") +item.color.b.ToString("x2")+">" +item.no.ToString() +":"+item.text +"</color>";
			return ret;
		}

		public void show(Item item){
			item_ = item;
			_text.text = ToText (item_);
		}
	}
}