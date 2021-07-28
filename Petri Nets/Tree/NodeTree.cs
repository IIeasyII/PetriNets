using Petri_Nets.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Petri_Nets.Core;

namespace Petri_Nets.TreeNet
{
    public class NodeTree
    {
        public int[] MNet { get; set; }

        /// <summary>
        /// Состояние сети
        /// </summary>
        public Model NetState{ get; set; }
    }
}
