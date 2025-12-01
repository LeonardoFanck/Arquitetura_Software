using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosShared.Model;

public class Email
{
    public string Destinatario { get; set; } = string.Empty;
    public string Mensagem { get; set; } = string.Empty;
}
