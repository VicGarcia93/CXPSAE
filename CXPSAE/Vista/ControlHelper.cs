using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPSAE.Vista
{
    public partial class ControlHelper : Component
    {
        public ControlHelper()
        {
            InitializeComponent();
        }

        public ControlHelper(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
