using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP{
	public interface IMessageHandler {

		IMessageWriter getWriter();
		IMessageReader getRreader();
		GameObject gameObject{get;}
	}
}