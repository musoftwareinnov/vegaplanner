using Microsoft.Extensions.Options;
using vega.Core.Models.Settings;

namespace vega.Core.Utils
{
    public class DateFormatString
    {
        private readonly DateFormatSetting options;
        public DateFormatString(IOptionsSnapshot<DateFormatSetting> options)
        {
            this.options = options.Value;
        }
    }
}