using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundAlpha.Services.Interfaces
{
    public interface ICurriculum
    {
        // Async
        Task<bool> DoAsynchWork1Second();
        Task<bool> DoAsyncWork2Seconds();


        // Non Async
        Task<bool> DoWork1Second();
        Task<bool> DoWork2Seconds();
        
    }
}
