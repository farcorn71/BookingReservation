using System.IO;
using Microsoft.AspNetCore.Hosting;
using MimeKit;

namespace Booking.BLL
{
    public interface IEmailHelper
    {
        BodyBuilder BodyBuilder(string file);
    }

    public class EmailHelper : IEmailHelper
    {
        private readonly IHostingEnvironment _env;

        public EmailHelper(IHostingEnvironment env)
        {
            _env = env;
        }

        public BodyBuilder BodyBuilder(string file)
        {
            var pathToFile = _env.WebRootPath
                             + Path.DirectorySeparatorChar.ToString()
                             + "Templates"
                             + Path.DirectorySeparatorChar.ToString()
                             + "Emails"
                             + Path.DirectorySeparatorChar.ToString()
                             + file;

            var builder = new BodyBuilder();

            using (StreamReader sourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = sourceReader.ReadToEnd();
            }

            return builder;
        }
    }
}
