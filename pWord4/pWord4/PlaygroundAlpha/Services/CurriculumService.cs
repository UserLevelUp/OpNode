using PlaygroundAlpha.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundAlpha.Services
{
    public class CurriculumService : ICurriculum
    {
        public async Task<bool> DoAsynchWork1Second()
        {
            try
            {
                return await new Task<bool>(() => true);
            }
            catch(Exception ex)
            {
                return await new Task<bool>(() =>  false);
            }
            
        }

        public async Task<bool> DoAsyncWork2Seconds()
        {
            try
            {
                return await new Task<bool>(() => true);
            }
            catch(Exception ex)
            {
                return await new Task<bool>(() => false);
            }
        }

        public Task<bool> DoWork1Second()
        {
            return new Task<bool>(() => true);
        }

        public Task<bool> DoWork2Seconds()
        {
            return new Task<bool>(() => false);
        }
    }
}
