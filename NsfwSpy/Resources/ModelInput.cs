using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsfwSpyNS
{
    class ModelInput
    {
        public ModelInput(byte[] image)
        {
            Image = image;
        }

        public byte[] Image { get; set; }
    }
}
