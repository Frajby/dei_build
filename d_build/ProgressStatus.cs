using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d_build
{
    class ProgressStatus
    {
        /// <summary>
        /// Datamodel pro report progresu v async/await operacích
        /// </summary>
        public float percent { get; set; }
        public string commentary { get; set; }

        public ProgressStatus(float percent, string commentary)
        {
            this.percent = percent;
            this.commentary = commentary;
        }

    }
}
