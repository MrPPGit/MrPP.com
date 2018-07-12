using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
namespace MrPP.Myth { 
    public interface IRedio {

        Task open { get; }
        Task close { get; }

        int id { get; }
    }
}