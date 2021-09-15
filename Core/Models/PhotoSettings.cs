using System.IO;
using System.Linq;

namespace udemy_course1.Core.Models
{
    // Photo Settings
    public class PhotoSettings
    {
        // Max bytes in file
        public int MaxBytes { get; set; }
        // Accepted file types
        public string[] AcceptedFileTypes { get; set; }

        // Check if filename is supported
        public bool IsSupported(string fileName){
            return AcceptedFileTypes.Any(s => s == Path.GetExtension(fileName).ToLower());
        }
    }
}