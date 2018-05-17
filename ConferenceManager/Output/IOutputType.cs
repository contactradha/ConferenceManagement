using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceManager.View
{
    /// <summary>
    /// abstract class to be implemented by different outputs
    /// </summary>
    public interface IOutputType
    {
        void DiplayData(Output output);
    }
}
