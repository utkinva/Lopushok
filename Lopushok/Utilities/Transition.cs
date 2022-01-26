using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lopushok.Entities;
using System.Windows.Controls;

namespace Lopushok.Utilities
{
    internal class Transition
    {
        public static Frame mainFrame;
        private static LopushokEntities _context { get; set; }
        public static LopushokEntities Context
        {
            get
            {
                if (_context == null)
                    _context = new LopushokEntities();
                return _context;
            }
        }
    }
}
