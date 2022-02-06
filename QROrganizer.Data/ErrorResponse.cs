using System.Collections.Generic;

namespace QROrganizer.Data;

public class ErrorResponse
{
    public ICollection<string> Errors { get; set; }
}
