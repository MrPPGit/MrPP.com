using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Myth
{
    public class PersonBifrost : Bifrost
    {

        public override void close()
        {

            if (!amIGod && _onClose != null)
            {
                base.close();

            }
        }
        public override void open()
        {
            if (!amIGod && _onOpen != null)
            {
                base.open();

            }
        }
    }
}